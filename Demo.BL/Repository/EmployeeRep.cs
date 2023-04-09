using AutoMapper;
using Demo.BL.Interface;
using Demo.DAL.Database;
using Demo.DAL.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Repository
{
    public class EmployeeRep : IEmployeeRep
    {
        #region Fields

        private readonly DemoContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public EmployeeRep(DemoContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        #endregion

        #region Actions

        public IEnumerable<Employee> Get()
        {
            var data = GetEmployees();
            return data;
        }

        public IEnumerable<Employee> SearchByName(string value)
        {
            return db.Employee.Include("Department").Where(a => a.Name.Contains(value));
        }
        public Employee GetById(int id)
        {
            var data = db.Employee.Where(a => a.Id == id).FirstOrDefault();

            return data;
        }
        public void Create(Employee obj)
        {
            db.Employee.Add(obj);
            db.SaveChanges();
        }
        public void Edit(Employee obj)
        {
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(Employee obj)
        {
            db.Employee.Remove(obj);
            db.SaveChanges();

        }
        #endregion
        private IEnumerable<Employee> GetEmployees()
        {
            return db.Employee.Include("Department").Select(a => a);
        }

    }
}
