using AutoMapper;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Ports;
using EmployeeManager.WebApi.DTOs;

namespace EmployeeManager.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository;
            _mapper = mapper;
        }

        public void AddPerson(PersonDTO personDto)
        {
            var person = _mapper.Map<Person>(personDto);
            if ((DateTime.Now - person.DateOfBirth).TotalDays / 365 > 150)
            {
                throw new ArgumentException("Person must be less than 150 years old.");
            }
            _personRepository.Add(person);
        }

        public void AddJob(int personId, JobDTO jobDto)
        {
            var person = _personRepository.GetById(personId);
            if (person == null)
            {
                throw new ArgumentException("Person not found.");
            }
            var job = _mapper.Map<Job>(jobDto);
            person.Jobs.Add(job);
            _personRepository.Update(person);
        }

        public List<PersonDTO> GetAllPersons()
        {
            var persons = _personRepository.GetAll()
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToList();
            return _mapper.Map<List<PersonDTO>>(persons);
        }

        public List<PersonDTO> GetPersonsByCompanyName(string companyName)
        {
            var persons = _personRepository.GetAll()
                .Where(p => p.Jobs.Any(j => j.CompanyName == companyName))
                .ToList();
            return _mapper.Map<List<PersonDTO>>(persons);
        }

        public List<JobDTO> GetJobsByPersonAndDateRange(int personId, DateTime startDate, DateTime endDate)
        {
            var person = _personRepository.GetById(personId);
            if (person == null)
            {
                throw new ArgumentException("Person not found.");
            }
            var jobs = person.Jobs
                .Where(j => j.StartDate >= startDate && (j.EndDate == null || j.EndDate <= endDate))
                .ToList();
            return _mapper.Map<List<JobDTO>>(jobs);
        }

        public void DeletePerson(int id)
        {
            _personRepository.Delete(id);
        }
        public IEnumerable<PersonWithCurrentJobsDTO> GetAllPersonsWithCurrentJobs()
        {
            var persons = _personRepository.GetAll();
            var personWithJobsDto = persons
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .Select(p => new PersonWithCurrentJobsDTO
                {
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Age = CalculateAge(p.DateOfBirth),
                    CurrentJobs = p.Jobs
                        .Where(j => !j.EndDate.HasValue || j.EndDate > DateTime.Now)
                        .Select(j => new JobDTO
                        {
                            CompanyName = j.CompanyName,
                            Position = j.Position,
                            StartDate = j.StartDate,
                            EndDate = j.EndDate
                        }).ToList()
                });

            return personWithJobsDto;
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
