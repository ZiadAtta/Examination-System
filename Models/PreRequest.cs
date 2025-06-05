using System.ComponentModel.DataAnnotations.Schema;

namespace Examination_System.Models
{
    public class PreRequest:BaseEntity
    {
        [ForeignKey("MainCourse")]
        public int MainCourseID { get; set; }

        [ForeignKey("PreRequestCourse")]
        public int PreRequestCourseID { get; set; }

        public Course MainCourse { get; set; }
        public Course PreRequestCourse { get; set; }

        public bool IsMandatory { get; set; }
    }
}
