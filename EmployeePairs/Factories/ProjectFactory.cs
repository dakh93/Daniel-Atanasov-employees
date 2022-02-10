
using EmployeePairs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Factories
{
    internal class ProjectFactory : Factory
    {
        protected override Project MakeProject(int id)
        {
            return new Project(id);
        }
    }
}
