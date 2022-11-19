using AutoMapper;
using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class UpdatedDateResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, DateTime>
                where TSource : BaseEntityDto 
                where TDestination : BaseEntity
    {
        public DateTime Resolve(TSource source, TDestination destination, DateTime destMember, ResolutionContext context)
        {
            return DateTime.Now;
        }
    }
}