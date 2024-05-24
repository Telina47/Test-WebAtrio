using EmployeeManager.Domain.Entities;
using EmployeeManager.WebApi.DTOs;

namespace EmployeeManager.Domain.Services
{
    public interface IPersonService
    {
        void AddPerson(PersonDTO personDto);
        void AddJob(int personId, JobDTO jobDto);
        List<PersonDTO> GetAllPersons();
        List<PersonDTO> GetPersonsByCompanyName(string companyName);
        List<JobDTO> GetJobsByPersonAndDateRange(int personId, DateTime startDate, DateTime endDate);
        void DeletePerson(int id);
        IEnumerable<PersonWithCurrentJobsDTO> GetAllPersonsWithCurrentJobs();
    }
}
