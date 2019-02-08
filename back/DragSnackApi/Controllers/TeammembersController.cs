using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragSnackApi.Models;
using DragSnackApi.Repositories;
using DragSnackApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace DragSnackApi.Controllers
{
    [Route("api/[controller]")]
    public class TeammembersController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<TeammemberModel> Get()
        {
            TeammemberRepository repo = new TeammemberRepository();
            return repo.SelectTeammembers();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public TeammemberModel Get(string id)
        {
            TeammemberRepository repo = new TeammemberRepository();
            return repo.SelectTeammemberById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]TeammemberModel value)
        {
            TeammemberService service = new TeammemberService();
            service.InsertTeammember(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]TeammemberModel value)
        {
            TeammemberService service = new TeammemberService();
            service.UpdateTeammember(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TeammemberService service = new TeammemberService();
            service.DeleteTeammember(id);
        }
    }
}
