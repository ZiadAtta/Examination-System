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

        [HttpPost("CreateWithQuestions")]
        public async Task<GeneralResponse<bool>> CreateWithQuestions(CreateExamWithQuestionsVM vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return GeneralResponse<bool>.Response(false, "Validation error", false, string.Join(" | ", errors));
            }

            return await _examService.CreateExamWithQuestions(vm);
        }


        [HttpGet("Results/{examId}")]
        public async Task<GeneralResponse<IEnumerable<StudentExamResultDTO>>> GetAllResults(int examId)
        {
            if (examId <= 0)
            {
                return GeneralResponse<IEnumerable<StudentExamResultDTO>>.Response(null, "Invalid Exam ID", false, ErrorCode.Invalide.ToString());
            }

            return await _examService.GetAllStudentResultsForExam(examId);
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
        [HttpPost("GetStudentResult")]
        public async Task<GeneralResponse<ExamResultDTO>> GetStudentResult(int examId)
        {
            if(examId < 1)
            {
                return GeneralResponse<ExamResultDTO>.Response();
            }
            return await _examService.GetStudentResult(examId);
        }

       [HttpPost("Take")]
        public async Task<GeneralResponse<ExamDTO>> TakeExam([FromBody] StudentExamVM request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<ExamDTO>.Response(
                    null,
                    "Model state is invalid.",
                    false,
                    string.Join(" | ", errors)
                );
            }

            return await _examService.TakeExam(request);
        }
        [HttpPost("Submit")]    
        public async Task<GeneralResponse<ExamResultDTO>> SubmitExam([FromBody] SubmitExamVM request)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return GeneralResponse<ExamResultDTO>.Response(
                    null,
                    "Model state is invalid.",
                    false,
                    string.Join(" | ", errors)
                );
            }

            return await _examService.SubmitExamAsync(request);
        }
    }
}
