using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class JoinRequest
{
    public int JoinRequestId { get; set; }

    public int ClassroomId { get; set; }

    public int StudentId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime RequestedAt { get; set; }

    public DateTime? HandledAt { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual User Student { get; set; } = null!;
}
