using AutoMapper;
using Application.Dtos;
using Domain.Entities;

namespace Application.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(e => e.ImageUrl, ed => ed.MapFrom<EmployeeDtoUrlResolver>())
                .ForMember(ed => ed.DepartmentName,e => e.MapFrom(e => e.Department.Name))
                .ForMember(ed => ed.DesignationName, e => e.MapFrom(e => e.Designation.Name));
                
            CreateMap<EmployeeDto, Employee>()
                .ForMember(ed => ed.ImageUrl, e => e.MapFrom<EmployeeNoImageResolver>());
            CreateMap<EmployeeDto, EmployeeDto>()
                .ForMember(e => e.ImageUrl, ed => ed.MapFrom<EmployeeUrlResolver>());

            CreateMap<DepartmentDto, Department>()
                            .ForMember(dd => dd.UpdatedDate,
                                    d => d.MapFrom<UpdatedDateResolver<DepartmentDto, Department>>())
                            .ForMember(dd => dd.UpdatedUTCDate,
                                d => d.MapFrom<UpdatedUTCDateResolver<DepartmentDto, Department>>());
                            

            CreateMap<Department,DepartmentDto>();
            CreateMap<DesignationDto, Designation>()
                    .ForMember(dd => dd.UpdatedDate,
                                d => d.MapFrom<UpdatedDateResolver<DesignationDto, Designation>>())
                    .ForMember(dd => dd.UpdatedUTCDate,
                                d => d.MapFrom<UpdatedUTCDateResolver<DesignationDto, Designation>>());
                    
            CreateMap<Designation, DesignationDto>();
        }
    }
}
