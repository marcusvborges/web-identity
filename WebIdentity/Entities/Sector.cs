namespace WebIdentity.Entities
{
    public class Sector
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
