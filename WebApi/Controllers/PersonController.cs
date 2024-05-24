using AutoMapper;
using EmployeeManager.Domain.Entities;
using EmployeeManager.Domain.Services;
using EmployeeManager.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManager.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AddPerson([FromBody] PersonDTO personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _personService.AddPerson(personDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{personId}/jobs")]
        public IActionResult AddJob(int personId, [FromBody] JobDTO jobDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _personService.AddJob(personId, jobDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllPersons()
        {
            var persons = _personService.GetAllPersons();
            return Ok(persons);
        }

        [HttpGet("company/{companyName}")]
        public IActionResult GetPersonsByCompanyName(string companyName)
        {
            var persons = _personService.GetPersonsByCompanyName(companyName);
            return Ok(persons);
        }

        [HttpGet("{personId}/jobs")]
        public IActionResult GetJobsByPersonAndDateRange(int personId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var jobs = _personService.GetJobsByPersonAndDateRange(personId, startDate, endDate);
            return Ok(jobs);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePerson(int id)
        {
            try
            {
                _personService.DeletePerson(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("all-with-current-jobs")]
        public ActionResult<IEnumerable<PersonWithCurrentJobsDTO>> GetAllPersonsWithCurrentJobs()
        {
            var result = _personService.GetAllPersonsWithCurrentJobs();
            return Ok(result);
        }
    }
}
