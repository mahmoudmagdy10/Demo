using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class DepartmentVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [StringLength(50, ErrorMessage = "Max Length Is 50")]
        [MinLength(2, ErrorMessage = "Min Length Is 2")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Is Required")]
        [StringLength(10, ErrorMessage = "Max Length Is 10")]
        [MinLength(3, ErrorMessage = "Min Length Is 3")]
        public string Code { get; set; }

    }
}
