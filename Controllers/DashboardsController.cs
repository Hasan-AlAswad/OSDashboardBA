using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSDashboardBA.DB;
using OSDashboardBA.Models;
using System.Security.Claims;

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
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllDashboards()
        {
            // get list of dashboards exist
            var dashs = _context.Dashboards.Where(st => st.IsDeleted != true).ToList();
            if (dashs.Count() > 0)
            {
                // new obj of dto to show data 
                var dashes = new List<DashGetDTO>();

                foreach (Dashboard dash in dashs)
                {
                    dashes.Add(new DashGetDTO()
                    {
                        Name = dash.Name,
                        Layers = dash.Layers,
                        Widgets = dash.Widgets,
                        CreatedOn = dash.CreatedOn,
                    });
                }
                return Ok(dashes);
            }
            else
            {
                return Ok("no Dashboards yet!");
            }
        }

        // SHORTLY add one Dashboard "json in body" fn()
        [HttpPost]
        public IActionResult AddDashboard(DashPostDTO newDsb)   //  int userId
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var dsp = new Dashboard()
            {
                Name = newDsb.Name,
                Widgets = newDsb.Widgets,
                Layers = newDsb.Layers,
                UserId = userId
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
        [HttpPut("{dashId:int}")]
        public IActionResult EditDashboard(int dashId, DashPostDTO newDs)
        {
            // access wanted dep with sent id
            var oldDs = _context.Dashboards.FirstOrDefault(ds => ds.Id == dashId);
            if (oldDs != null)
            {
                oldDs.Name = newDs.Name;
                oldDs.Widgets = newDs.Widgets;
                oldDs.Layers = newDs.Layers;
                _context.SaveChanges();
                return Ok($"Dashboard: {oldDs.Name} was edited");
            }
            else
            {
                return Ok($"no Dashboard with id: {dashId}");
            }
        }

        // delete fn()
        [HttpDelete("{dashId:int}")]
        public IActionResult DeleteDashboard(int dashId)
        {
            // access dep with given id 
            var ds = _context.Dashboards.FirstOrDefault(ds => ds.Id == dashId);
            if (ds != null)
            {
                ds.IsDeleted = true;
                _context.SaveChanges();
                return Ok($"Dashboard with id: {dashId} was deleted!");
            }
            else
            {
                return Ok($"no Dashboard with id: {dashId}");
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

        // get 
        [HttpGet("{dashId:int}")]
        public IActionResult getDashById(int dashId)             /*List<Dashboard>*/
        {

            var dash = _context.Dashboards.FirstOrDefault(ds => ds.Id == dashId);
            // new obj of dto to show data 
            if (dash!=null)
            {
            var dashe = new DashGetDTO() {
                Name = dash.Name,
                Layers = dash.Layers,
                Widgets = dash.Widgets,
                CreatedOn = dash.CreatedOn,
            };
                return Ok(dashe);
            }
            else
            {
                return Ok("no Dashboards with the with such a part of its name !");
            }
        }
    }
}
