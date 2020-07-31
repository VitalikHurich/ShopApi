using AutoMapper;
using ShopApi.BLL.DTO;
using ShopApi.Core.Domain;
using ShopApi.DAL.Models;

namespace ShopApi.Root.Converters
{
    public class PagedListConverter : ITypeConverter<PagedList<Good>, PagedList<GoodDTO>>
    {
        public PagedList<GoodDTO> Convert(PagedList<Good> source, PagedList<GoodDTO> destination,
            ResolutionContext context)
        {
            if (destination == null)
            {
                destination = new PagedList<GoodDTO>();
            }
            foreach (var item in source)
            {
                var dest = context.Mapper.Map<Good, GoodDTO>(item);
                destination.Add(dest);
            }
            destination.PageSize = source.PageSize;
            destination.TotalCount = source.TotalCount;
            destination.CurrentPage = source.CurrentPage;
            destination.TotalPages = source.TotalCount;

            return destination;
        }
    }
}