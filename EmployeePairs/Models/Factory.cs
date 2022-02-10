
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Models
{
    internal abstract class Factory
    {
        protected abstract Project MakeProject(int id);

        public Project CreateProject(int id)
        {
            return MakeProject(id);
        }


        
    }
}
