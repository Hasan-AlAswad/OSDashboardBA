using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSDashboardBA.DB;
using OSDashboardBA.Models;
using OSDashboardBA.Services;
using System.Security.Claims;

namespace OSLayerBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LayersController : ControllerBase
    {
        private readonly FileService fileService;

        // prop 
        public AppDbContext _context { get; set; }

        // constructor 
        public LayersController(AppDbContext context, FileService fileService)
        {
            _context = context;
            this.fileService = fileService;
        }

        // fn - actions - 

        // get 
        [Route("GetAllLayers")]     // route
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllLayers()
        {
            // 
            // get list of dashboards exist
            var oldLay = _context.Layers.Where(ly => ly.IsDeleted != true).ToList();

            // new obj of dto to show data 
            var newLay = new List<LayGetDTO>();

            foreach (Layer lay in oldLay)
            {
                newLay.Add(new LayGetDTO()
                {
                    LayerName = lay.LayerName,
                    LayerDescription = lay.LayerDescription,
                    CreatedDate = lay.CreatedDate,
                    // LayerUser = lay.UserL
                });
            }

            if (newLay.Count() > 0)
            {
                return Ok(newLay);
            }
            else
            {
                return Ok("no Layers yet!");
            }
        }

        // SHORTLY add one Layer "json in body" fn()
        [Route("AddLayer")]
        [HttpPost]
        public IActionResult AddLayer(LayPostDTO newLay)  // , int userId
        {
            var lay = new Layer()
            {
                LayerName = newLay.LayerName,
                LayerDescription = newLay.LayerDescription,

            };

            _context.Layers.Add(lay);   // wrong: add new layer to layers list not to layers of a user 
            //string.Format("{0}",newLay)
            //RequestContext.Principal
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            fileService.WriteFile($@"Files\{userId}\GeoJsons", "json", newLay.GeoJson);

            // userId : extract from jwt token 
            _context.SaveChanges();
            return Ok($"Layer: {lay.LayerName} was added successfully!");

            //var userD = _context.UserDashs.FirstOrDefault(us => us.Id == userId);
            //if (userD != null)
            //{
            //    userD.Layers.Add(lay);
            //    //_context.Layers.Add(lay);   // wrong: add new layer to layers list not to layers of a user 
            //    _context.SaveChanges();
            //    return Ok($"Layer: {lay.LayerName} was added successfully!");
            //}
            //else
            //{
            //    return Ok($"no user with id: {userId}");
            //}
        }

        // edit DepName by id fn() // NEED CHECK !
        [Route("EditLayer")]
        [HttpPut]
        public IActionResult EditLayer(int id, LayPostDTO newLay)
        {
            // access wanted dep with sent id
            var oldDs = _context.Layers.FirstOrDefault(lay => lay.Id == id);
            if (oldDs != null)
            {
                oldDs.LayerName = newLay.LayerName;
                oldDs.LayerDescription = newLay.LayerDescription;


                _context.SaveChanges();
                return Ok($"Layer: {oldDs.LayerName} was edited");
            }
            else
            {
                return Ok($"no Layer with id: {id}");
            }
        }

        // delete fn()
        [Route("DeleteLayer")]
        [HttpDelete]
        public IActionResult DeleteLayer(int id)
        {
            // access dep with given id 
            var lay = _context.Layers.FirstOrDefault(ly => ly.Id == id); // notes 
            if (lay != null)
            {
                lay.IsDeleted = true;
                _context.SaveChanges();
                return Ok($"Layer with id: {id} was deleted!");
            }
            else
            {
                return Ok($"no Layer with id: {id}");
            }

        }

        // drop fn()
        [Route("DropLayer")]
        [HttpDelete]
        public IActionResult DropLayer(int id)
        {
            // access dep with given id 
            var lay = _context.Layers.FirstOrDefault(ly => ly.Id == id);
            if (lay != null)
            {
                _context.Remove(lay);
                _context.SaveChanges();
                return Ok($"Layer with id: {id} was deleted!");
            }
            else
            {
                return Ok($"no Layer with id: {id}");
            }

        }

        // search by part of dash name 
        [Route("SearchByName")]
        [HttpGet]
        public List<Layer> SearchByName(string pName)
        {
            var pNameE = pName.ToUpper();
            return _context.Layers.Where(ly => ly.LayerName.Contains(pNameE)).ToList();
        }
    }
}
