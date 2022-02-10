

//using EmployeePairs.Controllers;
//using System;
//using System.IO;
//using System.Linq;

//string path = @"..\..\..\Input\input.cvs";

//using (var reader = new StreamReader(path))
//{
   
//    ProjectController projectController = new ProjectController(reader);

//    projectController.ImplementProjectFromFile();
    

//    var best = projectController.GenerateListOfUniqueProjectObjects()
//        .OrderByDescending(p => p.TotalDays)
//        .Take(1)
//        .FirstOrDefault();

//    Console.WriteLine($"{best.FirstEmployeeId}, {best.SecondEmployeeId}, {best.ProjectId}");


//}