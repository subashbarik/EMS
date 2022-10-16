
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public Boolean RemoveImage { get; set; }
    }
}
