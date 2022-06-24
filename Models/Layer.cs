namespace OSDashboardBA.Models
{
    public class Layer
    {
        // ctor
        public Layer()
        {
            Dashbords = new List<Dashboard>();
            CreatedOn = DateTime.Now;
        }

        // props
        public int Id { get; set; }
        public string? LayerName { get; set; }
        public string? LayerDescription { get; set; }
        // public string[]? Attributes { get; set; }       // check - schemeless ?
        public DateTime CreatedDate { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        // geojson
        public string GeoJson { get; set; }

        // attribtes



        // relations 
        public User? Users { get; set; }             // check 
        public List<Dashboard> Dashbords { get; set; }
    }

    // DTOs
    // GET 
    public class LayGetDTO
    {
        public string? LayerName { get; set; }
        public string? LayerDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public User? LayerUser { get; set; }   // pending
    }

    // POST 
    public class LayPostDTO
    {
        public string? LayerName { get; set; }
        public string? LayerDescription { get; set; }
        public string GeoJson { get; set; }

    }
}
