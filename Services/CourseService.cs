using Examination_System.Repository;
using Examination_System.Models;
using Examination_System.ViewModels;
using Examination_System.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Examination_System.DTOS;
namespace Examination_System.Services
{
    public class CourseService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Course> _repository;
        private readonly IRepository<StudentCourse> _studentCourse;
        public CourseService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new Repository<Course>();
            _studentCourse = new Repository<StudentCourse>();
        }

        public GeneralResponse<IEnumerable<CourseDTO>> Display()
        {
            var listCourse = _repository.GetAll().Select(x => new CourseDTO
            (
                x.Id,
                x.Name,
                x.Description,
                x.Hours
            )).ToList();

            return GeneralResponse<IEnumerable<CourseDTO>>.Response(listCourse,"The Data Returned Succeffully.",true);
        }
        public async Task<GeneralResponse<bool>> Create(CreateCourseVM courseVM)
        {
            try
            {
                // Check For Existence 
                bool exist = await _repository.GetAll().Where(x => x.Name == courseVM.Name).AnyAsync();

                if(exist)
                {
                    return GeneralResponse<bool>.Response(false, "This Course Is Was Exist Before.",false,ErrorCode.Exist.ToString());
                }

                var course = _mapper.Map<Course>(courseVM);
                await _repository.AddAsync(course);
                await _repository.SaveChangesAsync();
                return GeneralResponse<bool>.Response(true, "The Data Saved Correct.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "The Is Internal Error. ", false, ErrorCode.InternalServerError.ToString() + "The Real Message" + ex.Message);
            }
        }

        public async Task<GeneralResponse<bool>> Update(UpdateCourseVM courseVM)
        {
            try
            {
                var course = await _repository.GetByIdAsync(courseVM.CourseId);
                course.Hours = courseVM.Hours;
                course.Name = courseVM.Name;
                course.Description = courseVM.Description;
                course.InstructorId = courseVM.InstructorId;

                await _repository.SaveIncludeAsync(course, nameof(course.Hours), nameof(course.Name), nameof(course.Description), nameof(course.InstructorId));
                await _repository.SaveChangesAsync();

                return GeneralResponse<bool>.Response(true, "The Data Saved Correct.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "The Is Exception Error", false, ex.ToString());
            }
        }

        public async Task<GeneralResponse<bool>> Delete(int Id)
        {
            if (Id < 0)
            {
                return GeneralResponse<bool>.Response(false, "The Id Is Not Valid.");
            }
            try
            {
                var course = await _repository.GetAll().Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (course != null)
                {
                    return GeneralResponse<bool>.Response(false, "The Id Is Not Valid.");
                }

                await _repository.SoftDeleteAsync(Id);
                await _repository.SaveChangesAsync();

                return GeneralResponse<bool>.Response(true, "The Data Deleted Succeffully.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "The Is Exception Error", false, ex.ToString());
            }

        }

        public async Task<GeneralResponse<bool>> Enroll(int studentId, int courseId)
        {
            if (studentId <= 0 || courseId <= 0)
            {
                return GeneralResponse<bool>.Response(false, "The Student Id Or Course Id Is Not Valid.", false, ErrorCode.InvalidInput.ToString());
            }
            try
            {
                bool exist =await  _studentCourse.GetAll().Where(x => x.StudentId == studentId && x.CourseId == courseId).AnyAsync();

                if (exist)
                {
                    return GeneralResponse<bool>.Response(false, "The Student Is Already Enrolled In This Course.", false, ErrorCode.Exist.ToString());
                }

                var studentCourse = new StudentCourse
                {
                    StudentId = studentId,
                    CourseId = courseId
                };
                await _studentCourse.AddAsync(studentCourse);
                await _studentCourse.SaveChangesAsync();
                return GeneralResponse<bool>.Response(true, "The Student Enrolled Successfully.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "An error occurred while enrolling the student.", false, ErrorCode.InternalServerError.ToString() + " The Real Message: " + ex.Message);
            }
        }

    }
}