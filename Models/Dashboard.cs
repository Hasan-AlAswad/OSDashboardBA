using Microsoft.EntityFrameworkCore;
using OSDashboardBA.Services;
using System.ComponentModel.DataAnnotations;

namespace OSDashboardBA.Models
{
    public class Dashboard
    {
        // constructor
        public Dashboard()
        {
            Layers = new List<Layer>();
            CreatedOn = DateTime.Now;
            UserD = new User();
            Widgets = new List<TextString>();
        }


        // public Guid Id { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }           // creation date
        public List<TextString> Widgets { get; set; }
        // charts ?

        // relation props 
        public string UserId { get; set; }     // nav prop 
        public List<Layer> Layers { get; set; } // many

        public User UserD { get; set; }

    }

    // dto 
    // get
    public class DashGetDTO
    {
        public string Name { get; set; }
        public List<Layer> Layers { get; set; } // many
        public DateTime CreatedOn { get; set; }
        public List<TextString> Widgets { get; set; }
    }
    // post
    public class DashPostDTO
    {
        public string Name { get; set; }
        public List<Layer> Layers { get; set; } // many
        public List<TextString> Widgets { get; set; }
    }
}
