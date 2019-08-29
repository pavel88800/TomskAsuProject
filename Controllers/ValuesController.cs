using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApiApplication.Models;

namespace WebApiApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private AppDatabaseContext _ctx;

        public object TheQuery { get; private set; }

        public ValuesController(AppDatabaseContext ctx) {
            this._ctx = ctx;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetTodoItems() // полная выборка из бд и передача в api
        {
            return await _ctx.Users.ToListAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
         {
            return await _ctx.Users.FindAsync(id);
         }


        // POST api/values
        [HttpPost]
        public void Post(User request)
        {
            User user = new User();

            user.Name = request.Name;
            user.Age = request.Age;

            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id,User request)
        {
            User user = _ctx.Users.Find(id);
            user.Name = request.Name;
            user.Age = request.Age;

            _ctx.SaveChanges();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            User user = _ctx.Users.Find(id);

            _ctx.Users.Remove(user);
            _ctx.SaveChanges();
        }
    }
}
