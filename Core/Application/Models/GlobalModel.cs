using Application.Dtos;
using Application.Options;

namespace Application.Models
{
    public class GlobalModel
    {
        public AppConfigurationOptions ServerAppConfigurations { get; set; }
        public CompanyDto companyInfo {get;set;}
        //public Dictionary<string, object> CnfList { get; set; }
    }
}
