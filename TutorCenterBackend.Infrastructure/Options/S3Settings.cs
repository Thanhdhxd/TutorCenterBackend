namespace TutorCenterBackend.Infrastructure.Options
{
    public class S3Settings
    {
        public string ServiceUrl { get; set; } = string.Empty;
        public string AccessKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string DefaultBucket { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
    }
}