using EmployeeManager.Domain.Entities;
using EmployeeManager.WebApi.DTOs;
using AutoMapper;

namespace EmployeeManager.WebApi.Profile
{
    public class MappingProfile : AutoMapper.Profile
    {
        public MappingProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Job, JobDTO>().ReverseMap();
            CreateMap<Person, PersonWithCurrentJobsDTO>()
              .ForMember(dest => dest.Age, opt => opt.MapFrom(src => CalculateAge(src.DateOfBirth)))
              .ForMember(dest => dest.CurrentJobs, opt => opt.MapFrom(src => src.Jobs.Where(j => !j.EndDate.HasValue || j.EndDate > DateTime.Now).ToList()));

        }
        private int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
