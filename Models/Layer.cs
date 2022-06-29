using Microsoft.EntityFrameworkCore;
using OSDashboardBA.Services;
using System.ComponentModel.DataAnnotations;

namespace OSDashboardBA.Models
{
    public class Layer
    {
        // ctor
        public Layer()
        {
            CreatedOn = DateTime.Now;
         
        }

        // props
        public int Id { get; set; }
        public string LayerName { get; set; }
        // public string[]? Attributes { get; set; }       // check - schemeless ?
        public DateTime CreatedOn { get; set; }
        // geojson
        public string GeoJson { get; set; }
        // symbology
        public string Style { get; set; }

        public Boolean IsDeleted { get; set; }
        // relations 
        public string UserId { get; set; }
    }

    // DTOs
    // GET 
    public class LayGetDTO
    {
        public int Id { get; set; }
        public string LayerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public string GeoJson { get; set; }
        public string Style { get; set; }
    }

    // POST 
    public class LayPostDTO
    {
        public string LayerName { get; set; }
        public string GeoJson { get; set; }
        public string Style { get; set; }
    }
}
