using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Question
{
    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    public int? SectionId { get; set; }

    public int? GroupId { get; set; }

    public string Content { get; set; } = null!;

    public string? Explanation { get; set; }

    public string QuestionType { get; set; } = null!;

    public double Points { get; set; }

    public int OrderIndex { get; set; }

    public virtual QuestionGroup? Group { get; set; }

    public virtual ICollection<QuestionMedia> QuestionMedia { get; set; } = new List<QuestionMedia>();

    public virtual ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();

    public virtual Quiz Quiz { get; set; } = null!;

    public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();

    public virtual QuizSection? Section { get; set; }
}
