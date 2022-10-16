using Domain.Interfaces;
using Domain.Entities;
using MediatR;
using AutoMapper;
using Application.Dtos;
using System.Net;
using Domain.Errors;

namespace Application.EmployeeService.Queries
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, Dtos.EmployeeDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICustomException _customException;

        public GetEmployeeByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, ICustomException customException)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _customException = customException;
        }

        public async Task<Dtos.EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {   
            var employee = await _unitOfWork.Repository<Employee>().GetByIdAsync(request.Id);
            var data = _mapper.Map<Employee, EmployeeDto>(employee);
            if(data == null)
            {
                _customException.ThrowItemNotFoundException("Employee not found");
            }
            return data;
        }
    }
}
