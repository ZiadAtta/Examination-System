using AutoMapper;
using Examination_System.Common;
using Examination_System.DTOS;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly ExamService _examService;

        public ExamController(IMapper mapper)
        {
            _examService = new ExamService(mapper);
        }

        [HttpGet]
        public GeneralResponse<IEnumerable<ExamDTO>> Index()
        {
            return _examService.Display();
        }

        [HttpPost]
        public async Task<GeneralResponse<bool>> Create(CreateExamVM examVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<bool>.Response(
                    false,
                    "Model state is invalid.",
                    false,
                    string.Join(" | ", errors)
                );
            }

            return await _examService.Create(examVM);
        }

        [HttpPut]
        public async Task<GeneralResponse<bool>> Update(UpdateExamVM examVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<bool>.Response(
                    false,
                    "Model state is invalid.",
                    false,
                    string.Join(" | ", errors)
                );
            }

            return await _examService.Update(examVM);
        }

        [HttpDelete("{examId}")]
        public async Task<GeneralResponse<bool>> Delete(int examId)
        {
            if (examId <= 0)
            {
                return GeneralResponse<bool>.Response(
                    false,
                    "Invalid ID",
                    false,
                    "The Id must be greater than 0"
                );
            }

            return await _examService.Delete(examId);
        }
    }
}
