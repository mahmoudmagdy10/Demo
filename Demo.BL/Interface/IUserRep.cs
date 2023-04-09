using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface IUserRep
    {
        IEnumerable<ApplicationUser> Get();
        Task<ApplicationUser> GetById(string id);
        Task<ApplicationUser> Edit(ApplicationUser obj);
        Task<ApplicationUser> Delete(ApplicationUser obj);
    }
}
