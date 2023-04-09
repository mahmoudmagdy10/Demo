using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Demo.DAL.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace Demo.BL.Models
{
    public class EmployeeVM
    {
        public EmployeeVM()
        {
            CreationDate = DateTime.Now;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, ErrorMessage = "Max Length Is 50")]
        [MinLength(3, ErrorMessage = "Min Length Is 3")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email Is Required")]
        [EmailAddress(ErrorMessage = "Email Isn't Valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Salary Is Required")]
        [Range(9000, 20000, ErrorMessage = "Salary Range between 20000 and 9000")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "HireDate Is Required")]
        public DateTime HireDate { get; set; }

        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        //[RegularExpression("[0-9]{2,5}-[a-zA-Z]{2,5}-[a-zA-Z]{2,5}-[a-zA-Z]{2,5}", ErrorMessage = "Like This : 12-Street-City-Country")]
        public string Address { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }

        public string CvName { get; set; }
        public string PhotoName { get; set; }
        public IFormFile Cv { get; set; }
        public IFormFile Photo { get; set; }


    }
}
