using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Classroom
{
    public int ClassroomId { get; set; }

    public int TutorId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int? CoverMediaId { get; set; }

    public bool IsArchived { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int CreatedBy { get; set; }

    public int UpdatedBy { get; set; }

    public int? DeletedBy { get; set; }

    public virtual ICollection<Aiconversation> Aiconversations { get; set; } = new List<Aiconversation>();

    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();

    public virtual ICollection<ClassroomChatMessage> ClassroomChatMessages { get; set; } = new List<ClassroomChatMessage>();

    public virtual ICollection<ClassroomStudent> ClassroomStudents { get; set; } = new List<ClassroomStudent>();

    public virtual Medium? CoverMedia { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual User? DeletedByNavigation { get; set; }

    public virtual ICollection<JoinRequest> JoinRequests { get; set; } = new List<JoinRequest>();

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();

    public virtual User Tutor { get; set; } = null!;

    public virtual User UpdatedByNavigation { get; set; } = null!;
}
