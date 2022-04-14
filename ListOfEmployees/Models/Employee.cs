namespace ListOfEmployees.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string FirstMidName { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organizations { get; set; }
        public int SubdivisionId { get; set; }
        public int PositionId { get; set; }
        public Position Positions { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
