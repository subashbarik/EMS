using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos;
using MediatR;

namespace Application.CompanyService.Queries
{
    public sealed record GetCompanyInfoQuery():IRequest<CompanyDto>;
    
}