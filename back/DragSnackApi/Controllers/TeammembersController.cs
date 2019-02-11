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
        [HttpGet]
        public IEnumerable<TeammemberModel> Get()
        {
            TeammemberRepository repo = new TeammemberRepository();
            return repo.SelectTeammembers();
        }
        
        [HttpGet("{id}")]
        public TeammemberModel Get(string id)
        {
            TeammemberRepository repo = new TeammemberRepository();
            return repo.SelectTeammemberById(id);
        }
        
        [HttpPost]
        public void Post([FromBody]TeammemberModel value)
        {
            TeammemberService service = new TeammemberService();
            service.InsertTeammember(value);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]TeammemberModel value)
        {
            TeammemberService service = new TeammemberService();
            service.UpdateTeammember(id, value);
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            TeammemberService service = new TeammemberService();
            service.DeleteTeammember(id);
        }
    }
}
