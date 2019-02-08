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
    public class ProjectsController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<ProjectModel> Get()
        {
            ProjectRepository repo = new ProjectRepository();
            return repo.GetProjects();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ProjectModel Get(string id)
        {
            ProjectRepository repo = new ProjectRepository();
            return repo.GetProjectById(id);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]ProjectModel value)
        {
            ProjectRepository repo = new ProjectRepository();
            repo.InsertProject(value);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]ProjectModel value)
        {
            ProjectRepository repo = new ProjectRepository();
            repo.UpdateProject(id, value);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ProjectService service = new ProjectService();
            service.DeleteProject(id);
        }
    }
}
