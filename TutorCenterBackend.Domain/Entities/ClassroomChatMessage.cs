using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class ClassroomChatMessage
{
    public int MessageId { get; set; }

    public int ClassroomId { get; set; }

    public int SenderId { get; set; }

    public string? Content { get; set; }

    public DateTime SentAt { get; set; }

    public bool IsEdited { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual ICollection<ClassroomChatMessageMedia> ClassroomChatMessageMedia { get; set; } = new List<ClassroomChatMessageMedia>();

    public virtual User Sender { get; set; } = null!;
}
