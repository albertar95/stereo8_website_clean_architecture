using Application.DTO.Brand;
using Application.DTO.Cart;
using Application.DTO.Category;
using Application.DTO.Comment;
using Application.DTO.Favorite;
using Application.DTO.File;
using Application.DTO.Order;
using Application.DTO.Product;
using Application.DTO.Ship;
using Application.DTO.Type;
using Application.Helper;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            //brand
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, BrandListDto>().ReverseMap();
            CreateMap<Brand, CreateBrandDto>();
            CreateMap<CreateBrandDto, Brand>().BeforeMap((c,b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Brand, UpdateBrandDto>();
            CreateMap<UpdateBrandDto, Brand>().BeforeMap((u,b) => { b.LastModified = DateTime.Now; b.PersianLastModified = Commons.GetPersianDate(b.LastModified ?? DateTime.Now); });
            //category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryListDto>().ForMember("ProductCount",m => m.MapFrom(x => x.Products.Count)).ReverseMap();
            CreateMap<Category, CreateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Category, UpdateCategoryDto>();
            CreateMap<UpdateCategoryDto, Category>().BeforeMap((u, b) => { b.LastModified = DateTime.Now; b.PersianLastModified = Commons.GetPersianDate(b.LastModified ?? DateTime.Now); });
            //product
            CreateMap<Product, CreateProductDto>();
            CreateMap<CreateProductDto, Product>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Product, UpdateProductDto>();
            CreateMap<UpdateProductDto, Product>().BeforeMap((u, b) => { b.LastModified = DateTime.Now; b.PersianLastModified = Commons.GetPersianDate(b.LastModified ?? DateTime.Now); });
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, DetailProductDto>().ForMember("CategoryName", m => m.MapFrom(x => x.Category.CategoryName)).ForMember("TypeName", m => m.MapFrom(x => x.Type.TypeName)).ForMember("BrandName", m => m.MapFrom(x => x.Brand.BrandName)).ForMember("UserName", m => m.MapFrom(x => x.User.Username)).ReverseMap();
            CreateMap<Product, ProductListDto>().ForMember("TypeName", m => m.MapFrom(x => x.Type.TypeName)).ForMember("BrandName", m => m.MapFrom(x => x.Brand.BrandName)).ForMember("CategoryName", m => m.MapFrom(x => x.Category.CategoryName)).ReverseMap();
            //type
            CreateMap<Domain.Type, TypeDto>().ReverseMap();
            CreateMap<Domain.Type, CreateTypeDto>();
            CreateMap<CreateTypeDto, Domain.Type>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Domain.Type, UpdateTypeDto>();
            CreateMap<UpdateTypeDto, Domain.Type>().BeforeMap((u, b) => { b.LastModified = DateTime.Now; b.PersianLastModified = Commons.GetPersianDate(b.LastModified ?? DateTime.Now); });
            CreateMap<Domain.Type, TypeListDto>().ReverseMap();
            //file
            CreateMap<Domain.File, CreateFileDto>();
            CreateMap<CreateFileDto, Domain.File>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; b.Priority = 0; b.FileExtension = ".webp"; });
            CreateMap<Domain.File, FileDto>().ReverseMap();
            CreateMap<Domain.File, FileListDto>().ReverseMap();
            //comment
            CreateMap<Comment, CreateCommentDto>();
            CreateMap<CreateCommentDto, Comment>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Comment, CommentListDto>().ForMember("FirstName", m => m.MapFrom(x => x.User.FirstName)).ForMember("LastName", m => m.MapFrom(x => x.User.LastName)).ReverseMap();
            //cart
            CreateMap<Cart, CreateCartDto>();
            CreateMap<CreateCartDto, Cart>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Cart, UpdateCartDto>();
            CreateMap<UpdateCartDto, Cart>().BeforeMap((u, b) => { b.LastModified = DateTime.Now; b.PersianLastModified = Commons.GetPersianDate(b.LastModified ?? DateTime.Now); });
            CreateMap<Cart, CartListDto>().ForMember("ProductName", m => m.MapFrom(x => x.Product.ProductName)).ForMember("ProductPrice", m => m.MapFrom(x => x.Product.Price)).ForMember("AvailableCount", m => m.MapFrom(x => x.Product.AvailableCount)).ReverseMap();
            //fav
            CreateMap<Favorite, CreateFavoriteDto>();
            CreateMap<CreateFavoriteDto, Favorite>().BeforeMap((c, b) => { b.Id = Guid.NewGuid(); b.CreateDate = DateTime.Now; b.PersianCreateDate = Commons.GetPersianDate(b.CreateDate); b.State = 0; });
            CreateMap<Favorite, FavoriteListDto>().ForMember("ProductName", m => m.MapFrom(x => x.Product.ProductName)).ForMember("ProductPrice", m => m.MapFrom(x => x.Product.Price)).ForMember("AvailableCount", m => m.MapFrom(x => x.Product.AvailableCount)).ReverseMap();
            //order
            CreateMap<Order, OrderDto>().ForMember("Username", m => m.MapFrom(x => x.User.Username)).ForMember("ProductNames", m => m.MapFrom(x => x.OrderDetails.Select(x => x.Product.ProductName).ToList())).ReverseMap();
            CreateMap<Order, OrderListDto>().ReverseMap();
            //ship
            CreateMap<Ship, ShipListDto>().ForMember("FirstName", m => m.MapFrom(x => x.Order.FirstName)).ForMember("LastName", m => m.MapFrom(x => x.Order.LastName)).ForMember("StateId", m => m.MapFrom(x => x.Order.StateId)).ForMember("CityId", m => m.MapFrom(x => x.Order.CityId)).ReverseMap();
        }
    }
}
