using DragSnackApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Services
{
    public class ProjectTeammembersService
    {
        public void MoveMemberToBank(string mappingId)
        {
            ProjectRepository projectRepo = new ProjectRepository();
            var bankProject = projectRepo.GetBankProject();
            ProjectTeammembersRepository projectTeammembersRepo = new ProjectTeammembersRepository();
            projectTeammembersRepo.DeleteProjectMemberOnce(projectId, memberId);
            projectTeammembersRepo.InsertProjectMember(bankProject.Id, memberId);
        }
    }
}
