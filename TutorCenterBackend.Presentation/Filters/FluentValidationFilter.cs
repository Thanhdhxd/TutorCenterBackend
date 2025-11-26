using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TutorCenterBackend.Presentation.Filters
{
    /// <summary>
    /// Action filter t? ??ng validate request DTOs s? d?ng FluentValidation
    /// </summary>
    public class FluentValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // L?y t?t c? parameters t? action
            foreach (var parameter in context.ActionDescriptor.Parameters)
            {
                var parameterType = parameter.ParameterType;
                
                // Ch? validate complex types (không validate primitive types)
                if (!parameterType.IsPrimitive && parameterType != typeof(string))
                {
                    // L?y validator cho type này
                    var validatorType = typeof(IValidator<>).MakeGenericType(parameterType);
                    var validator = _serviceProvider.GetService(validatorType) as IValidator;

                    if (validator != null)
                    {
                        // L?y giá tr? parameter t? action arguments
                        if (context.ActionArguments.TryGetValue(parameter.Name, out var parameterValue) 
                            && parameterValue != null)
                        {
                            // Validate
                            var validationContext = new ValidationContext<object>(parameterValue);
                            var validationResult = await validator.ValidateAsync(validationContext);

                            if (!validationResult.IsValid)
                            {
                                // T?o error response
                                var errors = validationResult.Errors
                                    .GroupBy(e => e.PropertyName)
                                    .ToDictionary(
                                        g => g.Key,
                                        g => g.Select(e => e.ErrorMessage).ToArray()
                                    );

                                context.Result = new BadRequestObjectResult(new
                                {
                                    success = false,
                                    message = "D? li?u không h?p l?",
                                    errors = errors
                                });

                                return;
                            }
                        }
                    }
                }
            }

            // N?u validation pass, ti?p t?c execute action
            await next();
        }
    }
}
