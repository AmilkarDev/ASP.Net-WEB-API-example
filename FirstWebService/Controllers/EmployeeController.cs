using FirstWebService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace FirstWebService.Controllers
{

    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        Employee empp = new Employee() { FirstName = "abdullah", LastName = "fekri" };
        [HttpGet]
        public List<Employee> Get()
        {
            EmpDbHandle dbhandle = new EmpDbHandle();
            List<Employee> li = new List<Employee>();
            li = dbhandle.GetAll();
            return li;
        }

        [HttpPost]
        [ResponseType(typeof(Employee))]
        public IHttpActionResult Post(Employee emp)
        {
            EmpDbHandle dbhandle = new EmpDbHandle();
            try
            {
                dbhandle.AddEmp(emp);
                return Ok();
            }
            catch(Exception ex)
            {
                return Content(HttpStatusCode.BadRequest,"Post Failure");
            }
        }
        [HttpPut]
        public IHttpActionResult Put(int id,Employee emp)
        {
            EmpDbHandle dbhandle = new EmpDbHandle();
            try
            {
                dbhandle.UpdateEmp(emp,id);
                return Ok();
            }
            catch(Exception ex)
            {
               
                return Content(HttpStatusCode.BadRequest, "Update Failure");
            }
        }
        
        [HttpDelete]
        [ResponseType(typeof(Employee))]
        public HttpResponseMessage delete(int id)
        {
            EmpDbHandle dbHandle = new EmpDbHandle();
            try
            {
                dbHandle.DeleteEmp(id);
                return Request.CreateResponse(HttpStatusCode.OK);

            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
             //   return Content(HttpStatusCode.BadRequest, "Delete Failure");
            }
        }
        //private bool EmployeeExists(int id)
        //{
        //    return db.Employees.Count(e => e.EmployeeID == id) > 0;
        //}
    }
}
