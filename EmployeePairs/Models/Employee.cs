
using EmployeePairs.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace EmployeePairs.Models
{
    internal class Employee : IEmployee
    {

        public Employee()
        {
            
        }
        public Employee(int employeeId, DateTime startDate, DateTime endDate)
            : this()
        {
            this.EmployeeId = employeeId;
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        public int EmployeeId { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public int GetTotalDaysInThisProject => this.GetDurationInDays();

        //public void AddProject(Project project)
        //{
        //    if (this.Projects.Any(p => p.ProjectId == project.ProjectId))
        //    {
        //        throw new Exception(ExceptionMessages.ExistingProjectInEmployee);
        //    }

        //    this.Projects.Add(project);
        //}

        //public bool CheckIfEmployeeHasProject(int projectId)
        //{
        //    var result = this.Projects.Any(p => p.ProjectId == projectId);

        //    return result;
        //}

        private int GetDurationInDays()
        {
            var durationInDays = (this.EndDate - this.StartDate).Days;
            return durationInDays;
        }
    }
}
