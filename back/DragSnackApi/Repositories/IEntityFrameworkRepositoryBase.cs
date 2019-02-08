using DragSnackApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DragSnackApi.Repositories
{
    public abstract class IEntityFrameworkRepositoryBase
    {
        protected DragSnackDataContext context = new DragSnackDataContext();

        public IEntityFrameworkRepositoryBase()
        {
            this.context = new DragSnackDataContext();
        }
    }
}
