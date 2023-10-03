using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        [Key]
        public int EmpId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Mobile { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [Required]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }

        //Navigation Properties
        public Department? Department { get; set; }
        public Designation? Designation { get; set; }
    }
}
