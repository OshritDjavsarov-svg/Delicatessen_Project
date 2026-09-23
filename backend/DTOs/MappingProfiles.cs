using AutoMapper;
using DAL.models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    // מחלקת המיפוי - כאן קורה כל המיפוי של כל מחלקה מהדאטה
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            //var config = new MapperConfiguration(cfg =>
            //{
            //    cfg.AddProfile<MappingProfiles>();
            //});

            //IMapper = config.CreateMapper();

            // מיפוי מחלקת מוצר של הדאטה - למחלקת כרטיס של מוצר
            CreateMap<Product, ProductCardDto>()
                .ForMember(dest => dest.CategoryName,opt => opt.MapFrom(src => src.Category.CategoryName)).ReverseMap();
            
            // מיפוי מחלקת מוצר של הדאטה - למחלקת תצוגת מוצר
            CreateMap<Product, ProductShowDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.HighQuantity, opt => opt.MapFrom(src =>
                    src.HighQuantities.Select(hq => new int[] {
                        hq.HighSalt ? 1 : 0,
                        hq.HighSugar ? 1 : 0,
                        hq.HighFat ? 1 : 0
                                }).FirstOrDefault() ?? new int[] { 0, 0, 0 }))
                .ReverseMap();

            // מיפוי מחלקות הדאטה - למחלקות ללא הקשרי גומלין
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<HighQuantity, HighQuantityDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();


            // מיפוי מחלקת משתמש של הדאטה - למחלקת הרשמת משתמש 
            CreateMap<UserRegisterDto, Customer>().ForMember(dest => dest.PasswordHash, opt => opt.Ignore()).ReverseMap();

            // מיפוי מחלקת משתמש של הדאטה - למחלקת תגובה למשתמש
            CreateMap<Customer, UserResponseDto>().ReverseMap();

            // מיפוי OrderEntity ל-CartResponseDto
            CreateMap<Order, CartResponseDto>()
                // שינוי שם השדה OrderItems ל-Items לצורך הפשטה
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.OrderItems))
                .ReverseMap();

            // מיפוי OrderItemEntity ל-OrderItemResponseDto
            CreateMap<OrderItem, OrderItemResponseDto>()
                // חובה למפות את שם המוצר והמחיר היחידתי
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.ProductName)) // דורש Include!
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.OrderItemPrice))
                .ReverseMap();
        }
    }
}
