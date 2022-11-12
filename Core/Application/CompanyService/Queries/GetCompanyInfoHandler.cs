using Application.Dtos;
using Domain.Interfaces;
using MediatR;

namespace Application.CompanyService.Queries
{
    public class GetCompanyInfoHandler : IRequestHandler<GetCompanyInfoQuery, CompanyDto>
    {
        private readonly IGenericDapperRepository _dapper;
        
        public GetCompanyInfoHandler(IGenericDapperRepository dapper,IMediator mediator)
        {
            _dapper = dapper;
        }
        public async Task<CompanyDto> Handle(GetCompanyInfoQuery request, CancellationToken cancellationToken)
        {
            CompanyDto output = null;
            var result = await _dapper.LoadData<CompanyDto,dynamic>("usp_GetCompanyDetails",new {});
            output = result.FirstOrDefault();
            return output;
        }
    }
}