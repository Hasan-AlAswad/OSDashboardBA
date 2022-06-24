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
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllLayers()
        {
            // 
            // get list of dashboards exist
            var oldLay = _context.Layers.Where(ly => ly.IsDeleted != true).ToList();

            // new obj of dto to show data 
            if (oldLay.Count() > 0)
            {
            var newLay = new List<LayGetDTO>();

            foreach (Layer lay in oldLay)
            {
                newLay.Add(new LayGetDTO()
                {
                    Id = lay.Id,
                    LayerName = lay.LayerName,
                    CreatedOn = lay.CreatedOn,
                    GeoJson = lay.GeoJson,
                });
            }

                return Ok(newLay);
            }
            else
            {
                return Ok("no Layers yet!");
            }
        }

        // SHORTLY add one Layer "json in body" fn()
        [HttpPost]
        public IActionResult AddLayer(LayPostDTO newLay)  // , int userId
        {
             string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var lay = new Layer()
            {
                LayerName = newLay.LayerName,
                GeoJson=newLay.GeoJson,
                UserId = userId
            };

            _context.Layers.Add(lay);   // wrong: add new layer to layers list not to layers of a user 
            _context.SaveChanges();
            return Ok("m**en Omak Eshtaghal");
            //string.Format("{0}",newLay)
            //RequestContext.Principal

            // fileService.WriteFile($@"Files\{userId}\GeoJsons", "json", newLay.GeoJson);

            // userId : extract from jwt token 
            // _context.SaveChanges();
            // return Ok($"Layer: {lay.LayerName} was added successfully!");

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
        [HttpPut("{layerId:int}")]
        public IActionResult EditLayer(int layerId, LayPostDTO newLay)
        {
            // access wanted dep with sent id
            var oldDs = _context.Layers.FirstOrDefault(lay => lay.Id == layerId);
            if (oldDs != null)
            {
                oldDs.LayerName = newLay.LayerName;
                oldDs.GeoJson = newLay.GeoJson;
                _context.SaveChanges();
                return Ok($"Layer: {oldDs.LayerName} was edited");
            }
            else
            {
                return Ok($"no Layer with id: {layerId}");
            }
        }

        // delete fn()
        [HttpDelete("{layerId:int}")]
        public IActionResult DeleteLayer(int layerId)
        {
            // access dep with given id 
            var lay = _context.Layers.FirstOrDefault(ly => ly.Id == layerId); // notes 
            if (lay != null)
            {
                lay.IsDeleted = true;
                _context.SaveChanges();
                return Ok($"Layer with id: {layerId} was deleted!");
            }
            else
            {
                return Ok($"no Layer with id: {layerId}");
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
        [HttpGet("{layerId:int}")]
        public IActionResult getLayerById(int layerId)             /*List<Dashboard>*/
        {

            var layer = _context.Layers.FirstOrDefault(ds => ds.Id == layerId);
            // new obj of dto to show data 
            if (layer != null)
            {
                var lay = new LayGetDTO()
                {
                    Id = layer.Id,
                    LayerName = layer.LayerName,
                    CreatedOn = layer.CreatedOn,
                    GeoJson = layer.GeoJson,
                };
                return Ok(lay);
            }
            else
            {
                return Ok("no Dashboards with the with such a part of its name !");
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
