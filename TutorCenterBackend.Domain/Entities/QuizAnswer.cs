using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuizAnswer
{
    public int AttemptId { get; set; }

    public int QuestionId { get; set; }

    public int OptionId { get; set; }

    public virtual QuizAttempt Attempt { get; set; } = null!;

    public virtual QuestionOption Option { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
