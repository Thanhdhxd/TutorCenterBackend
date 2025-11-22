using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class ActivityLog
{
    public long ActivityLogId { get; set; }

    public int UserId { get; set; }

    public string Action { get; set; } = null!;

    public string EntityType { get; set; } = null!;

    public int? EntityId { get; set; }

    public string? Metadata { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
