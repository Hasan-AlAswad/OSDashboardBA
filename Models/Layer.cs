using Microsoft.EntityFrameworkCore;
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
            GeoJson = new List<TextDTOString>();
        }

        // props
        public int Id { get; set; }
        public string LayerName { get; set; }
        // public string[]? Attributes { get; set; }       // check - schemeless ?
        public DateTime CreatedOn { get; set; }
        // geojson
        public List<TextDTOString> GeoJson { get; set; }

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
        public List<TextDTOString> GeoJson { get; set; }
    }

    // POST 
    public class LayPostDTO
    {
        public string LayerName { get; set; }
        public List<TextDTOString> GeoJson { get; set; }

    }
}
