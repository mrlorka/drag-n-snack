using System;
using System.Collections.Generic;

namespace DragSnackApi.Entities
{
    public partial class Teammember
    {
        public Teammember()
        {
            ProjectMember = new HashSet<ProjectMember>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public short Capacity { get; set; }

        public virtual ICollection<ProjectMember> ProjectMember { get; set; }
    }
}
