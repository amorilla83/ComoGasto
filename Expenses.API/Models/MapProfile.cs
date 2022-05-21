﻿using System;
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
            CreateMap<Brand, BrandModel>();
            CreateMap<Format, FormatModel>();
            CreateMap<Purchase, PurchaseModel>()
                .ForMember(p =>
                p.Count,
                opt => opt.MapFrom(src => src.ProductList.Count));

            CreateMap<AddPurchaseModel, Purchase>()
                .ForMember(p =>
                p.StoreId,
                opt => opt.MapFrom(src => src.Store.Id))
                .ForMember(p =>
                p.Store,
                opt => opt.Ignore());
            CreateMap<AddProductPurchaseModel, ProductPurchase>();
            CreateMap<ProductPurchaseModel, ProductPurchase>();
            CreateMap<ProductPurchase, ProductPurchaseModel>();
        }

    }
}