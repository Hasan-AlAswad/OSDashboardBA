using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("GetAllUserDashs")]     // route
        [HttpGet]                     // verb // [attribute]
        public IActionResult GetAllUserDashs()
        {
            // get list of dashboards exist
            var oldUsers = _context.Users.Where(ly => ly.IsDeleted != true).ToList();

            // new obj of dto to show data 
            var newUsers = new List<UserGetDTO>();

            foreach (User user in oldUsers)
            {
                newUsers.Add(new UserGetDTO()
                {
                    Name = user.Name,
                    // Dashboards = user.Dashboards,
                    Layers = user.Layers,
                    
                });
            }

            if (newUsers.Count > 0)
            {
                return Ok(newUsers);
            }
            else
            {
                return Ok("no Users yet!");
            }
        }

        // SHORTLY add one User "json in body" fn()
        [Route("AddUser")]
        [HttpPost]
        public IActionResult AddUser(UserPostDTO newUser)
        {
            var user = new User()
            {
                Name = newUser.Name,
         

            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return Ok($"User: {newUser.Name} was added successfully!");
        }

        // edit DepName by id fn() // NEED CHECK !
        [Route("EditUser")]
        [HttpPut]
        public IActionResult EditUser(int id, UserPostDTO newUser)
        {
            // access wanted dep with sent id
            var oldUser = _context.Users.FirstOrDefault(user => user.Id == id);
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
        [Route("DeleteUser")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            // access dep with given id 
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
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

        // drop fn()
        [Route("DropUser")]
        [HttpDelete]
        public IActionResult DropUser(int id)
        {
            // access dep with given id 
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChanges();
                return Ok($"User with id: {id} was deleted!");
            }
            else
            {
                return Ok($"no User with id: {id}");
            }

        }

        // search by part of dash name 
        [Route("SearchByName")]
        [HttpGet]
        public List<User> SearchByName(string pName)
        {
            var pNameE = pName.ToUpper();
            return _context.Users.Where(u => u.Name.Contains(pNameE)).ToList();
        }
    }
}
