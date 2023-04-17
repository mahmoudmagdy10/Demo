using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Models
{
    public class ChatVM
    {
        public List<IdentityUser> Users { get; set; }
        public IdentityUser MyUsers { get; set; }
    }
}
