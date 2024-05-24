using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.Domain.Entities
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        public List<Job> Jobs { get; set; } = new List<Job>();
    }
}
