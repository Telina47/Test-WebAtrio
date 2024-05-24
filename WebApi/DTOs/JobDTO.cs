namespace EmployeeManager.WebApi.DTOs
{
    public class JobDTO
    {
        public string CompanyName { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
