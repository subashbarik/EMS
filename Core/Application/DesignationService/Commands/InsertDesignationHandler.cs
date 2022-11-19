using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.DesignationService.Commands
{
    public class InsertDesignationHandler : IRequestHandler<InsertDesignationCommand, DesignationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public InsertDesignationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DesignationDto> Handle(InsertDesignationCommand request, CancellationToken cancellationToken)
        {
            DesignationDto output = null;
            var designation = _mapper.Map<DesignationDto, Designation>(request.Designation);
            _unitOfWork.Repository<Designation>().Add(designation);
            var retval = await _unitOfWork.Complete();
            if (retval > 0)
            {
                output = _mapper.Map<Designation, DesignationDto>(designation);
            }
            return output;
        }
    }
}
