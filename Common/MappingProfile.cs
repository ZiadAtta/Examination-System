using AutoMapper;
using Examination_System.Models;
using Examination_System.ViewModels;
namespace Examination_System.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CreateCourseVM>().ReverseMap();
        }
    }
}
