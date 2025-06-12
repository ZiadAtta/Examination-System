using Examination_System.Repository;
using Examination_System.Models;
using Examination_System.ViewModels;
using Examination_System.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Examination_System.DTOS;

namespace Examination_System.Services
{
    public class ExamQuestionService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ExamQuestion> _repository;
        public ExamQuestionService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new Repository<ExamQuestion>();
        }

        public GeneralResponse<IEnumerable<ExamQuestion>> Display()
        {
            var listExamQuestion = _repository.GetAll().Select(x => new ExamQuestion
            {
                ExamId = x.ExamId,
               QuestionId = x.QuestionId,
            }).ToList();

            return GeneralResponse<IEnumerable<ExamQuestion>>.Response(listExamQuestion, "The Data Returned Succeffully.", true);
        }
        public async Task<GeneralResponse<bool>> Create(CreateExamQuestionVM ExamQuestionVM)
        {
            try
            {
                // Check For Existence 
                bool exist = await _repository.GetAll().Where(
                    x => x.ExamId == ExamQuestionVM.ExamId &&
                    x.QuestionId == ExamQuestionVM.QuestionId
                  ).AnyAsync();

                if (exist)
                {
                    return GeneralResponse<bool>.Response(false, "This ExamQuestion Is Was Exist Before.", false, ErrorCode.Exist.ToString());
                }

                var ExamQuestion = _mapper.Map<ExamQuestion>(ExamQuestionVM);
                await _repository.AddAsync(ExamQuestion);
                await _repository.SaveChangesAsync();
                return GeneralResponse<bool>.Response(true, "The Data Saved Correct.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "The Is Internal Error. ", false, ErrorCode.InternalServerError.ToString() + "The Real Message" + ex.Message);
            }
        }

        public async Task<GeneralResponse<bool>> Update(UpdateExamQuestionVM examQuestionVM)
        {
            try
            {
                var examQuestion = await _repository.GetByIdAsync(examQuestionVM.Id);
                examQuestion.ExamId = examQuestionVM.ExamId;
                examQuestion.QuestionId = examQuestionVM.QuestionId;

                await _repository.SaveIncludeAsync(examQuestion, nameof(examQuestion.ExamId), nameof(examQuestion.QuestionId));
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
                var course = await _repository.GetAll().Where(x => x.Id == Id).AnyAsync();
                if (course)
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
    }
}
