
using GoGo.Models;
using GoGo.Models.Enums;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class CreateCourceViewModel //: IMapFrom<Cource>//, IHaveCustomMappings
    {
        //public string Id { get; set; }
        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxCountParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationOfDays { get; set; }
        public int CountOfHours { get; set; }
        public GoUserViewModel Creator { get; set; }
        public Status Status { get; set; }
        public Category Category { get; set; }

        //public string Id { get; set; }
        //public byte[] Image { get; set; }
        //public string Title { get; set; }
        //public string Description { get; set; }
        //public int MaxCountParticipants { get; set; }
        //public DateTime StartDate { get; set; }
        //public int DurationOfDays { get; set; }
        //public int CountOfHours { get; set; }
        //public string CreatorId { get; set; }
        //public GoUser Creator { get; set; }
        //public Status Status { get; set; }
        //public Category Category { get; set; }
        //public ICollection<CourcesUsers> Participants { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    //configuration.CreateMap<CreateCourceViewModel,Cource>().ReverseMap()
        //    //    .ForMember(x => x.Image, x => x.MapFrom(d => d.Image));
        //}


    }
}
