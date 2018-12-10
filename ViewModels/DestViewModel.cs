
using GoGo.Models;
using GoGo.Models.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DestViewModel //: IMapFrom<Destination>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public byte[] Image { get; set; }

        public LevelOfDifficulty Level { get; set; }

        public string Naame { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EndDateToJoin { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    //configuration.CreateMap<Destination, DestViewModel>()
        //    //    .ForMember(x => x.Naame, x => x.MapFrom(d => d.Naame));
        //}
    }

    
}
