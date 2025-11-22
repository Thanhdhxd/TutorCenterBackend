using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Aiagent
{
    public int AgentId { get; set; }

    public string Name { get; set; } = null!;

    public string? SystemPrompt { get; set; }

    public bool IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Aiconversation> Aiconversations { get; set; } = new List<Aiconversation>();

    public virtual User? CreatedByNavigation { get; set; }
}
