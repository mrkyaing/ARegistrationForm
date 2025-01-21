using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RegistrationProcessForm.Models {
    [Table("Register")]
    public class RegisterModel {
        [Required(ErrorMessage = "Please Enter the Employee ID")]
        [MaxLength(5)]
        public required string EmployeeID { get; set; }

        [Required(ErrorMessage = "Please Enter the Employee Name")]
        public required string Name { get; set; }

        [MaxLength(11)]
        public string? Phone { get; set; }
    }
}
