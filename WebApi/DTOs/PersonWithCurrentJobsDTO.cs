namespace EmployeeManager.WebApi.DTOs
{
    public class PersonWithCurrentJobsDTO
    {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int Age { get; set; }
            public List<JobDTO> CurrentJobs { get; set; }
    }
}
