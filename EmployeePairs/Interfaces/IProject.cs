using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Contracts
{
    internal interface IProject
    {
        int ProjectId { get; }

        List<IEmployee> Employees { get; }

        void AddEmployeeToProject(IEmployee employee);
    }
}
