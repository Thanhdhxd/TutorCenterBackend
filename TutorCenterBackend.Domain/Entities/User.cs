using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public int RoleId { get; set; }

    public int? AvatarMediaId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<Aiagent> Aiagents { get; set; } = new List<Aiagent>();

    public virtual ICollection<Aiconversation> Aiconversations { get; set; } = new List<Aiconversation>();

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual Medium? AvatarMedia { get; set; }

    public virtual ICollection<ClassroomChatMessage> ClassroomChatMessages { get; set; } = new List<ClassroomChatMessage>();

    public virtual ICollection<Classroom> ClassroomCreatedByNavigations { get; set; } = new List<Classroom>();

    public virtual ICollection<Classroom> ClassroomDeletedByNavigations { get; set; } = new List<Classroom>();

    public virtual ICollection<ClassroomStudent> ClassroomStudents { get; set; } = new List<ClassroomStudent>();

    public virtual ICollection<Classroom> ClassroomTutors { get; set; } = new List<Classroom>();

    public virtual ICollection<Classroom> ClassroomUpdatedByNavigations { get; set; } = new List<Classroom>();

    public virtual ICollection<ExerciseSubmission> ExerciseSubmissions { get; set; } = new List<ExerciseSubmission>();

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual ICollection<QuizAttempt> QuizAttempts { get; set; } = new List<QuizAttempt>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    public virtual ICollection<Report> ReportHandledByNavigations { get; set; } = new List<Report>();

    public virtual ICollection<Report> ReportReporters { get; set; } = new List<Report>();

    public virtual Role Role { get; set; } = null!;
}
