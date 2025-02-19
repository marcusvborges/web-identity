namespace WebIdentity.Entities
{
    public class Department
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual ICollection<Sector> Sectors { get; set; } = new List<Sector>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
