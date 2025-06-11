using AutoMapper;
using Examination_System.Common;
using Examination_System.DTOS;
using Examination_System.Models;
using Examination_System.Repository;
using Examination_System.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Examination_System.Services
{
    public class ChoiceService
    {
        private readonly IRepository<Choice> _repository;
        private readonly IMapper _mapper;
        public  ChoiceService(IMapper mapper)
        {
            _repository = new Repository<Choice>();
            _mapper = mapper;
        }

        public GeneralResponse<IEnumerable<ChoiceDTO>> Display()
        {
            var listCourse = _repository.GetAll().Select(x => new ChoiceDTO
            (
               x.Text,
               x.IsCorrect
            )).ToList();

            return GeneralResponse<IEnumerable<ChoiceDTO>>.Response(listCourse, "The Data Returned Succeffully.", true);
        }
        public async Task<GeneralResponse<bool>> Create(CreateChoiceVM choiceVM)
        {
            try
            {
                // Check For Existence 
                bool exist = await _repository.GetAll().Where(
                    x => x.Text == choiceVM.Text&&
                    x.QuestionId == choiceVM.QuestionId
                  ).AnyAsync();

                if (exist)
                {
                    return GeneralResponse<bool>.Response(false, "This Choice Is Was Exist Before.", false, ErrorCode.Exist.ToString());
                }

                var choice = _mapper.Map<Choice>(choiceVM);
                await _repository.AddAsync(choice);
                await _repository.SaveChangesAsync();
                return GeneralResponse<bool>.Response(true, "The Data Saved Correct.", true);
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Response(false, "The Is Internal Error. ", false, ErrorCode.InternalServerError.ToString() + "The Real Message" + ex.Message);
            }
        }

        public async Task<GeneralResponse<bool>> Update(UpdateChoiceVM choiceVM)
        {
            try
            {
                var choice = await _repository.GetByIdAsync(choiceVM.Id);
                choice.Id = choiceVM.Id;
                choice.Text = choiceVM.Text;
                choice.IsCorrect = choiceVM.IsCorrect;
                choice.QuestionId = choiceVM.QuestionId;

                await _repository.SaveIncludeAsync(choice,nameof(choice.Id),nameof(choiceVM.QuestionId),nameof(choice.IsCorrect),nameof(choice.IsCorrect));
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
