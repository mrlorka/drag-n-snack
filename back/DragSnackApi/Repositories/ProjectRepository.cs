using DragSnackApi.Entities;
using DragSnackApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Repositories
{
    public class ProjectRepository : IEntityFrameworkRepositoryBase
    {
        public ProjectRepository() : base()
        {

        }

        public IEnumerable<ProjectModel> GetProjects()
        {
            return Map(context.Project.Where(p => !p.BankProject).Include(p => p.ProjectMember).ThenInclude(pm => pm.Member));
        }

        public ProjectModel GetProjectById(string id)
        {
            var entity = context.Project.Include(p => p.ProjectMember).Where(t => t.Id == id).SingleOrDefault();
            if (entity != null)
            {
                return Map(entity);
            }
            else
            {
                return null;
            }
        }

        public ProjectModel GetBankProject()
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

        public void InsertProject(ProjectModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = Guid.NewGuid().ToString();
            }
            Project entity = Map(model);

            context.Project.Add(entity);
            context.SaveChangesAsync();
        }

        public void UpdateProject(string id, ProjectModel model)
        {
            var entity = context.Project.Where(t => t.Id == id).SingleOrDefault();
            if (entity != null)
            {
                entity.Name = model.Name;
                context.SaveChangesAsync();
            }
        }
        public void DeleteProject(string id)
        {
            var entity = context.Project.Where(t => t.Id == id).SingleOrDefault();
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChangesAsync();
            }
        }

        public IEnumerable<ProjectModel> Map(IEnumerable<Project> entities)
        {
            List<ProjectModel> models = new List<ProjectModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }
            return models;
        }

        public ProjectModel Map(Project projectEntity)
        {
            IEnumerable<TeammemberModel> members = TeammemberRepository.Map(projectEntity.ProjectMember.Select(pm => pm.Member));
            ProjectModel model = new ProjectModel
            {
                Id = projectEntity.Id,
                Name = projectEntity.Name,
                Members = members
            };
            return model;
        }

        public Project Map(ProjectModel model)
        {
            Project project = new Project
            {
                Id = model.Id,
                Name = model.Name
            };
            return project;
        }
    }
}
