using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Models
{
    public class TeammemberMappingModel
    {
        public string MappingId { get; set; }
        public TeammemberModel Member { get; set; }
    }
}
