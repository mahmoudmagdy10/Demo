using AutoMapper;
using Demo.BL.Interface;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class UserRep : IUserRep
    {
        #region Fields
        private readonly UserManager<ApplicationUser> userManager;
        #endregion

        #region Ctor
        public UserRep(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }
        #endregion

        #region Actions

        public IEnumerable<ApplicationUser> Get()
        {
            var data = userManager.Users;
            return data;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var data = await userManager.FindByIdAsync(id);
            return data;
        }

        public async Task<ApplicationUser> Edit(ApplicationUser obj)
        {
            var data = await userManager.FindByIdAsync(obj.Id);

            data.UserName = obj.UserName;
            data.Email = obj.Email;

            await userManager.UpdateAsync(data);
            return data;
        }

        public async Task<ApplicationUser> Delete(ApplicationUser obj)
        {
            var user = await userManager.FindByIdAsync(obj.Id);

            await userManager.DeleteAsync(user);
            return obj;
        }


        #endregion


    }
}
