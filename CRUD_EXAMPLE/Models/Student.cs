using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace CRUD_EXAMPLE.Models
{
    public class Student
    {

        [Key]
        public int StudentID { get; set; }

       

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "First Name can only contain letters and spaces")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Last Name can only contain letters and spaces")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Range(1000000000, 9999999999, ErrorMessage = "Phone number must be 10 digits long")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(255, ErrorMessage = "Address cannot exceed 255 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters")]
        [RegularExpression(@"^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other")]
        public string Gender { get; set; }


        [Required(ErrorMessage = "Date of Birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dob { get; set; }


        [Required(ErrorMessage = "City is required")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces")]
        public string City { get; set; }

        
        [Required(ErrorMessage = "State is required")]
        [StringLength(100, ErrorMessage = "State cannot exceed 100 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "State can only contain letters and spaces")]
        public string State { get; set; }

        [Required(ErrorMessage = "PIN is required")]
        [Range(100000, 999999, ErrorMessage = "PIN must be a 6-digit number")]
        public long Pin { get; set; }


        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        [StringLength(254, ErrorMessage = "Email cannot exceed 254 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }


        public byte[] ImageData { get; set; }  // Base64 string for image

        public byte[] PdfData { get; set; }

        



    }
}