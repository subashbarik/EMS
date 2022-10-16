using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.EmployeeService.Commands
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteEmployeeHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<EmployeeDto, Employee>(request.employee);
            _unitOfWork.Repository<Employee>().Delete(employee);
            var retval = await _unitOfWork.Complete();
            return retval;
        }
    }
}
