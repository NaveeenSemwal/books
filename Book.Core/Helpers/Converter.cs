using AutoMapper;
using System.Collections.Generic;

namespace Books.Core.Helpers
{
    /// <summary>
    /// Conversion of custom list in Automapper
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    public class CustomConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>>
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
           return new PagedList<TDestination>(context.Mapper.Map<List<TSource>, List<TDestination>>(source));
        }
    }
}
