using API.Dto;
using AutoMapper;
using Entity.Models;

namespace API.Helper.Mapping
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<Course, CourseDto>()
                .ForMember(Cdto => Cdto.Category, opt => opt.MapFrom(C => C.Category.Name));

            CreateMap<Learning, LearningDto>();
            CreateMap<Requerment, RequermentDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoriesDto>();


            CreateMap<Basket, BasketDto>();
            CreateMap<BasketItems, BasketItemsDto>()
                .ForMember(dis => dis.Image, opt => opt.MapFrom(src => src.Course.Image))
                .ForMember(dis => dis.Instructor, opt => opt.MapFrom(src => src.Course.Instructor))
                .ForMember(dis => dis.Price, opt => opt.MapFrom(src => src.Course.Price))
                .ForMember(dis => dis.Rating, opt => opt.MapFrom(src => src.Course.Rating))
                .ForMember(dis => dis.Title, opt => opt.MapFrom(src => src.Course.Title))
                .ForMember(dis => dis.Description, opt => opt.MapFrom(src => src.Course.Description));


            CreateMap<Section, SectionDto>();
            CreateMap<Lecture, LectureDto>();

        
        
        }

    }
}
