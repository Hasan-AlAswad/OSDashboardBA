namespace OSDashboardBA.Models
{
    public class Dashboard
    {
        // constructor
        public Dashboard()
        {
            Layers = new List<Layer>();
            Users= new User();
            CreatedOn = DateTime.Now;
        }
        
        // need to check mongodb or use params []

        // public Guid Id { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }              
        public Boolean IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }           // creation date
        // charts ?

        // relation props 
        public User? Users { get; set; }     // one 
        public List<Layer> Layers { get; set; } // many
        public int UserId { get; set; } // navigation property

    }

    // dto 
    // get
    public class DashGetDTO
    {
        public string? Name { get; set; }
        public User? Users { get; set; }     // one 
        public List<Layer>? Layers { get; set; } // many
        public int LayersCount { get; set; }
        public DateTime CreatedDate { get; set; }



    }
    // post
    public class DashPostDTO
    {
        public string? Name { get; set; }
         
    }
}
