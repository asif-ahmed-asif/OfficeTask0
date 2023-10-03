using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }

        //Navigation Properties
        public ICollection<Employee>? Employee { get; set; }
        public ICollection<Designation>? Designation { get; set; }
    }
}
