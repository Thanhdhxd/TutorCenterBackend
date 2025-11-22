using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuizAttempt
{
    public int QuizAttemptId { get; set; }

    public int LessonId { get; set; }

    public int QuizId { get; set; }

    public int StudentId { get; set; }

    public DateTime StartedAt { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public string Status { get; set; } = null!;

    public decimal? ScoreRaw { get; set; }

    public decimal? ScoreScaled10 { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();

    public virtual User Student { get; set; } = null!;
}
