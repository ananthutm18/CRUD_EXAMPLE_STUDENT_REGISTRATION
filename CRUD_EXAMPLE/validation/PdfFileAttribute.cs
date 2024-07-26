using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;

namespace CRUD_EXAMPLE.validation
{
    public class PdfFileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var fileData = value as byte[];

            if (fileData == null || fileData.Length < 5)
            {
                return new ValidationResult("The file is not a valid PDF or is too small.");
            }

            // Check if the file starts with %PDF-
            string header = Encoding.ASCII.GetString(fileData, 0, 5);
            if (header != "%PDF-")
            {
                return new ValidationResult("The file is not a valid PDF.");
            }

            return ValidationResult.Success;
        }
    }
}