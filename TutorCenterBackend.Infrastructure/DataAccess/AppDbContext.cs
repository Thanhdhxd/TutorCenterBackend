using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TutorCenterBackend.Domain.Entities;

namespace TutorCenterBackend.Infrastructure.DataAccess;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Aiagent> Aiagents { get; set; }

    public virtual DbSet<Aiconversation> Aiconversations { get; set; }

    public virtual DbSet<Aimessage> Aimessages { get; set; }

    public virtual DbSet<AimessageMedia> AimessageMedias { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<ClassroomChatMessage> ClassroomChatMessages { get; set; }

    public virtual DbSet<ClassroomChatMessageMedia> ClassroomChatMessageMedias { get; set; }

    public virtual DbSet<ClassroomStudent> ClassroomStudents { get; set; }

    public virtual DbSet<Exercise> Exercises { get; set; }

    public virtual DbSet<ExerciseSubmission> ExerciseSubmissions { get; set; }

    public virtual DbSet<JoinRequest> JoinRequests { get; set; }

    public virtual DbSet<Lecture> Lectures { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<Medium> Media { get; set; }

    public virtual DbSet<OtpRecord> OtpRecords { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<QuestionGroup> QuestionGroups { get; set; }

    public virtual DbSet<QuestionGroupMedia> QuestionGroupMedias { get; set; }

    public virtual DbSet<QuestionMedia> QuestionMedias { get; set; }

    public virtual DbSet<QuestionOption> QuestionOptions { get; set; }

    public virtual DbSet<QuestionOptionMedia> QuestionOptionMedias { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizAnswer> QuizAnswers { get; set; }

    public virtual DbSet<QuizAttempt> QuizAttempts { get; set; }

    public virtual DbSet<QuizSection> QuizSections { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=TutorCenterDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.ActivityLogId).HasName("PK__Activity__19A9B7AFF0EA7C88");

            entity.HasIndex(e => new { e.Action, e.CreatedAt }, "IX_ActivityLogs_Action_Created").IsDescending(false, true);

            entity.HasIndex(e => new { e.EntityType, e.EntityId }, "IX_ActivityLogs_Entity");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "IX_ActivityLogs_User_Created").IsDescending(false, true);

            entity.Property(e => e.Action).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.EntityType).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActivityLogs_User");
        });

        modelBuilder.Entity<Aiagent>(entity =>
        {
            entity.HasKey(e => e.AgentId).HasName("PK__AIAgents__9AC3BFF16A50F7C6");

            entity.ToTable("AIAgents");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Aiagents)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_AIAgents_CreatedBy");
        });

        modelBuilder.Entity<Aiconversation>(entity =>
        {
            entity.HasKey(e => e.ConversationId).HasName("PK__AIConver__C050D877D822DBA7");

            entity.ToTable("AIConversations");

            entity.HasIndex(e => new { e.OwnerUserId, e.UpdatedAt }, "IX_AIConversations_Owner_Updated").IsDescending(false, true);

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Agent).WithMany(p => p.Aiconversations)
                .HasForeignKey(d => d.AgentId)
                .HasConstraintName("FK_AIConversations_Agent");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Aiconversations)
                .HasForeignKey(d => d.ClassroomId)
                .HasConstraintName("FK_AIConversations_Classroom");

            entity.HasOne(d => d.OwnerUser).WithMany(p => p.Aiconversations)
                .HasForeignKey(d => d.OwnerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AIConversations_Owner");
        });

        modelBuilder.Entity<Aimessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__AIMessag__C87C0C9C9D6A6A0A");

            entity.ToTable("AIMessages");

            entity.HasIndex(e => new { e.ConversationId, e.CreatedAt }, "IX_AIMessages_Conv_Created");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.SenderRole)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.Conversation).WithMany(p => p.Aimessages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AIMessages_Conversation");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_AIMessages_Parent");
        });

        modelBuilder.Entity<AimessageMedia>(entity =>
        {
            entity.HasKey(e => e.MessageMediaId).HasName("PK__AIMessag__6D5926268E55A0C2");

            entity.ToTable("AIMessageMedias");

            entity.HasIndex(e => e.MessageId, "IX_AIMessageMedias_Message_Order");

            entity.HasIndex(e => new { e.MessageId, e.MediaId }, "UQ_AIMessageMedias").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Purpose)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Media).WithMany(p => p.AimessageMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AIMessageMedias_Media");

            entity.HasOne(d => d.Message).WithMany(p => p.AimessageMedia)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AIMessageMedias_Message");
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasKey(e => e.AnnouncementId).HasName("PK__Announce__9DE44574F65C77E3");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.ClassroomId)
                .HasConstraintName("FK_Announcements_Classroom");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Announcements)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Announcements_CreatedBy");
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.HasKey(e => e.ClassroomId).HasName("PK__Classroo__11618EAA2A5DF64D");

            entity.HasIndex(e => e.DeletedAt, "IX_Classrooms_DeletedAt").HasFilter("([DeletedAt] IS NOT NULL)");

            entity.HasIndex(e => e.TutorId, "IX_Classrooms_TutorId");

            entity.HasIndex(e => e.Name, "UQ__Classroo__737584F67E4EB7A8").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CoverMedia).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.CoverMediaId)
                .HasConstraintName("FK_Classrooms_CoverMedia");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ClassroomCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classrooms_CreatedBy");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.ClassroomDeletedByNavigations)
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Classrooms_DeletedBy");

            entity.HasOne(d => d.Tutor).WithMany(p => p.ClassroomTutors)
                .HasForeignKey(d => d.TutorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classrooms_Tutor");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.ClassroomUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classrooms_UpdatedBy");
        });

        modelBuilder.Entity<ClassroomChatMessage>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Classroo__C87C0C9CD4DB5AA7");

            entity.HasIndex(e => new { e.ClassroomId, e.SentAt }, "IX_ClassroomChat_Class_SentAt").IsDescending(false, true);

            entity.Property(e => e.SentAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Classroom).WithMany(p => p.ClassroomChatMessages)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classroom__Class__7EF6D905");

            entity.HasOne(d => d.Sender).WithMany(p => p.ClassroomChatMessages)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classroom__Sende__7FEAFD3E");
        });

        modelBuilder.Entity<ClassroomChatMessageMedia>(entity =>
        {
            entity.HasKey(e => e.ChatMediaId).HasName("PK__Classroo__FED30275C62087DE");

            entity.HasIndex(e => new { e.MessageId, e.MediaId }, "UQ_CCMMed").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Media).WithMany(p => p.ClassroomChatMessageMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classroom__Media__0D44F85C");

            entity.HasOne(d => d.Message).WithMany(p => p.ClassroomChatMessageMedia)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Classroom__Messa__0C50D423");
        });

        modelBuilder.Entity<ClassroomStudent>(entity =>
        {
            entity.HasKey(e => new { e.ClassroomId, e.StudentId }).HasName("PK__Classroo__924DDC1385B2C4A0");

            entity.HasIndex(e => new { e.ClassroomId, e.HasPaid }, "IX_ClassroomStudents_HasPaid").HasFilter("([DeletedAt] IS NULL)");

            entity.HasIndex(e => new { e.ClassroomId, e.StudentId }, "IX_ClassroomStudents_Unpaid").HasFilter("([HasPaid]=(0) AND [DeletedAt] IS NULL)");

            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.JoinedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.PaidAt).HasPrecision(0);

            entity.HasOne(d => d.Classroom).WithMany(p => p.ClassroomStudents)
                .HasForeignKey(d => d.ClassroomId)
                .HasConstraintName("FK_ClassroomStudents_Classroom");

            entity.HasOne(d => d.PaymentTransaction).WithMany(p => p.ClassroomStudents)
                .HasForeignKey(d => d.PaymentTransactionId)
                .HasConstraintName("FK_ClassroomStudents_PaymentTransaction");

            entity.HasOne(d => d.Student).WithMany(p => p.ClassroomStudents)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ClassroomStudents_Student");
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.HasKey(e => e.ExerciseId).HasName("PK__Exercise__A074AD2F46555F12");

            entity.HasIndex(e => e.CreatedBy, "IX_Exercises_CreatedBy");

            entity.HasIndex(e => e.DeletedAt, "IX_Exercises_DeletedAt").HasFilter("([DeletedAt] IS NOT NULL)");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.AttachMedia).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.AttachMediaId)
                .HasConstraintName("FK_Exercises_AttachMedia");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Exercises)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Exercises_CreatedBy");
        });

        modelBuilder.Entity<ExerciseSubmission>(entity =>
        {
            entity.HasKey(e => e.ExerciseSubmissionId).HasName("PK__Exercise__1344B271D76078C8");

            entity.HasIndex(e => e.ExerciseId, "IX_Submissions_Exercise");

            entity.HasIndex(e => e.LessonId, "IX_Submissions_Lesson");

            entity.HasIndex(e => new { e.LessonId, e.StudentId }, "UQ_Submission_OnePerStudentPerLesson").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(1000);
            entity.Property(e => e.GradedAt).HasPrecision(0);
            entity.Property(e => e.Score).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.SubmittedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Exercise).WithMany(p => p.ExerciseSubmissions)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseSubmissions_Exercise");

            entity.HasOne(d => d.Lesson).WithMany(p => p.ExerciseSubmissions)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseSubmissions_Lesson");

            entity.HasOne(d => d.Media).WithMany(p => p.ExerciseSubmissions)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseSubmissions_Media");

            entity.HasOne(d => d.Student).WithMany(p => p.ExerciseSubmissions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExerciseSubmissions_Student");
        });

        modelBuilder.Entity<JoinRequest>(entity =>
        {
            entity.HasKey(e => e.JoinRequestId).HasName("PK__JoinRequ__257393AA42A7BBF6");

            entity.HasIndex(e => new { e.ClassroomId, e.Status }, "IX_JoinRequests_Status");

            entity.HasIndex(e => new { e.ClassroomId, e.StudentId }, "UQ_JoinRequests_Classroom_Student").IsUnique();

            entity.Property(e => e.HandledAt).HasPrecision(0);
            entity.Property(e => e.RequestedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Classroom).WithMany(p => p.JoinRequests)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JoinRequests_Classroom");

            entity.HasOne(d => d.Student).WithMany(p => p.JoinRequests)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JoinRequests_Student");
        });

        modelBuilder.Entity<Lecture>(entity =>
        {
            entity.HasKey(e => e.LectureId).HasName("PK__Lectures__B739F6BF74BB16A5");

            entity.HasIndex(e => e.DeletedAt, "IX_Lectures_DeletedAt").HasFilter("([DeletedAt] IS NOT NULL)");

            entity.HasIndex(e => e.ParentId, "IX_Lectures_ParentId").HasFilter("([ParentId] IS NOT NULL)");

            entity.HasIndex(e => e.UploadedBy, "IX_Lectures_UploadedBy");

            entity.Property(e => e.Content).HasMaxLength(2000);
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.UploadedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Media).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_Lectures_Media");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Lectures_Parent");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Lectures)
                .HasForeignKey(d => d.UploadedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lectures_UploadedBy");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.LessonId).HasName("PK__Lessons__B084ACD0BD68C885");

            entity.HasIndex(e => e.ClassroomId, "IX_Lessons_Classroom");

            entity.HasIndex(e => e.ExerciseId, "IX_Lessons_Exercise");

            entity.HasIndex(e => e.LectureId, "IX_Lessons_Lecture");

            entity.HasIndex(e => e.QuizId, "IX_Lessons_Quiz");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.ExerciseDueAt).HasPrecision(0);
            entity.Property(e => e.LessonType).HasMaxLength(20);
            entity.Property(e => e.QuizEndAt).HasPrecision(0);
            entity.Property(e => e.QuizStartAt).HasPrecision(0);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.Classroom).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ClassroomId)
                .HasConstraintName("FK_Lessons_Classroom");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Lessons_CreatedBy");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.ExerciseId)
                .HasConstraintName("FK_Lessons_Exercise");

            entity.HasOne(d => d.Lecture).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.LectureId)
                .HasConstraintName("FK_Lessons_Lecture");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK_Lessons_Quiz");
        });

        modelBuilder.Entity<Medium>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__Media__B2C2B5CF8FEBE58B");

            entity.HasIndex(e => e.DeletedAt, "IX_Media_DeletedAt").HasFilter("([DeletedAt] IS NOT NULL)");

            entity.HasIndex(e => e.UploadedBy, "IX_Media_UploadedBy").HasFilter("([UploadedBy] IS NOT NULL)");

            entity.HasIndex(e => new { e.Bucket, e.ObjectKey }, "UQ_Media_Bucket_Key").IsUnique();

            entity.Property(e => e.Bucket).HasMaxLength(100);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.Disk).HasMaxLength(30);
            entity.Property(e => e.MimeType).HasMaxLength(100);
            entity.Property(e => e.ObjectKey).HasMaxLength(700);
            entity.Property(e => e.Visibility)
                .HasMaxLength(10)
                .HasDefaultValue("private");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.Media)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK_Media_UploadedBy");
        });

        modelBuilder.Entity<OtpRecord>(entity =>
        {
            entity.HasKey(e => e.OtpRecordId).HasName("PK__OtpRecor__5899710B4DB18758");

            entity.HasIndex(e => new { e.Email, e.OtpCode, e.CodeType, e.ExpiresAt }, "IX_OtpRecords_Email_Code").IsDescending(false, false, false, true);

            entity.Property(e => e.CodeType).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.ExpiresAt).HasPrecision(0);
            entity.Property(e => e.OtpCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__PaymentT__55433A6B87679F56");

            entity.HasIndex(e => new { e.ClassroomId, e.StudentId, e.CreatedAt }, "IX_PaymentTransactions_Class_Stu_Created").IsDescending(false, false, true);

            entity.HasIndex(e => new { e.Status, e.PaidAt }, "IX_PaymentTransactions_Status_Paid").HasFilter("([Status]='paid')");

            entity.HasIndex(e => e.OrderCode, "UQ_PaymentTransactions_Order").IsUnique();

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Method)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.OrderCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaidAt).HasPrecision(0);
            entity.Property(e => e.ProviderTxnId)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending");

            entity.HasOne(d => d.Classroom).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentTransactions_Classroom");

            entity.HasOne(d => d.Student).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentTransactions_Student");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB2FD37C515A");

            entity.HasIndex(e => e.PermissionName, "UQ__Permissi__0FFDA35721FFF8A0").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.Method).HasMaxLength(10);
            entity.Property(e => e.Module).HasMaxLength(50);
            entity.Property(e => e.Path).HasMaxLength(200);
            entity.Property(e => e.PermissionName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.QuestionId).HasName("PK__Question__0DC06FAC5AE15BCE");

            entity.Property(e => e.Content).HasMaxLength(1000);
            entity.Property(e => e.Explanation).HasMaxLength(1000);
            entity.Property(e => e.Points).HasDefaultValue(1.0);
            entity.Property(e => e.QuestionType)
                .HasMaxLength(20)
                .HasDefaultValue("single_choice");

            entity.HasOne(d => d.Group).WithMany(p => p.Questions)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_Questions_Group");

            entity.HasOne(d => d.Quiz).WithMany(p => p.Questions)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK_Questions_Quiz");

            entity.HasOne(d => d.Section).WithMany(p => p.Questions)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK_Questions_Section");
        });

        modelBuilder.Entity<QuestionGroup>(entity =>
        {
            entity.HasKey(e => e.QuestionGroupId).HasName("PK__Question__17C46189C0DF1A99");

            entity.Property(e => e.IntroText).HasMaxLength(1000);
            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuestionGroups)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK_QuestionGroups_Quiz");

            entity.HasOne(d => d.Section).WithMany(p => p.QuestionGroups)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK_QuestionGroups_Section");
        });

        modelBuilder.Entity<QuestionGroupMedia>(entity =>
        {
            entity.HasKey(e => e.QuestionGroupMediaId).HasName("PK__Question__C76DE72FE75D75FD");

            entity.HasIndex(e => new { e.GroupId, e.MediaId }, "UQ_QuestionGroupMed").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Group).WithMany(p => p.QuestionGroupMedia)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK_QuestionGroupMedias_Group");

            entity.HasOne(d => d.Media).WithMany(p => p.QuestionGroupMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionGroupMedias_Media");
        });

        modelBuilder.Entity<QuestionMedia>(entity =>
        {
            entity.HasKey(e => e.QuestionMediaId).HasName("PK__Question__52F2AC3CE4DBF3B9");

            entity.HasIndex(e => new { e.QuestionId, e.MediaId }, "UQ_QuestionMed").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Media).WithMany(p => p.QuestionMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionMedias_Media");

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionMedia)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionMedias_Question");
        });

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.HasKey(e => e.QuestionOptionId).HasName("PK__Question__064FB4DCBA05B1E8");

            entity.Property(e => e.Content).HasMaxLength(500);

            entity.HasOne(d => d.Question).WithMany(p => p.QuestionOptions)
                .HasForeignKey(d => d.QuestionId)
                .HasConstraintName("FK_QuestionOptions_Question");
        });

        modelBuilder.Entity<QuestionOptionMedia>(entity =>
        {
            entity.HasKey(e => e.QuestionOptionMediaId).HasName("PK__Question__18F34E8DE6534047");

            entity.HasIndex(e => new { e.OptionId, e.MediaId }, "UQ_QuestionOptionMed").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Media).WithMany(p => p.QuestionOptionMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuestionOptionMedias_Media");

            entity.HasOne(d => d.Option).WithMany(p => p.QuestionOptionMedia)
                .HasForeignKey(d => d.OptionId)
                .HasConstraintName("FK_QuestionOptionMedias_Option");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quizzes__8B42AE8E804C4CD7");

            entity.HasIndex(e => e.CreatedBy, "IX_Quizzes_CreatedBy");

            entity.HasIndex(e => e.DeletedAt, "IX_Quizzes_DeletedAt").HasFilter("([DeletedAt] IS NOT NULL)");

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.GradingMethod)
                .HasMaxLength(20)
                .HasDefaultValue("first");
            entity.Property(e => e.MaxAttempts).HasDefaultValue(1);
            entity.Property(e => e.ShuffleOptions).HasDefaultValue(true);
            entity.Property(e => e.ShuffleQuestions).HasDefaultValue(true);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Quizzes_CreatedBy");
        });

        modelBuilder.Entity<QuizAnswer>(entity =>
        {
            entity.HasKey(e => new { e.AttemptId, e.QuestionId, e.OptionId }).HasName("PK__QuizAnsw__A754A9BD476E17C4");

            entity.HasOne(d => d.Attempt).WithMany(p => p.QuizAnswers)
                .HasForeignKey(d => d.AttemptId)
                .HasConstraintName("FK_QuizAnswers_Attempt");

            entity.HasOne(d => d.Option).WithMany(p => p.QuizAnswers)
                .HasForeignKey(d => d.OptionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAnswers_Option");

            entity.HasOne(d => d.Question).WithMany(p => p.QuizAnswers)
                .HasForeignKey(d => d.QuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAnswers_Question");
        });

        modelBuilder.Entity<QuizAttempt>(entity =>
        {
            entity.HasKey(e => e.QuizAttemptId).HasName("PK__QuizAtte__F39FDCEDB84F3D6C");

            entity.HasIndex(e => e.LessonId, "IX_QuizAttempts_Lesson");

            entity.HasIndex(e => new { e.LessonId, e.StudentId, e.SubmittedAt }, "IX_QuizAttempts_Lesson_Student_Submitted").IsDescending(false, false, true);

            entity.HasIndex(e => e.QuizId, "IX_QuizAttempts_Quiz");

            entity.HasIndex(e => e.Status, "IX_QuizAttempts_Status");

            entity.HasIndex(e => e.StudentId, "IX_QuizAttempts_Student");

            entity.Property(e => e.ScoreRaw).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ScoreScaled10).HasColumnType("decimal(4, 2)");
            entity.Property(e => e.StartedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("in_progress");
            entity.Property(e => e.SubmittedAt).HasPrecision(0);

            entity.HasOne(d => d.Lesson).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.LessonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAttempts_Lesson");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAttempts_Quiz");

            entity.HasOne(d => d.Student).WithMany(p => p.QuizAttempts)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAttempts_Student");
        });

        modelBuilder.Entity<QuizSection>(entity =>
        {
            entity.HasKey(e => e.QuizSectionId).HasName("PK__QuizSect__4BE61F097BAD43F4");

            entity.Property(e => e.Title).HasMaxLength(200);

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizSections)
                .HasForeignKey(d => d.QuizId)
                .HasConstraintName("FK_QuizSections_Quiz");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Token).HasName("PK__RefreshT__1EB4F816A76DD4CB");

            entity.HasIndex(e => e.ExpiresAt, "IX_RefreshTokens_ExpiresAt");

            entity.Property(e => e.Token).HasMaxLength(1000);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.ExpiresAt).HasPrecision(0);

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshTokens_User");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD4805802AFFB9");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.HandledAt).HasPrecision(0);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.Reason).HasMaxLength(500);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("pending");
            entity.Property(e => e.TargetType)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.HandledByNavigation).WithMany(p => p.ReportHandledByNavigations)
                .HasForeignKey(d => d.HandledBy)
                .HasConstraintName("FK__Reports__Handled__0697FACD");

            entity.HasOne(d => d.Reporter).WithMany(p => p.ReportReporters)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__Reporte__05A3D694");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A66FFA442");

            entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61608B634A70").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.DeletedAt).HasPrecision(0);
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK_RolePermissions_Permission"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_RolePermissions_Role"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__RolePerm__6400A1A8D2AB57AA");
                        j.ToTable("RolePermissions");
                    });
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C6EE3F96C");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E385E555B26").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C187B92C").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.RoleId).HasDefaultValue(1);
            entity.Property(e => e.UpdatedAt)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.AvatarMedia).WithMany(p => p.Users)
                .HasForeignKey(d => d.AvatarMediaId)
                .HasConstraintName("FK_Users_AvatarMedia");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Users_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
