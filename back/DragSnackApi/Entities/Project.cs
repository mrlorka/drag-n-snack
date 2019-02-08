using System;
using System.Collections.Generic;

namespace DragSnackApi.Entities
{
    public partial class Project
    {
        public Project()
        {
            ProjectMember = new HashSet<ProjectMember>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public bool BankProject { get; set; }

        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
    }
}
