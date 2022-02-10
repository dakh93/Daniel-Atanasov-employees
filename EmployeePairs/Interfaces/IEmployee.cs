using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Contracts
{
    internal interface IEmployee
    {
        int EmployeeId { get; }
        DateTime StartDate { get; }
        DateTime EndDate { get; }
    }
}
