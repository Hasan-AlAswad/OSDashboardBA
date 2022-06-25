using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OSDashboardBA.Auth;
using OSDashboardBA.DB;
using OSDashboardBA.Models;

namespace OSDashboardBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // prop 
        public AppDbContext _context { get; set; }

        // constructor 
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        // fn - actions - 

        // get 
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllUserDashs()
        {
            // get list of dashboards exist
            var oldUsers = _context.UsersD.Where(ly => ly.IsDeleted != true).ToList();

            if (oldUsers.Count > 0)
            {
                // new obj of dto to show data 
                var newUsers = new List<UserGetDTO>();

                foreach (User user in oldUsers)
                {
                    newUsers.Add(new UserGetDTO()
                    {
                        Name = user.Name,
                        Dashboards = user.Dashboards,
                        Layers = user.Layers,

                    });
                }
                return Ok(newUsers);
            }
            else
            {
                return Ok("no Users yet!");
            }
        }

        // SHORTLY add one User "json in body" fn()
        [HttpPost]
        public IActionResult AddUser(UserPostDTO newUser)
        {
            var user = new User()
            {
                Name = newUser.Name,
            

            };
            _context.UsersD.Add(user);
            _context.SaveChanges();
            return Ok($"User: {user.Name} was added successfully!");
        }

        // edit user by id fn() // NEED CHECK !
        [HttpPut("{id}")]
        public IActionResult EditUser(int id, UserPostDTO newUser)
        {
            // access wanted dep with sent id
            var oldUser = _context.UsersD.FirstOrDefault(user => user.Id == id);
            if (oldUser != null)
            {
                oldUser.Name = newUser.Name;


                _context.SaveChanges();
                return Ok($"User: {oldUser.Name} was edited");
            }
            else
            {
                return Ok($"no User with id: {id}");
            }
        }

        // delete fn()
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            // access dep with given id 
            var user = _context.UsersD.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                _context.SaveChanges();
                return Ok($"User with id: {id} was deleted!");
            }
            else
            {
                return Ok($"no User with id: {id}");
            }

        }

        //// drop fn()
        //[HttpDelete("{id}")]
        //public IActionResult DropUser(int id)
        //{
        //    // access dep with given id 
        //    var user = _context.UsersD.FirstOrDefault(u => u.Id == id);
        //    if (user != null)
        //    {
        //        _context.Remove(user);
        //        _context.SaveChanges();
        //        return Ok($"User with id: {id} was deleted!");
        //    }
        //    else
        //    {
        //        return Ok($"no User with id: {id}");
        //    }

        //}

        // search by id
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId)             
        {

            var user = _context.UsersD.FirstOrDefault(ds => ds.Id == userId);
            // new obj of dto to show data 
            if (user != null)
            {
                var userD = new UserGetDTO()
                {
                    Name = user.Name,
                    Layers = user.Layers,
                    Dashboards=user.Dashboards,
                };
                return Ok(userD);
            }
            else
            {
                return Ok($"no user with the with id:{userId}");
            }
        }

        // search by part of layer name 
        [HttpGet("{pName}")]
        public IActionResult SearchByName(string pName)
        {
            var pNameE = pName.ToUpper();
            var oldU = _context.UsersD.Where(d => d.Name.ToUpper().Contains(pNameE)).ToList();

            if (oldU.Count() > 0)
            {
                var newU = new List<UserGetDTO>();// new obj of dto to show data

                foreach (User ds in oldU)
                {
                    newU.Add(new UserGetDTO()
                    {

                        Name = ds.Name,
                        Layers = ds.Layers,
                        Dashboards = ds.Dashboards,
                    });
                }
                return Ok(newU);
            }
            else
            {
                return Ok("No Users with specific part od name !");
            }

        }

    }
}
