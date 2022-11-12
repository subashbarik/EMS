using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.DepartmentService.Commands
{   
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDepartmentHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }      
        public async Task<DepartmentDto> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            DepartmentDto output = null;
            var department = _mapper.Map<DepartmentDto, Department>(request.department);
                _unitOfWork.Repository<Department>().Update(department);
                var retval = await _unitOfWork.Complete();
                if (retval > 0)
                {
                    var updatedDepartment = await _unitOfWork.Repository<Department>().GetByIdAsync(department.Id);
                    output = _mapper.Map<Department, DepartmentDto>(updatedDepartment);
                }
            return output;
        }
    }
}