using EmployeePairs.Controllers;
using EmployeePairs.Exceptions;
using EmployeePairs.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EmployeePairs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            

        }

        private static void FillTableWithInitialDataFromFile(DataTable dt, List<Project> initialProjects)
        {
            foreach (var project in initialProjects)
            {
                foreach (var employee in project.Employees)
                {
                    var arr = new string[]
                    {
                            employee.EmployeeId.ToString(),
                            project.ProjectId.ToString(),
                            employee.StartDate.ToString(),
                            employee.EndDate.ToString()
                    };

                    dt.Rows.Add(arr);

                }
            }
        }

        

        private void btnChooseFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();



            using (var reader = new StreamReader(fileDialog.FileName))
            {

                ProjectController projectController = new ProjectController(reader);

                projectController.ImplementProjectFromFile();


                //CLOSE PROGRAM IF FILE IS EMPTY OR DIFFERENT FORMAT
                if (projectController.GetProjects().Count == 0)
                {
                    MessageBox.Show(ExceptionMessages.EmptyChosenFile,"Project", MessageBoxButton.OK, MessageBoxImage.Information);
                    Environment.Exit(0);
                }


                var initialProjects = projectController.GetProjects();

                //POPULATE FIRST TABLE WITH ALL VALID RECORDS
                DataTable dt = new DataTable();
                dt.Columns.Add("Employee Id", typeof(string));
                dt.Columns.Add("Project Id", typeof(string));
                dt.Columns.Add("Start Date", typeof(string));
                dt.Columns.Add("End Date", typeof(string));

                FillTableWithInitialDataFromFile(dt, initialProjects);

                DataView dv = new DataView(dt);
                dtGrid.ItemsSource = dv;


                //POPULATE SECOND TABLE WITH THE BEST EMPLOYEE PROJECT PAIR
                var longestProjectIntersectionPair = projectController.GetLongestCommonPairProject();  

                var longestIntersectionData = new string[]
                    {
                            longestProjectIntersectionPair.FirstEmployeeId.ToString(),
                            longestProjectIntersectionPair.SecondEmployeeId.ToString(),
                            longestProjectIntersectionPair.ProjectId.ToString(),
                            longestProjectIntersectionPair.TotalDays.ToString()
                    };

                DataTable dtBest = new DataTable();
                dtBest.Columns.Add("Employee ID #1", typeof(string));
                dtBest.Columns.Add("Employee ID #2", typeof(string));
                dtBest.Columns.Add("Project ID", typeof(string));
                dtBest.Columns.Add("Days Worked", typeof(string));

                dtBest.Rows.Add(longestIntersectionData);

                DataView dv2 = new DataView(dtBest);
                bestResultGrid.ItemsSource = dv2;



                //POPULATE THIRD TABLE WITH BEST PAIR EMPLOEES OTHER COMMON PROJECTS
                DataTable dtAllBest = new DataTable();
                dtAllBest.Columns.Add("Employee ID #1", typeof(string));
                dtAllBest.Columns.Add("Employee ID #2", typeof(string));
                dtAllBest.Columns.Add("Project ID", typeof(string));
                dtAllBest.Columns.Add("Days Worked", typeof(string));

                var allCommonProjects = projectController.GetAllCommonProjectsOfTopPair().ToList();
                for (int i = 0; i < allCommonProjects.Count(); i++)
                {
                    var currProject = allCommonProjects[i];

                    if (currProject.ProjectId == longestProjectIntersectionPair.ProjectId)
                    {
                        continue;
                    }
                    var projectArr = new string[]
                    {
                            currProject.FirstEmployeeId.ToString(),
                            currProject.SecondEmployeeId.ToString(),
                            currProject.ProjectId.ToString(),
                            currProject.TotalDays.ToString()
                    };

                    dtAllBest.Rows.Add(projectArr);
                }
                DataView allCommonDataView = new DataView(dtAllBest);
                commonGrid.ItemsSource = allCommonDataView;

                //ERROR MESSAGE IF THERE IS NO OTHER COMMON PROJECTS
                if (dtAllBest.Rows.Count < 1) noDataLabel.Visibility = Visibility.Visible;
                else noDataLabel.Visibility = Visibility.Hidden;



            }
        }

        private void btnCloseProgram_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dtGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
    }
}
