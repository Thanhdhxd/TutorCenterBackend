using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuestionGroup
{
    public int QuestionGroupId { get; set; }

    public int QuizId { get; set; }

    public int? SectionId { get; set; }

    public string? Title { get; set; }

    public string? IntroText { get; set; }

    public int OrderIndex { get; set; }

    public bool ShuffleInside { get; set; }

    public virtual ICollection<QuestionGroupMedia> QuestionGroupMedia { get; set; } = new List<QuestionGroupMedia>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual QuizSection? Section { get; set; }
}
