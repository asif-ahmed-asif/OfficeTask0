using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Designation
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DepartmentId { get; set; }

        //Navigation Properties
        public Department? Department { get; set; }
        public ICollection<Employee>? Employee { get; set; }
    }
}
