using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuestionOptionMedia
{
    public int QuestionOptionMediaId { get; set; }

    public int OptionId { get; set; }

    public int MediaId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Medium Media { get; set; } = null!;

    public virtual QuestionOption Option { get; set; } = null!;
}
