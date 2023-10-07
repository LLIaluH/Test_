using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace Test_.Models
{
    public static class WorkWithData
    {
        public static string ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=I:\Projects\Test_\Test_\Data\TestChart.mdf;Integrated Security=True";
        static WorkWithData()
        {
            //ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
        }
    }
}
