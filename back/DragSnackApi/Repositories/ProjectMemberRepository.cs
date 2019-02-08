using DragSnackApi.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Repositories
{
    public class ProjectMemberRepository : IEntityFrameworkRepositoryBase
    {
        public ProjectMemberRepository() : base()
        {

        }

        public void InsertProjectMember(string projectId, string memberId)
        {
            ProjectMember pm = new ProjectMember
            {
                Id = Guid.NewGuid().ToString(),
                ProjectId = projectId,
                MemberId = memberId
            };
            context.ProjectMember.Add(pm);
            context.SaveChanges();
        }

        public void InsertMember(string memberId)
        {
            var bankProject = context.Project.Where(p => p.BankProject).SingleOrDefault();
            InsertProjectMember(bankProject.Id, memberId);
        }

        public void DeleteMemberOnce(string memberId)
        {
            var bankProject = context.Project.Where(p => p.BankProject).Include(p => p.ProjectMember).SingleOrDefault();
            // delete member from bank first
            var projectMemberToDelete = bankProject.ProjectMember.Where(p => p.MemberId == memberId).FirstOrDefault();
            if (projectMemberToDelete == null)
            {
                projectMemberToDelete = context.ProjectMember.Where(pm => pm.MemberId == memberId).FirstOrDefault();
            }
            context.Remove(projectMemberToDelete);
            context.SaveChanges();
        }

        public void DeleteMember(string memberId)
        {
            var projectMembersToDelete = context.ProjectMember.Where(pm => pm.MemberId == memberId);
            context.RemoveRange(projectMembersToDelete);
            context.SaveChanges();
        }

        public void ChangeProject(string oldProjectId, string newProjectId)
        {
            foreach (var projectMember in context.ProjectMember.Where(pm => pm.ProjectId == oldProjectId))
            {
                projectMember.ProjectId = newProjectId;
            }
            context.SaveChanges();
        }
    }
}
