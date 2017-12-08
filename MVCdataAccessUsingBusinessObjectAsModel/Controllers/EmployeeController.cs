using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace MVCdataAccessUsingBusinessObjectAsModel.Controllers
{
    public class EmployeeController : Controller
    {
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            EmployeeBusinessLayer Bl = new EmployeeBusinessLayer();
            List<Employee> ems = Bl.Employees.ToList();
            return View(ems);
        }
        [HttpGet]
        public ActionResult Create()
        {
          
            return View();
        }

        //when the user wil submit the create employee
        //post action method with call
        [HttpPost]
        public ActionResult Create(Employee em)
        {
            EmployeeBusinessLayer Bl = new EmployeeBusinessLayer();
            Bl.AddNewEmployee(em);
            //will redirect the user to the index page
            return RedirectToAction("Index");
        }
        //controller for edit a employee
        //when user clicks edit --a get request will hit this controller
        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit_get(int Id)
        {
            EmployeeBusinessLayer bl = new EmployeeBusinessLayer();
            Employee emp = bl.Employees.Single(x => x.Id == Id);
            //return form to edit the employee data
            return View(emp);
        }
        //post action method to update the employee
        //
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit_post([Bind(Include="Id,FirstName,City")]Employee emp){
            // not giving access to user to change the last name
            //get the name from business layer
            EmployeeBusinessLayer bl= new EmployeeBusinessLayer();
            emp.LastName = bl.Employees.Single(x => x.Id == emp.Id).LastName;
            if (ModelState.IsValid)
            {
                //update the database
                bl.saveUpdatedEmployeeInfo(emp);
                return RedirectToAction("Index");
            }
            return View(emp);

        }

        //Deleting a Record using Pist
        [HttpPost]
        public ActionResult Delete(int Id) //it will bind automatically by parameter binding on submit
        {
            EmployeeBusinessLayer bl = new EmployeeBusinessLayer();
            bl.DeleteEmployee(Id);
            return RedirectToAction("Index");
        }
	}
}