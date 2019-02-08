using DragSnackApi.Entities;
using DragSnackApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Repositories
{
    public class TeammemberRepository : IEntityFrameworkRepositoryBase
    {
        public TeammemberRepository() : base()
        {

        }

        public IEnumerable<TeammemberModel> SelectTeammembers()
        {
            return Map(context.Teammember);
        }

        public TeammemberModel SelectTeammemberById(string id)
        {
            var entity = context.Teammember.Where(t => t.Id == id).SingleOrDefault();
            if (entity != null)
            {
                return Map(entity);
            }
            else
            {
                return null;
            }
        }

        public TeammemberModel InsertTeammember(TeammemberModel model)
        {
            if (string.IsNullOrEmpty(model.Id))
            {
                model.Id = Guid.NewGuid().ToString();
            }
            Teammember entity = Map(model);
            
            context.Teammember.Add(entity);
            context.SaveChangesAsync();
            
            return model;
        }

        public void UpdateTeammember(string id, TeammemberModel model)
        {
            var entity = context.Teammember.Where(t => t.Id == id).SingleOrDefault();
            if (entity != null)
            {
                entity.Name = model.Name;
                entity.Capacity = model.Capacity;
                context.SaveChangesAsync();
            }
        }
        public void DeleteTeammember(string id)
        {
            var entity = context.Teammember.Where(t => t.Id == id).SingleOrDefault();
            if (entity != null)
            {
                context.Remove(entity);
                context.SaveChangesAsync();
            }
        }

        public static IEnumerable<TeammemberModel> Map(IEnumerable<Teammember> entities)
        {
            List<TeammemberModel> models = new List<TeammemberModel>();
            foreach (var entity in entities)
            {
                models.Add(Map(entity));
            }
            return models;
        }

        public static TeammemberModel Map(Teammember entity)
        {
            TeammemberModel model = new TeammemberModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Capacity = entity.Capacity
            };
            return model;
        }

        public Teammember Map(TeammemberModel model)
        {
            Teammember entity = new Teammember
            {
                Id = model.Id,
                Name = model.Name,
                Capacity = model.Capacity
            };
            return entity;
        }
    }
}
