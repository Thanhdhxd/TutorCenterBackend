using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuestionMedia
{
    public int QuestionMediaId { get; set; }

    public int QuestionId { get; set; }

    public int MediaId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Medium Media { get; set; } = null!;

    public virtual Question Question { get; set; } = null!;
}
