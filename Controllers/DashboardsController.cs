using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSDashboardBA.DB;
using OSDashboardBA.Models;

namespace OSDashboardBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        // prop 
        public AppDbContext _context { get; set; }

        // constructor 
        public DashboardsController(AppDbContext context)
        {
            _context = context;
        }

        // actions ------------------------------------

        // get 
        [Route("getAllDashboards")]     // route
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllDashboards()
        {
            // get list of dashboards exist
            var dashs = _context.Dashboards.Where(st => st.IsDeleted != true).ToList();

            // new obj of dto to show data 
            var dashes = new List<DashGetDTO>();

            foreach (Dashboard dash in dashs)
            {
                dashes.Add(new DashGetDTO()
                {
                    Name = dash.Name,
                    Users = dash.Users,
                    Layers = dash.Layers,
                    LayersCount = dash.Layers.Count(),
                    CreatedDate =dash.CreatedDate,
                });
            }

            if (dashes.Count() > 0)
            {
                return Ok(dashes);
            }
            else
            {
                return Ok("no Dashboards yet!");
            }
        }

        // SHORTLY add one Dashboard "json in body" fn()
        [Route("AddDashboard")]
        [HttpPost]
        public IActionResult AddDashboard(DashPostDTO newDsb)   //  int userId
        {
            var dsp = new Dashboard()
            {
                Name = newDsb.Name,
                
            };

            _context.Dashboards.Add(dsp);   // add new db to general dbs list not to db of a user 
            _context.SaveChanges();
            return Ok($"Dashboard: {newDsb.Name} was added successfully!");

            //var userD = _context.UserDashs.FirstOrDefault(us => us.Id == userId);
            //if (userD != null)
            //{
            //    userD.Dashboards.Add(dsp);
            //    _context.SaveChanges();
            //    return Ok($"Dashboard: {newDsb.Name} was added successfully!");
            //}
            //else
            //{
            //    return Ok($"no user with id: {userId}");
            //}
                
                
        }

        // edit DepName by id fn() // NEED CHECK !
        [Route("EditDashboard")]
        [HttpPut]
        public IActionResult EditDashboard(int id, DashPostDTO newDs)
        {
            // access wanted dep with sent id
            var oldDs = _context.Dashboards.FirstOrDefault(ds => ds.Id == id);
            if (oldDs != null)
            {
                oldDs.Name = newDs.Name;
                

                _context.SaveChanges();
                return Ok($"Dashboard: {oldDs.Name} was edited");
            }
            else
            {
                return Ok($"no Dashboard with id: {id}");
            }
        }

        // delete fn()
        [Route("DeleteDashboard")]
        [HttpDelete]
        public IActionResult DeleteDashboard(int id)
        {
            // access dep with given id 
            var ds = _context.Dashboards.FirstOrDefault(ds => ds.Id == id);
            if (ds != null)
            {
                ds.IsDeleted = true;
                _context.SaveChanges();
                return Ok($"Dashboard with id: {id} was deleted!");
            }
            else
            {
                return Ok($"no Dashboard with id: {id}");
            }

        }

        // drop fn()
        [Route("DropDashboard")]
        [HttpDelete]
        public IActionResult DropDashboard(int id)
        {
            // access dep with given id 
            var ds = _context.Dashboards.FirstOrDefault(dsh => dsh.Id == id);
            if (ds != null)
            {
                _context.Remove(ds);
                _context.SaveChanges();
                return Ok($"Dashboard with id: {id} was deleted!");
            }
            else
            {
                return Ok($"no Dashboard with id: {id}");
            }

        }

        // search by part of dash name 
        [Route("SearchByName")]
        [HttpGet]
        public IActionResult SearchByName(string pName)             /*List<Dashboard>*/
        {
            var pNameE = pName.ToUpper();

            //return _context.Dashboards.Where(ly => ly.Name.Contains(pNameE)).ToList();
            
            var dashs = _context.Dashboards.Where(ds => ds.Name.Contains(pNameE)).ToList();
            // new obj of dto to show data 
            var dashes = new List<DashGetDTO>();

            foreach (Dashboard d in dashs)
            {
                dashes.Add(new DashGetDTO()
                {
                    Name = d.Name,
                    Users = d.Users,
                    Layers = d.Layers,
                    LayersCount = d.Layers.Count(),
                    CreatedDate = d.CreatedDate,
                });
            }

            if (dashes.Count() > 0)
            {
                return Ok(dashes);
            }
            else
            {
                return Ok("no Dashboards with the with such a part of its name !");
            }

        }




    }
}
