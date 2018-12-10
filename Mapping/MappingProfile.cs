using AutoMapper;
using GoGo.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViewModels;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<DestViewModel, Destination >().ReverseMap();
            CreateMap<GoUserViewModel, GoUser>().ReverseMap();
            CreateMap<CurrentUserViewModel, GoUser>().ReverseMap();
            CreateMap<DestDetailsViewModel, Destination>().ReverseMap();
            CreateMap<CourceViewModel, Cource>().ReverseMap();
            CreateMap<UsersResultsViewModel, CourcesUsers>().ReverseMap()
                .ForMember(x => x.Result, x => x.MapFrom(d => d.StatusUser)); ;
            
            CreateMap<CreateCourceViewModel, Cource>()
                .ForMember(d => d.Image, d => d.Ignore())
                .ForMember(d => d.Creator, d => d.Ignore());

            CreateMap<CommentViewModel, Comment>().ReverseMap();
            CreateMap<CreateDestinationViewModel, Destination>()
                .ForMember(d => d.Image, d => d.Ignore());
            CreateMap<CreateStoryViewModel, Story>().ReverseMap()
                //.ForMember(x => x.PeopleWhosLikeThis, x => x.MapFrom(d => d.PeopleWhosLikeThis.Count()))
                /*.ForMember(x => x.Author, x => x.MapFrom(d => d.Author))*/;
            CreateMap<StoryViewModel, Story>().ReverseMap()
                .ForMember(x => x.PeopleWhosLikeThis, x => x.MapFrom(d => d.PeopleWhosLikeThis.Count()));



        }
        //private static byte[] ImageToByteArray(CreateDestinationViewModel model)
        //{
        //    byte[] file = null;
        //    if (model.Image.Length > 0)
        //    {
        //        using (var ms = new MemoryStream())
        //        {
        //            model.Image.CopyTo(ms);
        //            file = ms.ToArray();
        //        }
        //    }

        //    return file;
        //}
    }
    
}
