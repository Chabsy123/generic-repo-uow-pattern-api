using AutoMapper;
using generic_repo_pattern_api.Entity;
using generic_repo_pattern_api.ViewModel;

namespace generic_repo_uow_pattern_api.MapperProfile
{
    public class YourMappingProfile : Profile
    {
        public YourMappingProfile()
        {
            //CreateMap<entity, Dto>();
            CreateMap<Product, ProductRequest>().ReverseMap();
            //CreateMap<Order, OrderRequest>().ReverseMap();

            //CreateMap<Blog, BlogPostReqeust>().ReverseMap();
            //CreateMap<Post, PostRequest>().ReverseMap();
            //CreateMap<Post, PostResponse>().ReverseMap();
        }
    }
}
