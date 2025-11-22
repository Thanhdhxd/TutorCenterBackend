using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class QuestionOption
{
    public int QuestionOptionId { get; set; }

    public int QuestionId { get; set; }

    public string Content { get; set; } = null!;

    public bool IsCorrect { get; set; }

    public int OrderIndex { get; set; }

    public virtual Question Question { get; set; } = null!;

    public virtual ICollection<QuestionOptionMedia> QuestionOptionMedia { get; set; } = new List<QuestionOptionMedia>();

    public virtual ICollection<QuizAnswer> QuizAnswers { get; set; } = new List<QuizAnswer>();
}
