using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuestionGroupMedia
{
    public int QuestionGroupMediaId { get; set; }

    public int GroupId { get; set; }

    public int MediaId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual QuestionGroup Group { get; set; } = null!;

    public virtual Medium Media { get; set; } = null!;
}
