using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Diagnostics;
using Test_.Models;

namespace Test_.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public string GetChartData()
        {
            using (SqlConnection connection = new SqlConnection(WorkWithData.ConnectionString))
            {
                connection.Open();
                string query = @"SELECT D.Name, 
                    t1.EmpCount AS 'Кол-во сотрудников в отделе',
                    t2.TaskCount AS 'Кол-во задач на сотрудников отдела',
                    t2.SumLaborIntensity AS 'Общая трудоемкость задач отдела',
                    t3.SumLaborIntensityImportantTasks AS 'Трудоемкость важных задач отдела'
                    FROM Department AS D
                    JOIN (
	                    Select D.Id AS DepartmentId, COUNT(Em.Id) AS EmpCount
	                    FROM Employes AS Em 
	                    JOIN Department AS D ON Em.DepartmentId = D.Id
	                    GROUP BY D.Id) AS t1 ON t1.DepartmentId = D.Id
                    JOIN (
	                    Select D.Id AS DepartmentId, COUNT(Et.Id) AS TaskCount, SUM(Et.LaborIntensity) AS SumLaborIntensity
	                    FROM Employes AS Em 
	                    JOIN Department AS D ON Em.DepartmentId = D.Id
	                    JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
	                    GROUP BY D.Id) AS t2 ON t2.DepartmentId = D.Id
                    JOIN (
	                    Select D.Id AS DepartmentId, SUM(Et.LaborIntensity) AS SumLaborIntensityImportantTasks
	                    FROM Employes AS Em 
	                    JOIN Department AS D ON Em.DepartmentId = D.Id
	                    JOIN EmployeeTasks AS Et ON Em.Id = Et.EmployeId
	                    WHERE Et.ImportantTask = 'Y'
	                    GROUP BY D.Id) AS t3 ON t3.DepartmentId = D.Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<object> result = new List<object>();
                        while (reader.Read())
                        {
                            result.Add(new { x = reader.GetValue(0), y = reader.GetValue(3) }); 
                        }
                        var colNames = new List<string>() { "Название отдела", "Общая трудоемкость задач отдела" };
                        object response = new {
                            columns = colNames.ToArray(),
                            table = result.ToArray()
                        };
                        return ConvertToJson(response);
                    }
                }
            }
        }

        [HttpGet]
        public string Ping()
        {
            return ConvertToJson("Pong");
        }

        private static string ConvertToJson(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}