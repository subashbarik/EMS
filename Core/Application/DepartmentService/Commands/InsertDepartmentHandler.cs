using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.DepartmentService.Commands
{
    public class InsertDepartmentHandler : IRequestHandler<InsertDepartmentCommand, DepartmentDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InsertDepartmentHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DepartmentDto> Handle(InsertDepartmentCommand request, CancellationToken cancellationToken)
        {
            DepartmentDto output = null;
            var department = _mapper.Map<DepartmentDto,Department>(request.department);
            _unitOfWork.Repository<Department>().Add(department);
            var retval = await _unitOfWork.Complete();
            if (retval > 0)
            {
                output = _mapper.Map<Department, DepartmentDto>(department);
            }
            return output;
        }
    }
}