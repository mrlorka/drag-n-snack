using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Models
{
    public class ProjectModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<TeammemberMappingModel> Members { get; set; }
    }
}
