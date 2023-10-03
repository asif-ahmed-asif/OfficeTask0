using AspNetCoreHero.ToastNotification.Abstractions;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly DataContext _db;
        private readonly INotyfService _toastNotification;

        public EmployeeController(DataContext db, INotyfService toastNotification)
        {
            _db = db;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var employees = await _db.Employee
                .Include("Department")
                .Include("Designation")
                .ToListAsync();
            return View(employees);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _db.Department.Select(x => new SelectListItem { 
                Value = x.Id.ToString(), Text = x.Name}).ToListAsync();

            return View();
        }

        public async Task<IActionResult> GetDesgnations(int id)
        {
            var designations = await _db.Designation
                .Where(d => d.DepartmentId == id)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }).ToListAsync();

            return Json(designations);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            if(ModelState.IsValid)
            {
                _db.Employee.Add(employee);
                await _db.SaveChangesAsync();
                _toastNotification.Success("Employee Succesfully Added!");
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _db.Employee.FindAsync(id);

            ViewBag.Departments = await _db.Department.Select(x => new SelectListItem { 
                Value = x.Id.ToString(), Text = x.Name}).ToListAsync();
            
            ViewBag.Designations = await _db.Designation
                .Where(d => d.DepartmentId == employee.DepartmentId)
                .Select(x => new SelectListItem { 
                Value = x.Id.ToString(), Text = x.Name}).ToListAsync();

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if(employee.EmpId == 0)
            {
                return View();
            }
            if (ModelState.IsValid)
            {
                _db.Employee.Update(employee);
                await _db.SaveChangesAsync();
                _toastNotification.Success("Employee Succesfully Updated!");
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _db.Employee.FindAsync(id);
            if (employee is not null)
            {
                _db.Employee.Remove(employee);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
