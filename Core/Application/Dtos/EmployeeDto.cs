
using Microsoft.AspNetCore.Http;

namespace Application.Dtos
{
    public class EmployeeDto: BaseEntityDto
    {   
        public string UserID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        public DateTime HireDate { get; set; }
        public string Sex { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string ContactNo { get; set; }
        public double Basic { get; set; }
        public double TAPercentage { get; set; }
        public double DAPercentage { get; set; }
        public double HRAPercentage { get; set; }
        public double Salary { get; set; }
        public string ImageUrl { get; set; }
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int EmployeeTypeId { get; set; }
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string EmployeeTypeName { get; set; }
        public Boolean RemoveImage { get; set; }
        
    }
}
