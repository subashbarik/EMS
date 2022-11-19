using AutoMapper;
using Application.Dtos;
using Domain.Entities;

namespace Application.Helpers
{
    public class CreatedDateResolver<TSource, TDestination> : IValueResolver<TSource, TDestination, DateTime>
                where TSource : BaseEntityDto 
                where TDestination : BaseEntity
    {
        public DateTime Resolve(TSource source, TDestination destination, DateTime destMember, ResolutionContext context)
        {
            return DateTime.UtcNow;
        }
    }
}