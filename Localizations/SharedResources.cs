using Microsoft.Extensions.Localization;

namespace Examination_System.Localizations
{
    public class SharedResources
    {
        private readonly IStringLocalizer<SharedResources> _localizer;

        public SharedResources(IStringLocalizer<SharedResources> localizer)
        {
            _localizer = localizer;
        }

        // Course related messages
        public string CourseNotFound(int id) => _localizer["CourseNotFound", id];
        public string CourseNameRequired => _localizer["CourseNameRequired"];
        public string CourseCannotBeNull => _localizer["CourseCannotBeNull"];
        public string CourseUpdateRequestCannotBeNull => _localizer["CourseUpdateRequestCannotBeNull"];
        public string CourseAlreadyExists(string name) => _localizer["CourseAlreadyExists", name];
        public string CourseHasActiveExams => _localizer["CourseHasActiveExams"];
        public string InvalidCourseId => _localizer["InvalidCourseId"];

        // Instructor related messages 
        public string InstructorNotFound(int id) => _localizer["InstructorNotFound", id];

        // Student related messages
        public string StudentNotFound(int id) => _localizer["StudentNotFound", id];

        // Exam related messages
        public string ExamNotFound(int id) => _localizer["ExamNotFound", id];

        // Question related messages
        public string QuestionNotFound(int id) => _localizer["QuestionNotFound", id];

        // Authorization messages
        public string AccessDenied => _localizer["AccessDenied"];

        // System messages
        public string UnexpectedError => _localizer["UnexpectedError"];
        public string ValidationError(string details) => _localizer["ValidationError", details];
    }
} 