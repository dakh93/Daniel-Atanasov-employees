
using EmployeePairs.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Models
{
    internal class UniqueProject : IUniqueProject
    {
        public UniqueProject(int firstEmployeeId, int secondEmployeeId, int projectId, int totalDays)
        {
            this.FirstEmployeeId = firstEmployeeId;
            this.SecondEmployeeId = secondEmployeeId;
            this.ProjectId = projectId;
            this.TotalDays = totalDays;
        }
        public int FirstEmployeeId { get; private set; }
        public int SecondEmployeeId { get; private set; }

        public int ProjectId { get; private set; }

        public int TotalDays { get; private set; }

    }
}
