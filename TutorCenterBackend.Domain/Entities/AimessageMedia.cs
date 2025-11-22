using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class AimessageMedia
{
    public int MessageMediaId { get; set; }

    public int MessageId { get; set; }

    public int MediaId { get; set; }

    public string? Purpose { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Medium Media { get; set; } = null!;

    public virtual Aimessage Message { get; set; } = null!;
}
