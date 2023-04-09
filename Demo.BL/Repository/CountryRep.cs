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
    public class CountryRep : ICountryRep
    {
        #region Fields

        private readonly DemoContext db;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public CountryRep(DemoContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        #endregion

        #region Actions

        public IEnumerable<Country> Get()
        {
            var data = GetCountry();
            return data;
        }

        public Country GetById(int id)
        {
            var data = db.Country.Where(a => a.Id == id).FirstOrDefault();

            return data;
        }
        #endregion


        // ---------------- Refactor Method best practice to high performance ----------------
        private IEnumerable<Country> GetCountry()
        {
            return db.Country.Select(a => a);
        }
    }
}
