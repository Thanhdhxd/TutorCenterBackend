namespace TutorCenterBackend.Domain.Constants;

/// <summary>
/// Optional constants for permission names - provides IntelliSense support
/// These are NOT required, you can use string literals directly: [RequirePermission("user.view")]
/// Permission definitions come from database, not these constants
/// </summary>
public static class PermissionNames
{
    // User permissions
    public const string UserView = "user.view";
    public const string UserCreate = "user.create";
    public const string UserEdit = "user.edit";
    public const string UserDelete = "user.delete";

    // Classroom permissions
    public const string ClassroomView = "classroom.view";
    public const string ClassroomCreate = "classroom.create";
    public const string ClassroomEdit = "classroom.edit";
    public const string ClassroomDelete = "classroom.delete";
    public const string ClassroomManageStudents = "classroom.manage_students";

    // Lesson permissions
    public const string LessonView = "lesson.view";
    public const string LessonCreate = "lesson.create";
    public const string LessonEdit = "lesson.edit";
    public const string LessonDelete = "lesson.delete";

    // Exercise permissions
    public const string ExerciseView = "exercise.view";
    public const string ExerciseCreate = "exercise.create";
    public const string ExerciseEdit = "exercise.edit";
    public const string ExerciseDelete = "exercise.delete";
    public const string ExerciseGrade = "exercise.grade";

    // Quiz permissions
    public const string QuizView = "quiz.view";
    public const string QuizCreate = "quiz.create";
    public const string QuizEdit = "quiz.edit";
    public const string QuizDelete = "quiz.delete";
    public const string QuizGrade = "quiz.grade";

    // Lecture permissions
    public const string LectureView = "lecture.view";
    public const string LectureCreate = "lecture.create";
    public const string LectureEdit = "lecture.edit";
    public const string LectureDelete = "lecture.delete";

    // Media permissions
    public const string MediaUpload = "media.upload";
    public const string MediaDelete = "media.delete";

    // Report permissions
    public const string ReportView = "report.view";
    public const string ReportCreate = "report.create";
    public const string ReportProcess = "report.process";

    // Payment permissions
    public const string PaymentView = "payment.view";
    public const string PaymentProcess = "payment.process";

    // Role & Permission management
    public const string RoleView = "role.view";
    public const string RoleManage = "role.manage";
    public const string PermissionView = "permission.view";
    public const string PermissionManage = "permission.manage";

    // System administration
    public const string SystemSettings = "system.settings";
    public const string ActivityLogView = "activity_log.view";
}
