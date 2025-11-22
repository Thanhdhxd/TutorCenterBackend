using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class ClassroomStudent
{
    public int ClassroomId { get; set; }

    public int StudentId { get; set; }

    public DateTime JoinedAt { get; set; }

    public bool HasPaid { get; set; }

    public DateTime? PaidAt { get; set; }

    public int? PaymentTransactionId { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual PaymentTransaction? PaymentTransaction { get; set; }

    public virtual User Student { get; set; } = null!;
}
