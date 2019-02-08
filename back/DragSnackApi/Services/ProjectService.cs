﻿using DragSnackApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Services
{
    public class ProjectService
    {
        public void DeleteProject(string id)
        {
            ProjectRepository repo = new ProjectRepository();
            var bankProject = repo.GetBankProject();

            ProjectMemberRepository projectMemberRepo = new ProjectMemberRepository();
            projectMemberRepo.ChangeProject(id, bankProject.Id);

            repo.DeleteProject(id);
        }
    }
}
