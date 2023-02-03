using Microsoft.EntityFrameworkCore;

namespace CodeFirstMigration.Models
{
    [Keyless]
    public class EmployeeDetails
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
