using DragSnackApi.Entities;
using DragSnackApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Repositories
{
    public class ProjectTeammembersRepository : IEntityFrameworkRepositoryBase
    {
        public ProjectTeammembersRepository() : base()
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

        public void DeleteProjectMemberOnce(string projectId, string memberId)
        {
            var deleteMe = context.ProjectMember.Where(pm => pm.ProjectId == projectId && pm.MemberId == memberId).FirstOrDefault();
            if (deleteMe == null)
            {
                throw new Exception("deletion not possible. data not found.");
            }
            else
            {
                context.ProjectMember.Remove(deleteMe);
                context.SaveChanges();
            }
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

        public IEnumerable<ProjectModel> GetProjectsWithMembers()
        {
            return Map(context.Project.Where(p => !p.BankProject).Include(p => p.ProjectMember).ThenInclude(pm => pm.Member));
        }

        public ProjectModel GetBankProjectWithMembers()
        {
            var entity = context.Project.Where(p => p.BankProject).Include(p => p.ProjectMember).ThenInclude(pm => pm.Member).SingleOrDefault();
            if (entity != null)
            {
                return Map(entity);
            }
            else
            {
                return null;
            }
        }

        public void UpdateProject(IEnumerable<string> mappingIds, string projectId)
        {
            var memberMappings = context.ProjectMember.Where(pm => mappingIds.Contains(pm.Id));
            foreach (var memberMapping in memberMappings)
            {
                memberMapping.ProjectId = projectId;
            }
            context.SaveChanges();
        }

        public void UpdateProject(string mappingId, string projectId)
        {
            var memberMapping = context.ProjectMember.Where(pm => pm.Id == mappingId).SingleOrDefault();
            memberMapping.ProjectId = projectId;
            context.SaveChanges();
        }

        private IEnumerable<ProjectModel> Map(IEnumerable<Project> entities)
        {
            List<ProjectModel> models = new List<ProjectModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }
            return models;
        }

        private ProjectModel Map(Project projectEntity)
        {
            IEnumerable<TeammemberMappingModel> members = Map(projectEntity.ProjectMember);
            ProjectModel model = new ProjectModel
            {
                Id = projectEntity.Id,
                Name = projectEntity.Name,
                Members = members
            };
            return model;
        }

        private IEnumerable<TeammemberMappingModel> Map(IEnumerable<ProjectMember> projectMembers)
        {
            List<TeammemberMappingModel> models = new List<TeammemberMappingModel>();
            foreach (var projectMember in projectMembers)
            {
                models.Add(Map(projectMember));
            }
            return models;
        }

        private TeammemberMappingModel Map(ProjectMember projectMember)
        {
            var member = TeammemberRepository.Map(projectMember.Member);
            return new TeammemberMappingModel
            {
                MappingId = projectMember.Id,
                Member = member
            };
        }
    }
}
