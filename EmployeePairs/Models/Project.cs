
using EmployeePairs.Contracts;
using EmployeePairs.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Models
{
    internal class Project : IProject
    {
        public Project()
        {
            this.Employees = new List<IEmployee>();
        }

        public Project(int projectId)
            : this()
        {
            this.ProjectId = projectId;

        }
        public int ProjectId { get; private set; }


        public List<IEmployee> Employees { get; private set; }

        public void AddEmployeeToProject(IEmployee employee)
        {
            if (this.Employees.Any(e => e.EmployeeId == employee.EmployeeId))
            {
                throw new Exception(ExceptionMessages.ExistingEmployeeInProject);
            }

            this.Employees.Add(employee);
        }

        public string GetTopPairOfProject()
        {

            var getLongestIntersectionOfEmployees = this.MostInclusiveDaysBetweenTwoEmployees();
            if (getLongestIntersectionOfEmployees.Count == 0)
            {
                return String.Empty;
            }

            var firstEmp = getLongestIntersectionOfEmployees[0];
            var secondEmp = getLongestIntersectionOfEmployees[1];
            var totalDays = int.Parse(getLongestIntersectionOfEmployees[2]);
            var projectId = getLongestIntersectionOfEmployees[3];


            var resultString = $"{firstEmp} {secondEmp} {projectId} {totalDays}";

            return resultString;
        }
        private List<string> MostInclusiveDaysBetweenTwoEmployees()
        {

            if (this.Employees.Count < 2)
            {
                return new List<string>();
            }
            var currentBestIntersection = 0;
            var firstEmpId = String.Empty;
            var secondEmpId = String.Empty;

            for (int i = 0; i < this.Employees.Count - 1; i++)
            {
                var currFirstEmp = this.Employees[i];
                for (int j = 1; j < this.Employees.Count; j++)
                {
                    var currSecondEmp = this.Employees[j];


                    if (!(currFirstEmp.StartDate <= currSecondEmp.EndDate && 
                        currFirstEmp.EndDate >= currSecondEmp.StartDate))
                    {
                        continue;
                    }
                    DateTime start = currFirstEmp.StartDate > currSecondEmp.StartDate ? currFirstEmp.StartDate : currSecondEmp.StartDate;
                    DateTime end = currFirstEmp.EndDate > currSecondEmp.EndDate ? currSecondEmp.EndDate : currFirstEmp.EndDate;

                    var totalDays =  (int)(end - start).TotalDays + 1;

                    if (totalDays > currentBestIntersection)
                    {
                        currentBestIntersection = totalDays;
                        firstEmpId = currFirstEmp.EmployeeId.ToString();
                        secondEmpId = currSecondEmp.EmployeeId.ToString();
                    }
                }
            }

            var list = new List<string>();
            list.Add(firstEmpId);
            list.Add(secondEmpId);
            list.Add(currentBestIntersection.ToString());
            list.Add(this.ProjectId.ToString());

            return list;
            
        }

        public UniqueProject GetTopCommonPairProjects(int firstEmployeeId, int secondEmployeeId)
        {
            var defaultResult = new UniqueProject(-1, -1, -1, -1);
            if (this.Employees.Count < 2)
            {
                return defaultResult;
            }

            var employeesIds = this.Employees.Select(e => e.EmployeeId).ToList();

            if (employeesIds.Contains(firstEmployeeId) && employeesIds.Contains(secondEmployeeId))
            {

                var getEmployeeOne = this.Employees.Where(e => e.EmployeeId == firstEmployeeId).FirstOrDefault();
                var getEmployeeTwo = this.Employees.Where(e => e.EmployeeId == secondEmployeeId).FirstOrDefault();


                if (!(getEmployeeOne.StartDate <= getEmployeeTwo.EndDate &&
                    getEmployeeOne.EndDate >= getEmployeeTwo.StartDate))
                {
                    return defaultResult;
                }
                DateTime start = getEmployeeOne.StartDate > getEmployeeTwo.StartDate ? getEmployeeOne.StartDate : getEmployeeTwo.StartDate;
                DateTime end = getEmployeeOne.EndDate > getEmployeeTwo.EndDate ? getEmployeeTwo.EndDate : getEmployeeOne.EndDate;

                var totalDays = (int)(end - start).TotalDays + 1;

                var result = new UniqueProject(getEmployeeOne.EmployeeId, getEmployeeTwo.EmployeeId, this.ProjectId, totalDays);

                return result;
            }

            return defaultResult;




        }

    }
}
