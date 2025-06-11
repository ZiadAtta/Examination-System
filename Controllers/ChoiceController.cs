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
    public class ChoiceController : ControllerBase
    {
        private readonly ChoiceService _choiceService;
        public ChoiceController(IMapper mapper)
        {
            _choiceService = new ChoiceService(mapper);
        }

        [HttpGet]
        public GeneralResponse<IEnumerable<ChoiceDTO>> Index()
        {
            return _choiceService.Display();
        }
        [HttpPost]
        public async Task<GeneralResponse<bool>> Create(CreateChoiceVM choiceVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .ToList();
                return GeneralResponse<bool>.Response(false, "Model state is invalid.", false, errors.ToString());
            }
            return await _choiceService.Create(choiceVM);
        }

        [HttpPut]
        public async Task<GeneralResponse<bool>> Update(UpdateChoiceVM choiceVM)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .ToList();
                return GeneralResponse<bool>.Response(false, "Model state is invalid.", false, errors.ToString());
            }
            return await _choiceService.Update(choiceVM);
        }
        [HttpDelete]
        public async Task<GeneralResponse<bool>> Delete(int choiceId)
        {
            if (choiceId <= 0)
            {
                return GeneralResponse<bool>.Response(false, "The ID IS Not Valid. ", false, "The Id Is Less Than 1.");
            }
            return await _choiceService.Delete(choiceId);
        }

    }
}
