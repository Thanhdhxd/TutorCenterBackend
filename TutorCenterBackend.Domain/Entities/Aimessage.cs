using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Aimessage
{
    public int MessageId { get; set; }

    public int ConversationId { get; set; }

    public string SenderRole { get; set; } = null!;

    public string? Content { get; set; }

    public int? ParentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<AimessageMedia> AimessageMedia { get; set; } = new List<AimessageMedia>();

    public virtual Aiconversation Conversation { get; set; } = null!;

    public virtual ICollection<Aimessage> InverseParent { get; set; } = new List<Aimessage>();

    public virtual Aimessage? Parent { get; set; }
}
