namespace EmployeeManager.WebApi.DTOs
{
    public class PersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<JobDTO> Jobs { get; set; } = new List<JobDTO>();
    }
}
