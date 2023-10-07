namespace Test_.Models
{
    public class EmployeeTasks
    {
        public Guid Id { get; set; }
        public string TaskText { get; set; }
        public int? LaborIntensity { get; set; }
        public string ImportantTask { get; set; }
        public Employee Employe { get; set; }
    }
}
