using Examination_System.Models;
using Examination_System.Repository;

namespace Examination_System.Services
{
    public class StudentService
    {
        private readonly IRepository<Student> _repository;
        public StudentService()
        {
            _repository = new Repository<Student>();
        }

    }
}
