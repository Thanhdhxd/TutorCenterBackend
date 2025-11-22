using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Medium
{
    public int MediaId { get; set; }

    public string Disk { get; set; } = null!;

    public string? Bucket { get; set; }

    public string ObjectKey { get; set; } = null!;

    public string? MimeType { get; set; }

    public long? SizeBytes { get; set; }

    public string Visibility { get; set; } = null!;

    public int? UploadedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<AimessageMedia> AimessageMedia { get; set; } = new List<AimessageMedia>();

    public virtual ICollection<ClassroomChatMessageMedia> ClassroomChatMessageMedia { get; set; } = new List<ClassroomChatMessageMedia>();

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();

    public virtual ICollection<ExerciseSubmission> ExerciseSubmissions { get; set; } = new List<ExerciseSubmission>();

    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

    public virtual ICollection<Lecture> Lectures { get; set; } = new List<Lecture>();

    public virtual ICollection<QuestionGroupMedia> QuestionGroupMedia { get; set; } = new List<QuestionGroupMedia>();

    public virtual ICollection<QuestionMedia> QuestionMedia { get; set; } = new List<QuestionMedia>();

    public virtual ICollection<QuestionOptionMedia> QuestionOptionMedia { get; set; } = new List<QuestionOptionMedia>();

    public virtual User? UploadedByNavigation { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
