using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Announcement
{
    public int AnnouncementId { get; set; }

    public int? ClassroomId { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Classroom? Classroom { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;
}
