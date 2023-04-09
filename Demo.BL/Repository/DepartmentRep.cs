using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Demo.BL.Repository
{
    public class DepartmentRep : IDepartmentRep
    {
        #region Fields

        private readonly DemoContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public DepartmentRep(DemoContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions

        public IEnumerable<Department> Get()
        {
            var data = GetDepartment();
            return data;
        }

        public Department GetById(int id)
        {
            var data = db.Department.Where(a => a.Id == id).FirstOrDefault();

            return data;
        }
        public void Create(Department obj)
        {
            db.Department.Add(obj);
            db.SaveChanges();
        }
        public void Edit(Department obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Department obj)
        {
            db.Department.Remove(obj);
            db.SaveChanges();

        }
        #endregion


        // ---------------- Refactor Method best practice to high performance ----------------
        private IEnumerable<Department> GetDepartment()
        {
            return db.Department.Select(a => a);
        }
    }
}
