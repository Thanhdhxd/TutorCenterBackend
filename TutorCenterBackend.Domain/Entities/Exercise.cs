using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Exercise
{
    public int ExerciseId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? AttachMediaId { get; set; }

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Medium? AttachMedia { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ExerciseSubmission> ExerciseSubmissions { get; set; } = new List<ExerciseSubmission>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
}
