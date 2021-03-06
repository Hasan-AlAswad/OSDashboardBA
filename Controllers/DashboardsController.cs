using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSDashboardBA.Models;
using System.Security.Claims;
using OSDashboardBA.Auth;
using OSDashboardBA.DB;
using Microsoft.EntityFrameworkCore;

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
                        Id = dash.Id,
                        Name = dash.Name,
                        CreatedOn = dash.CreatedOn,
                        // Layers = dash.Layers,
                        // Widgets = dash.Widgets,
                    });
                }
                return Ok(dashes);
            }
            else
            {
                return Ok("No Dashboards yet!");
            }
        }

        // get by id 
        [HttpGet("{dashId}")]
        public IActionResult GetDashById(int dashId)
        {
            var dash = _context.Dashboards.Include(dash => dash.Layers).FirstOrDefault(ds => ds.Id == dashId);
            // new obj of dto to show data 
            if (dash != null)
            {
                var dashe = new DashGetDTO()
                {
                    Id = dash.Id,
                    Name = dash.Name,
                    Widgets = dash.Widgets,
                    CreatedOn = dash.CreatedOn,
                    Layers = dash.Layers,
                };
                return Ok(dashe);
            }
            else
            {
                return Ok($"No Dashboards with the with id :{dashId} !");
            }
        }

        // SHORTLY add one Dashboard "json in body" fn()
        [HttpPost]
        public IActionResult AddDashboard(DashPostDTO newDsb)   //  int userId
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            List<Layer> DBLayers = new();
            if (newDsb.LayersIds != null)
            {
                foreach (int id in newDsb.LayersIds) // how ???
                {
                    Layer layer = _context.Layers.FirstOrDefault(ly => ly.Id == id);
                    DBLayers.Add(layer);
                }
            }
            var dsp = new Dashboard()
            {
                UserId = userId,
                Name = newDsb.Name,
                Widgets = newDsb.Widgets,
                Layers = DBLayers
            };

            _context.Dashboards.Add(dsp);   // add new db to general dbs list not to db of a user 
            _context.SaveChanges();
            return Ok($"Dashboard: {newDsb.Name} was added successfully!");
        }

        // edit D Name by id fn() // NEED CHECK !
        [HttpPut("{id}")]
        public IActionResult EditDashboard(int id, DashPostDTO newDs)
        {
            // access wanted dep with sent id
            var oldDs = _context.Dashboards.FirstOrDefault(ds => ds.Id == id);
            if (oldDs != null)
            {
                oldDs.Name = newDs.Name;
                oldDs.Widgets = newDs.Widgets;

                List<Layer> DBLayers = new();
                foreach (int Id in newDs.LayersIds)
                {
                    Layer layer = _context.Layers.FirstOrDefault(ly => ly.Id == Id);
                    DBLayers.Add(layer);
                }

                oldDs.Layers = DBLayers;
                _context.SaveChanges();
                return Ok($"Dashboard: {oldDs.Name} was edited");
            }
            else
            {
                return Ok($"No Dashboard with id: {id}");
            }
        }

        // delete fn()
        [HttpDelete("{id}")]
        public IActionResult DeleteDashboard(int id)
        {
            // access ds with given id 
            var ds = _context.Dashboards.FirstOrDefault(ds => ds.Id == id);
            if (ds != null)
            {
                ds.IsDeleted = true;
                _context.SaveChanges();
                return Ok($"Dashboard with id: {id} was deleted!");
            }
            else
            {
                return Ok($"No Dashboard with id: {id}");
            }

        }

        // search by part of layer name 
        [HttpGet("getByName")]
        public IActionResult SearchByName(string pName)
        {
            var pNameE = pName.ToUpper();
            var oldD = _context.Dashboards.Where(d => d.Name.ToUpper().Contains(pNameE)).ToList();

            if (oldD.Count() > 0)
            {
                var newD = new List<DashGetDTO>();// new obj of dto to show data

                foreach (Dashboard ds in oldD)
                {
                    newD.Add(new DashGetDTO()
                    {
                        Name = ds.Name,
                        Widgets = ds.Widgets,
                        Layers = ds.Layers,
                        CreatedOn = ds.CreatedOn,
                    });
                }
                return Ok(newD);
            }
            else
            {
                return Ok("No Dashboards with specific part od name !");
            }

        }

        //// drop fn()
        //[HttpDelete("{id}")]
        //public IActionResult DropDashboard(int id)
        //{
        //    // access dep with given id 
        //    var ds = _context.Dashboards.FirstOrDefault(dsh => dsh.Id == id);
        //    if (ds != null)
        //    {
        //        _context.Remove(ds);
        //        _context.SaveChanges();
        //        return Ok($"Dashboard with id: {id} was deleted!");
        //    }
        //    else
        //    {
        //        return Ok($"No Dashboard with id: {id}");
        //    }

        //}

    }
}
