using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Demo.DAL.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Demo.Controllers
{
    public class EmployeeController : Controller
    {
        #region Fields
        private readonly IEmployeeRep employee;
        private readonly ICityRep city;
        private readonly IDistrictRep district;
        private readonly IDepartmentRep department;
        private readonly IMapper mapper;

        #endregion

        #region Ctor
        public EmployeeController(IEmployeeRep employee, ICityRep city, IDistrictRep district, IDepartmentRep department, IMapper mapper)
        {
            this.employee = employee;
            this.city = city;
            this.district = district;
            this.department = department;
            this.mapper = mapper;
        }
        #endregion

        #region Actions

        public IActionResult Index()
        {
            var data = employee.Get();
            var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeVM model)
        {
            try
            {
                if (true)
                {
                    #region Upload Files

                    model.PhotoName = FileUpload.UploadFile("/wwwroot/Files/Imgs", model.Photo);
                    model.CvName = FileUpload.UploadFile("/wwwroot/Files/Docs", model.Cv);
                    #endregion

                    var data = mapper.Map<Employee>(model);
                    employee.Create(data);
                    return RedirectToAction("Index");
                }

                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(model);
            }
            catch (Exception ex)
            {

                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                ViewBag.CreateMessage = "Failed To Create";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeVM obj)
        {
            try
            {
                if (true)
                {
                    var model = mapper.Map<Employee>(obj);
                    employee.Edit(model);

                    return RedirectToAction("Index");
                }
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(obj);
            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(obj);
            }
        }

        public IActionResult Details(int id)
        {
            var data = employee.GetById(id);
            var model = mapper.Map<EmployeeVM>(data);
            ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name", model.DepartmentId);
            return View(model);
        }

        public IActionResult Delete(EmployeeVM obj)
        {
            try
            {
                var model = mapper.Map<Employee>(obj);
                FileUpload.RemoveFile("/wwwroot/Files/Imgs/", model.PhotoName);
                FileUpload.RemoveFile("/wwwroot/Files/Docs/", model.CvName);
                employee.Delete(model);

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                ViewBag.DepartmentList = new SelectList(department.Get(), "Id", "Name");
                return View(obj);

            }
        }
        #endregion

        #region Ajax Requests

        [HttpPost]
        public JsonResult GetCityByCountryId(int CountryId)
        {
            var data = city.Get(a => a.CountryId == CountryId);
            var model = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(model);
        }

        [HttpPost]
        public JsonResult GetDistrictByCityId(int CityId)
        {
            var data = district.Get(a => a.CityId == CityId);
            var model = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(model);
        }


        #endregion

    }
}
