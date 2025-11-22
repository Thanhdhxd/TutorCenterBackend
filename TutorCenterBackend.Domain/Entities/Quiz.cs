using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Quiz
{
    public int QuizId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int TimeLimitSec { get; set; }

    public int MaxAttempts { get; set; }

    public bool ShuffleQuestions { get; set; }

    public bool ShuffleOptions { get; set; }

    public string GradingMethod { get; set; } = null!;

    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<QuestionGroup> QuestionGroups { get; set; } = new List<QuestionGroup>();

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<QuizSection> QuizSections { get; set; } = new List<QuizSection>();
}
