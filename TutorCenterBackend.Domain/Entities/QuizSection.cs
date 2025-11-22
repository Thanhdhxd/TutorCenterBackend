using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuizSection
{
    public int QuizSectionId { get; set; }

    public int QuizId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int OrderIndex { get; set; }

    public virtual ICollection<QuestionGroup> QuestionGroups { get; set; } = new List<QuestionGroup>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Quiz Quiz { get; set; } = null!;
}
