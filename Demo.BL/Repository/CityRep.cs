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
using System.Linq.Expressions;

namespace Demo.BL.Repository
{
    public class CityRep : ICityRep
    {
        #region Fields

        private readonly DemoContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public CityRep(DemoContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions

        public IEnumerable<City> Get(Expression<Func<City, bool>> filter = null)
        {
            if (filter == null)
            {
                return db.City.Select(a => a);
            }
            else
            {
                return db.City.Where(filter);
            }
        }

        public City GetById(int id)
        {
            var data = db.City.Where(a => a.Id == id).FirstOrDefault();

            return data;
        }
        #endregion


    }
}
