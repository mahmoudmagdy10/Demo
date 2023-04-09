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
    public class DistrictRep : IDistrictRep
    {
        #region Fields

        private readonly DemoContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public DistrictRep(DemoContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions

        public IEnumerable<District> Get(Expression<Func<District, bool>> filter = null)
        {
            if (filter == null)
            {
                return db.District.Select(a => a);
            }
            else
            {
                return db.District.Where(filter);
            }
        }

        public District GetById(int id)
        {
            var data = db.District.Where(a => a.Id == id).FirstOrDefault();

            return data;
        }

        #endregion
    }
}
