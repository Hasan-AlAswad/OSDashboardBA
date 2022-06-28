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
            UserD = new User();
         
        }

        // props
        public int Id { get; set; }
        public string LayerName { get; set; }
        // public string[]? Attributes { get; set; }       // check - schemeless ?
        public DateTime CreatedOn { get; set; }
        // geojson
        public string GeoJson { get; set; }

        public Boolean IsDeleted { get; set; }
        // relations 
        public string UserId { get; set; }
        public User UserD { get; set; }             // check 
    }

    // DTOs
    // GET 
    public class LayGetDTO
    {
        public int Id { get; set; }
        public string LayerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public String GeoJson { get; set; }
    }

    // POST 
    public class LayPostDTO
    {
        public string LayerName { get; set; }
        public string GeoJson { get; set; }

    }
}
