using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DragSnackApi.Models;
using DragSnackApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DragSnackApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Board")]
    public class BoardController : Controller
    {
        [HttpGet]
        public BoardModel Get()
        {
            ProjectRepository repo = new ProjectRepository();
            var projects = repo.GetProjects();
            var bankProject = repo.GetBankProject();

            return new BoardModel
            {
                Projects = projects,
                BankProject = bankProject
            };
        }
    }
}