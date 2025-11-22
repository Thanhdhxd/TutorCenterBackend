using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Aiconversation
{
    public int ConversationId { get; set; }

    public int? AgentId { get; set; }

    public int OwnerUserId { get; set; }

    public int? ClassroomId { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Aiagent? Agent { get; set; }

    public virtual ICollection<Aimessage> Aimessages { get; set; } = new List<Aimessage>();

    public virtual Classroom? Classroom { get; set; }

    public virtual User OwnerUser { get; set; } = null!;
}
