
using GoGo.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class CommentViewModel //: IMapFrom<Comment>, IHaveCustomMappings
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string ComentatorId { get; set; }
        public virtual GoUserViewModel Comentator { get; set; }
        public string DestinationId { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    //configuration.CreateMap<Comment, CommentViewModel>()
        //    //    .ForMember(x => x.Comentator, x => 
        //    //                x.MapFrom(d => configuration.CreateMap<GoUser, GoUserViewModel>()
        //    //                .ForMember(u=>u.FirstName, u=>u.MapFrom(us => us.FirstName))
        //    //                .ForMember(u => u.Image, u => u.MapFrom(us => us.Image))
        //    //                //.ForMember(u => u.Id, u => u.MapFrom(us => us.Id))
        //    //                ));
        //}
    }
}
