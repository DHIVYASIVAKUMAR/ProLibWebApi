using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiLibrary.Models
{
	public class ServiceStudents
	{
        [Key]
        public int serviceStudentId { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter name"), MaxLength(30)]
        [Display(Name = " Name ")]
        public string serviceStudentName { get; set; }

        [Required(ErrorMessage = "Please enter branch name")]
        [Display(Name = "Branch")]
        public string serviceStudentBranch { get; set; }

        [Required(ErrorMessage = "Please select Gender")]
        [Display(Name = "Gender")]
        public GenderType serviceGender { get; set; }


        [Required(ErrorMessage = "please enter phone number")]
        [Display(Name = " Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[6-9]\d{9}$", ErrorMessage = "enter valid phone number")]
        public string servicePhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        [Display(Name = "Address")]
        [StringLength(150)]
        public string serviceAddress { get; set; }

        [Required(ErrorMessage = "Please enter city")]
        [Display(Name = "City")]
        [StringLength(50)]
        public string serviceCity { get; set; }

        [Required(ErrorMessage = "Please enter Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string serviceEmail { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [StringLength(10, ErrorMessage = "Must be between 5 and 10 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string servicePassword { get; set; }

    }
    public enum GenderType
    {
        Female,
        Male,
        Genderless
    }
}