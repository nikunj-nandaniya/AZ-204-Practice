using System.ComponentModel.DataAnnotations;

namespace CodeFirstMigration.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set; }
        public int DepartmentId { get; set; }
    }
}
