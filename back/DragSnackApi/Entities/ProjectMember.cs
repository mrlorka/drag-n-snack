using System;
using System.Collections.Generic;

namespace DragSnackApi.Entities
{
    public partial class ProjectMember
    {
        public string Id { get; set; }
        public string ProjectId { get; set; }
        public string MemberId { get; set; }

        public virtual Teammember Member { get; set; }
        public virtual Project Project { get; set; }
    }
}
