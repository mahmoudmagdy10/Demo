using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.BL.Repository;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Controllers
{
    public class DepartmentController : Controller
    {
        #region Fields

        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public DepartmentController(IDepartmentRep department, IMapper mapper)
        {
            this.department = department;
            this.mapper = mapper;
        }
        #endregion

        #region Actions

        public IActionResult Index()
        {
            var data = department.Get();
            var model = mapper.Map<IEnumerable<DepartmentVM>>(data);

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = mapper.Map<Department>(obj);
                    department.Create(model);
                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                return View(obj);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentVM obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = mapper.Map<Department>(obj);
                    department.Edit(model);

                    return RedirectToAction("Index");
                }
                return View(obj);
            }
            catch (Exception ex)
            {
                return View(obj);
            }
        }

        public IActionResult Details(int id)
        {
            var data = department.GetById(id);
            var model = mapper.Map<DepartmentVM>(data);
            return View(model);
        }

        public IActionResult Delete(DepartmentVM obj)
        {
            var model = mapper.Map<Department>(obj);
            department.Delete(model);
            return RedirectToAction("Index");
        }
        #endregion


    }
}
