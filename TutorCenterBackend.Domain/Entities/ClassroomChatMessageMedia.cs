using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class ClassroomChatMessageMedia
{
    public int ChatMediaId { get; set; }

    public int MessageId { get; set; }

    public int MediaId { get; set; }

    public int OrderIndex { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Medium Media { get; set; } = null!;

    public virtual ClassroomChatMessage Message { get; set; } = null!;
}
