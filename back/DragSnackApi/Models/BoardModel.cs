using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Models
{
    public class BoardModel
    {
        public IEnumerable<ProjectModel> Projects { get; set; }
        public ProjectModel BankProject { get; set; }

        public BoardModel()
        {
            Projects = new List<ProjectModel>();
        }
    }
}
