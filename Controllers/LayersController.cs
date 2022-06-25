﻿using Microsoft.AspNetCore.Http;
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

        // fn - actions -------------------------------- 

        // get 
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllLayers()
        {
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
            return Ok($"layer: {lay.LayerName} was added successfully");

            //string.Format("{0}",newLay)
            //RequestContext.Principal

            // fileService.WriteFile($@"Files\{userId}\GeoJsons", "json", newLay.GeoJson);

            // userId : extract from jwt token 
  

        }

        // edit layer by id fn() 
        [HttpPut("{layerId}")]
        public IActionResult EditLayer(int layerId, LayPostDTO newLay)
        {
            // access wanted lay with sent id
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
        [HttpDelete("{layerId}")]
        public IActionResult DeleteLayer(int layerId)
        {
            // access lay with given id 
            var lay = _context.Layers.FirstOrDefault(ly => ly.Id == layerId);
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
        [HttpDelete("{layerId}")]
        public IActionResult DropLayer(int layerId)
        {
            // access lay with given id 
            var lay = _context.Layers.FirstOrDefault(ly => ly.Id == layerId);
            if (lay != null)
            {
                _context.Remove(lay);
                _context.SaveChanges();
                return Ok($"Layer with id: {layerId} was deleted!");
            }
            else
            {
                return Ok($"no Layer with id: {layerId}");
            }

        }
        [HttpGet("{layerId}")]
        public IActionResult GetLayerById(int layerId)             
        {

            var layer = _context.Layers.FirstOrDefault(ds => ds.Id == layerId);
            if (layer != null)
            {
                var lay = new LayGetDTO()               // new obj of dto to show data 
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
                return Ok("no Layers with the with such a part of its name !");
            }
        }

        // search by part of layer name 
        [HttpGet("{pName}")]
        public IActionResult SearchByName(string pName)
        {
            var pNameE = pName.ToUpper();
            var oldLay =  _context.Layers.Where(ly => ly.LayerName.ToUpper().Contains(pNameE)).ToList();
             
            if (oldLay.Count() > 0)
            {
                var newLay = new List<LayGetDTO>();// new obj of dto to show data

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
                return Ok("no Layers with specific part od name !");
            }

        }

    }
}
