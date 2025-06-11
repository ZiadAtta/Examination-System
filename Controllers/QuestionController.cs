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
    public class QuestionController : ControllerBase
    {
        private readonly QuestionService _questionService;

        public QuestionController(IMapper mapper)
        {
            _questionService = new QuestionService(mapper);
        }

        [HttpGet]
        public GeneralResponse<IEnumerable<QuestionDTO>> Index()
        {
            return _questionService.Display();
        }

        [HttpPost]
        public async Task<GeneralResponse<bool>> Create(CreateQuestionVM questionVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<bool>.Response(false, "Validation failed", false, string.Join(" | ", errors));
            }

            return await _questionService.Create(questionVM);
        }

        [HttpPut]
        public async Task<GeneralResponse<bool>> Update(UpdateQuestionDTO questionVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<bool>.Response(false, "Validation failed", false, string.Join(" | ", errors));
            }

            return await _questionService.Update(questionVM);
        }

        [HttpDelete]
        public async Task<GeneralResponse<bool>> Delete(int questionId)
        {
            if (questionId <= 0)
            {
                return GeneralResponse<bool>.Response(false, "Invalid ID", false, "ID must be greater than 0");
            }

            return await _questionService.Delete(questionId);
        }
    }
}
