
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
    public class CreateDestinationViewModel //: IMapFrom<Destination>//, IHaveCustomMappings
    {
        [Required]
        [Display(Name = "Image")]
        [DataType(DataType.Upload)]
        [BindProperty]
        public IFormFile Image { get; set; }

        public LevelOfDifficulty Level { get; set; }

        public string Naame { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EndDateToJoin { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    
        //}
    }
    //public string Id { get; set; }
    //public byte[] Image { get; set; }
    //public LevelOfDifficulty Level { get; set; }
    //public string Naame { get; set; }
    //public string Description { get; set; }
    //public DateTime StartDate { get; set; }
    //public DateTime EndDate { get; set; }
    //public DateTime EndDateToJoin { get; set; }

    //public string CreatorId { get; set; }
    //public GoUser Creator { get; set; }
    //public ICollection<Acsesoar> Acsesoaries { get; set; }
    //public ICollection<Comment> Comments { get; set; }
    //public ICollection<DestinationsUsers> Participants { get; set; }
    //public ICollection<DestinationPhoto> Photos { get; set; }
    //public ICollection<Story> Stories { get; set; }
}
