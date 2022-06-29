using Microsoft.AspNetCore.Identity;

namespace OSDashboardBA.Models
{
    public class User:IdentityUser
    {
        // ctor
        public User()
        {
            Dashboards = new List<Dashboard>();
            Layers = new List<Layer>();
        }

        // props
        public int _Id { get; set; }
        public string Name { get; set; }
        public Boolean IsDeleted { get; set; }

        // relations 
        public List<Dashboard> Dashboards { get; set; }         // many
        public List<Layer> Layers { get; set; }                    // many

    }


    // dto  
    public class UserGetDTO
    {
        public string Name { get; set; }
        public List<Dashboard> Dashboards { get; set; }
        public List<Layer> Layers { get; set; }

    }

    public class UserPostDTO
    {
        public string Name { get; set; }
    }
}
