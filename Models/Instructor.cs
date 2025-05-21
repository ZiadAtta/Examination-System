namespace Examination_System.Models
{
    public class Instructor:User
    {
        public ICollection<Course>? CoursesCreated { get; set; }
    }
}
