using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
namespace Application.DesignationService.Commands
{
    public class UpdateDesignationHandler : IRequestHandler<UpdateDesignationCommand, DesignationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateDesignationHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DesignationDto> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            DesignationDto output = null;
            var designation = _mapper.Map<DesignationDto, Designation>(request.Designation);
            _unitOfWork.Repository<Designation>().Update(designation);
            var retval = await _unitOfWork.Complete();
            if (retval > 0)
            {
                var updatedDesignation = await _unitOfWork.Repository<Designation>().GetByIdAsync(designation.Id,cancellationToken);
                output = _mapper.Map<Designation, DesignationDto>(updatedDesignation);
            }
            return output;
        }
    }
}
