
using EmployeePairs.Factories;
using EmployeePairs.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePairs.Controllers
{
    internal class ProjectController
    {

        private List<Project> projects;
        private ProjectFactory projectFactory;
        private StreamReader reader;

        public ProjectController(StreamReader reader)
        {
            this.projectFactory = new ProjectFactory();
            this.projects = new List<Project>();
            this.reader = reader;
        }

        public List<Project> GetProjects()
        {
            return this.projects;
        }
        public void ImplementProjectFromFile()
        {
            while (!this.reader.EndOfStream)
            {
                var line = this.reader.ReadLine();


                var values = line?
                    .Split(new[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                if (values.Count < 4 || values == null) continue;


                var projectId = int.Parse(values[1]);


                //CHECK IF PROJECT EXISTS AND CREATES IT IF NOT
                bool projectExist = this.projects.Any(e => e.ProjectId == projectId);
                if (!projectExist)
                {
                    var createdProject = projectFactory.CreateProject(projectId);
                    this.projects.Add(createdProject);
                }


                var empId = int.Parse(values[0]);
                var checkIfProjectHasEmployee = this.projects
                    .FirstOrDefault(p => p.ProjectId == projectId)
                    .Employees.Any(e => e.EmployeeId == empId);


                if (!checkIfProjectHasEmployee)
                {
                    var startDate = DateTime.Parse(values[2]);
                    var endDate = DateTime.Now;
                    if (values[3] != "NULL") endDate = DateTime.Parse(values[3]);
                    var createdEmployee = new Employee(empId, startDate, endDate);

                    this.projects
                        .FirstOrDefault(p => p.ProjectId == projectId)
                        .AddEmployeeToProject(createdEmployee);

                }

            }
        }

        public List<string> GetLongestValidSetForEveryProject()
        {
            //CHECK FOR PAIR OF EMPLOYEES FROM PROJECT WITH LONGEST DURATION
            var validProjects = this.projects
                .Where(p => p.Employees.Count >= 2)
                .Select(p => p.GetTopPairOfProject())
                .ToList();

            return validProjects;
        }

        public List<UniqueProject> GenerateListOfUniqueProjectObjects()
        {
            var listOfBestProjectTimeSpan = new List<UniqueProject>();

            var validProjects = this.GetLongestValidSetForEveryProject();
            foreach (var project in validProjects)
            {
                var currProject = project
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

                var firstEmployee = int.Parse(currProject[0]);
                var secondEmployee = int.Parse(currProject[1]);
                var empProject = int.Parse(currProject[2]);
                var empTotalDays = int.Parse(currProject[3]);

                var currUnique = new UniqueProject(firstEmployee, secondEmployee, empProject, empTotalDays);

                listOfBestProjectTimeSpan.Add(currUnique);
            }

            return listOfBestProjectTimeSpan;
        }

        public UniqueProject GetLongestCommonPairProject()
        {
            var longestCommonPair = this.GenerateListOfUniqueProjectObjects()
                .OrderByDescending(p => p.TotalDays)
                .Take(1)
                .FirstOrDefault();

            return longestCommonPair;
        }

        public List<UniqueProject> GetAllCommonProjectsOfTopPair()
        {
            var topPair = this.GetLongestCommonPairProject();

            var employeeOne = topPair.FirstEmployeeId;
            var employeeTwo = topPair.SecondEmployeeId;

            var listOfProjects = new List<UniqueProject>();
            foreach (var project in this.projects)
            {
                var currCommon = project.GetTopCommonPairProjects(employeeOne, employeeTwo);
                if (currCommon.FirstEmployeeId != -1)
                {
                    listOfProjects.Add(currCommon);

                }
            }


            return listOfProjects;

        }
    }
}
