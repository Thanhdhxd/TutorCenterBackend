-- Seed default roles
USE TutorCenterDb;
GO

-- Insert default roles if they don't exist
IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'student')
BEGIN
    INSERT INTO Roles (RoleName, [Description], CreatedAt, UpdatedAt)
    VALUES ('student', 'Student role with basic access', SYSUTCDATETIME(), SYSUTCDATETIME());
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'tutor')
BEGIN
    INSERT INTO Roles (RoleName, [Description], CreatedAt, UpdatedAt)
    VALUES ('tutor', 'Tutor role with classroom management access', SYSUTCDATETIME(), SYSUTCDATETIME());
END

IF NOT EXISTS (SELECT 1 FROM Roles WHERE RoleName = 'admin')
BEGIN
    INSERT INTO Roles (RoleName, [Description], CreatedAt, UpdatedAt)
    VALUES ('admin', 'Administrator role with full system access', SYSUTCDATETIME(), SYSUTCDATETIME());
END

GO

-- Verify inserted roles
SELECT * FROM Roles WHERE DeletedAt IS NULL;
GO
