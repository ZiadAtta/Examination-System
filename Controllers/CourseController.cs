using Examination_System.ViewModels;
using Examination_System.Services;
using Microsoft.AspNetCore.Mvc;
using Examination_System.Common;
using AutoMapper;
using Examination_System.DTOS;

namespace Examination_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseService _courseService;
        public CourseController(IMapper mapper)
        {
            _courseService = new CourseService(mapper);
        }
        [HttpGet]
        public GeneralResponse<IEnumerable<CourseDTO>> Index()
        {
           return  _courseService.Display();
        }
        [HttpPost]
        public async Task<GeneralResponse<bool>> Create(CreateCourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .ToList();
                return GeneralResponse<bool>.Response(false, "Model state is invalid.", false, errors.ToString());
            }
           return await _courseService.Create(courseVM);
        }

        [HttpPut]
        public async Task<GeneralResponse<bool>> Update(UpdateCourseVM courseVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .ToList();
                return GeneralResponse<bool>.Response(false, "Model state is invalid.", false, errors.ToString());
            }
            return await _courseService.Update(courseVM);
        }
        [HttpDelete]
        public async Task<GeneralResponse<bool>> Delete(int courseId)
        {
            if (courseId <= 0)
            {
                return GeneralResponse<bool>.Response(false, "The ID IS Not Valid. ", false, "The Id Is Less Than 1.");
            }
            return await _courseService.Delete(courseId);
        }

        [HttpPost("Enroll")]
        public async Task<GeneralResponse<bool>> Enroll(int studentId ,int courseId)
        {
            if (courseId <= 0 && studentId <= 0)
            {
                return GeneralResponse<bool>.Response(false, "The ID IS Not Valid. ", false, "The Id Is Less Than 1.");
            }
            return await _courseService.Enroll(studentId,courseId);
        }
    }
}
