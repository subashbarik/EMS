using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.DesignationService.Commands
{
    internal class DeleteDesignationHandler : IRequestHandler<DeleteDesignationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteDesignationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
        {
            var designation = _mapper.Map<DesignationDto, Designation>(request.Designation);
            _unitOfWork.Repository<Designation>().Delete(designation);
            var retval = await _unitOfWork.Complete();
            return retval;
        }
    }
}
