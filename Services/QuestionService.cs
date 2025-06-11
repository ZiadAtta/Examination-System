using AutoMapper;
using Examination_System.Common;
using Examination_System.DTOS;
using Examination_System.Models;
using Examination_System.Repository;
using Examination_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Services
{
    public class QuestionService
    {
        private readonly IMapper _mapper;   
        private readonly IRepository<Question> _repository;
        public QuestionService(IMapper mapper)
        {
            _mapper = mapper;
            _repository = new Repository<Question>();
        }

        public GeneralResponse<IEnumerable<QuestionDTO>> Display()
        {
            var listQuestions= _repository.GetAll().Select(x => new QuestionDTO
            {
                Text = x.Text,
                Difficulty = (int)x.Difficulty,
                InstructorId = x.InstructorId,
                Questions = x.Choices.Select(
                    C => new ChoiceDTO(C.Text,C.IsCorrect)
                    ).ToList()
            }).ToList();

            return GeneralResponse<IEnumerable<QuestionDTO>>.Response(listQuestions, "The Data Returned Succeffully.", true);
        }
        public async Task<GeneralResponse<bool>> Create(CreateQuestionVM questionVM)
        {
            try
            {
                // Check For Existence 
                bool exist = await _repository.GetAll().Where(
                    x => x.Text == questionVM.Text &&
                    x.InstructorId == questionVM.InstructorId
                  ).AnyAsync();

                if (exist)
                {
                    return GeneralResponse<bool>.Response(false, "This Question Is Was Exist Before.", false, ErrorCode.Exist.ToString());
                }

                var question = _mapper.Map<Question>(questionVM);
                await _repository.AddAsync(question);
                await _repository.SaveChangesAsync();
                return GeneralResponse<bool>.Response(true, "The Data Saved Correct.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "The Is Internal Error. ", false, ErrorCode.InternalServerError.ToString() + "The Real Message" + ex.Message);
            }
        }

        public async Task<GeneralResponse<bool>> Update(UpdateQuestionDTO questionVM)
        {
            try
            {
                var question = await _repository.GetByIdAsync(questionVM.Id);
                question.Text = questionVM.Text;
                question.Difficulty = questionVM.Difficulty;
                question.InstructorId = questionVM.InstructorId;

                await _repository.SaveIncludeAsync(question, nameof(question.Text), nameof(question.Difficulty), nameof(question.InstructorId));
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
