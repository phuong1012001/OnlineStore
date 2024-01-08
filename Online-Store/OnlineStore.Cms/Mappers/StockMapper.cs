using AutoMapper;
using OnlineStore.BusinessLogic.Dtos.Product;
using OnlineStore.BusinessLogic.Dtos.Stock;
using OnlineStore.Cms.Models.Request.Stock;
using OnlineStore.Cms.Models.Response.Stock;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.Cms.Mappers
{
    public class StockMapper : Profile
    {
        public StockMapper()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<StockDto, StockRes>();
            CreateMap<StockEventReq, StockEventDto>();
            CreateMap<StockEventDto, StockEvent>();
        }
    }
}
