using System;
using AutoMapper;
using Expenses.API.Models.Brands;
using Expenses.API.Models.Stores;
using Expenses.Core.Entities;

namespace Expenses.API.Models
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Store, StoreModel>()
                .ForMember(d =>
                d.Id,
                opt => opt.MapFrom(src => src.StoreId)).ReverseMap();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductDetails, ProductDetailsModel>();
            CreateMap<Brand, BrandModel>();
            CreateMap<Format, FormatModel>();
            CreateMap<Brand, ItemModel>();
            CreateMap<Format, ItemModel>();
            CreateMap(typeof(PaginatedEntity<>), typeof(PaginatedEntity<>));
            CreateMap<Purchase, PurchaseModel>()
                .ForMember(p =>
                p.Count,
                opt => opt.MapFrom(src => src.ProductList.Count))
                .ForMember(p =>
                p.IdPurchase,
                opt => opt.MapFrom(src => src.Id));

            CreateMap<PurchaseModel, Purchase>()
                .ForMember(p => p.Date, opt => opt.MapFrom(src => Convert.ToDateTime(src.DateString)))
                .ForMember(p => p.StoreId, opt => opt.MapFrom(src => src.Store.Id))
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.IdPurchase));

            CreateMap<AddPurchaseModel, Purchase>()
                .ForMember(p =>
                p.StoreId,
                opt => opt.MapFrom(src => src.Store.Id))
                .ForMember(p => p.Date, opt => opt.MapFrom(src => Convert.ToDateTime(src.DateString)))
                .ForMember(p =>
                p.Store,
                opt => opt.Ignore());
            CreateMap<AddProductPurchaseModel, ProductPurchase>();
            CreateMap<AddProductDetailsModel, ProductDetails>();
            CreateMap<ProductDetailsModel, ProductDetails>();
            CreateMap<ProductPurchaseModel, ProductPurchase>();
            CreateMap<ProductPurchase, ProductPurchaseModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<ItemModel, Brand>();
            CreateMap<ItemModel, Format>();
        }

    }
}
