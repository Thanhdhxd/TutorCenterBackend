using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class ExerciseSubmission
{
    public int ExerciseSubmissionId { get; set; }

    public int LessonId { get; set; }

    public int ExerciseId { get; set; }

    public int StudentId { get; set; }

    public int MediaId { get; set; }

    public DateTime SubmittedAt { get; set; }

    public decimal? Score { get; set; }

    public string? Comment { get; set; }

    public DateTime? GradedAt { get; set; }

    public virtual Exercise Exercise { get; set; } = null!;

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Medium Media { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
