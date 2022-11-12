using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.DepartmentService.Commands
{
    public class DeleteDepartmentHandler : IRequestHandler<DeleteDepartmentCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDepartmentHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
             var department = _mapper.Map<DepartmentDto, Department>(request.department);
            _unitOfWork.Repository<Department>().Delete(department);
            var retval = await _unitOfWork.Complete();
            return retval;
        }
    }
}