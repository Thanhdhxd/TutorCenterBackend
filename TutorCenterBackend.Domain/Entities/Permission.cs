using System;
using System.Collections.Generic;

namespace TutorCenterBackend.Domain.Entities;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string Module { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
