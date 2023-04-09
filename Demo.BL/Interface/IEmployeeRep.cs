using Demo.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BL.Interface
{
    public interface IEmployeeRep
    {
        IEnumerable<Employee> Get();
        IEnumerable<Employee> SearchByName(string value);
        Employee GetById(int id);
        void Create(Employee obj);
        void Edit(Employee obj);
        void Delete(Employee obj);
    }
}
