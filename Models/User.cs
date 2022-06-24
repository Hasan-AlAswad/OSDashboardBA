using Microsoft.AspNetCore.Identity;

namespace OSDashboardBA.Models
{
    public class User 
    {
        // ctor
        public User()
        {
            Dashboards = new List<Dashboard>();
            Layers = new List<Layer>();
        }

        // props
        public int Id { get; set; }
        public string Name { get; set; }
        public Boolean IsDeleted { get; set; }

        //// from registeration
        //public string? FirstName { get; set; }
        //public string? LastName { get; set; }
        //public int Age { get; set; }

        // relations 
        public List<Dashboard> Dashboards { get; set; }         // many
        public List<Layer> Layers { get; set; }                    // many

    }

    //// roles
    //public class AppRole : IdentityRole
    //{
    //    //public int RoleId { get; set; }
    //    //public string? RoleName { get; set; }
    //    //public string? UserName { get; set; }


    //}

    // dto  - to be continued 
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
