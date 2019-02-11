using DragSnackApi.Models;
using DragSnackApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Services
{
    public class TeammemberService
    {
        public void InsertTeammember(TeammemberModel model)
        {
            TeammemberRepository repo = new TeammemberRepository();
            var member = repo.InsertTeammember(model);

            ProjectRepository projectRepo = new ProjectRepository();
            var bankProject = projectRepo.GetBankProject();

            for (int i = 0; i < model.Capacity; i++)
            {
                ProjectTeammembersRepository projectMemberRepo = new ProjectTeammembersRepository();
                projectMemberRepo.InsertProjectMember(bankProject.Id, member.Id);
            }
        }

        public void UpdateTeammember(string id, TeammemberModel newModel)
        {
            TeammemberRepository memberRepo = new TeammemberRepository();
            ProjectTeammembersRepository projectMemberRepo = new ProjectTeammembersRepository(); 
            var oldModel = memberRepo.SelectTeammemberById(id);

            int capacityDifference = newModel.Capacity - oldModel.Capacity;
            if (capacityDifference > 0)
            {
                // capa was increased
                for (int i = 0; i < capacityDifference; i++)
                {
                    projectMemberRepo.InsertMember(newModel.Id);
                }
            }
            else if (capacityDifference < 0)
            {
                // capa was decreased
                for (int i = 0; i < Math.Abs(capacityDifference); i++)
                {
                    projectMemberRepo.DeleteMemberOnce(newModel.Id);
                }
            }

            memberRepo.UpdateTeammember(id, newModel);
        }

        public void DeleteTeammember(string id)
        {
            ProjectTeammembersRepository projectMemberRepo = new ProjectTeammembersRepository();
            projectMemberRepo.DeleteMember(id);

            TeammemberRepository repo = new TeammemberRepository();
            repo.DeleteTeammember(id);
        }
    }
}
