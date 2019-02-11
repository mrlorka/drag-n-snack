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
        [HttpGet]
        public IEnumerable<ProjectModel> Get()
        {
            ProjectRepository repo = new ProjectRepository();
            return repo.GetProjects();
        }
        
        [HttpGet("{id}")]
        public ProjectModel Get(string id)
        {
            ProjectRepository repo = new ProjectRepository();
            return repo.GetProjectById(id);
        }
        
        [HttpPost]
        public void Post([FromBody]ProjectModel value)
        {
            ProjectRepository repo = new ProjectRepository();
            repo.InsertProject(value);
        }
        
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]ProjectModel value)
        {
            ProjectRepository repo = new ProjectRepository();
            repo.UpdateProject(id, value);
        }
        
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            ProjectService service = new ProjectService();
            service.DeleteProject(id);
        }
    }
}
