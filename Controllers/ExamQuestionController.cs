using AutoMapper;
using Examination_System.Common;
using Examination_System.Models;
using Examination_System.Services;
using Examination_System.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Examination_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamQuestionController : ControllerBase
    {
        private readonly ExamQuestionService _examQuestionService;

        public ExamQuestionController(IMapper mapper)
        {
            _examQuestionService = new ExamQuestionService(mapper);
        }

        [HttpGet]
        public GeneralResponse<IEnumerable<ExamQuestion>> Index()
        {
            return _examQuestionService.Display();
        }

        [HttpPost]
        public async Task<GeneralResponse<bool>> Create(CreateExamQuestionVM examQuestionVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<bool>.Response(false, "Model state is invalid.", false, string.Join(" | ", errors));
            }

            return await _examQuestionService.Create(examQuestionVM);
        }

        [HttpPut]
        public async Task<GeneralResponse<bool>> Update(UpdateExamQuestionVM examQuestionVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<bool>.Response(false, "Model state is invalid.", false, string.Join(" | ", errors));
            }

            return await _examQuestionService.Update(examQuestionVM);
        }

        [HttpDelete("{id}")]
        public async Task<GeneralResponse<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return GeneralResponse<bool>.Response(false, "Invalid ID", false, "ID must be greater than 0");
            }

            return await _examQuestionService.Delete(id);
        }
    }
}
