using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DragSnackApi.Models;
using DragSnackApi.Repositories;
using DragSnackApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DragSnackApi.Controllers
{
    [Produces("application/json")]
    [Route("api/ProjectTeammembers")]
    public class ProjectTeammembersController : Controller
    {
        [Route("[action]/{mappingId}")]
        [HttpPut]
        public void MemberToBank(string mappingId)
        {
            ProjectTeammembersService service = new ProjectTeammembersService();
            service.MoveMemberToBank(mappingId);
        }

        [Route("[action]/{projectId}")]
        [HttpPut]
        public void AssignMembers(string projectId, [FromBody]IEnumerable<TeammemberMappingModel> members)
        {
            ProjectTeammembersRepository repo = new ProjectTeammembersRepository();
            repo.UpdateProject(members.Select(m => m.MappingId), projectId);
        }
    }
}