using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Lecture
{
    public int LectureId { get; set; }

    public int? ParentId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public int? MediaId { get; set; }

    public DateTime UploadedAt { get; set; }

    public int UploadedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Lecture> InverseParent { get; set; } = new List<Lecture>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Medium? Media { get; set; }

    public virtual Lecture? Parent { get; set; }

    public virtual User UploadedByNavigation { get; set; } = null!;
}
