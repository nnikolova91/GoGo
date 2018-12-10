
using GoGo.Models;
using GoGo.Models.Enums;

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DestDetailsViewModel //: IMapFrom<Destination>//, IHaveCustomMappings
    {
        public DestDetailsViewModel()
        {
            this.AllComments = new List<CommentViewModel>();
            this.ParticipantsKnowSomeone = new List<GoUserViewModel>();
            this.ParticipantsNotKnowAnyone = new List<GoUserViewModel>();
        }

        public string Id { get; set; }

        public byte[] Image { get; set; }

        public CurrentUserViewModel CurrentUser { get; set; } //

        public string CurrentComment { get; set; } //

        public LevelOfDifficulty Level { get; set; }

        public string Naame { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime EndDateToJoin { get; set; }

        public string Creator { get; set; } //

        public Socialization Socialization { get; set; }

        public ICollection<CommentViewModel> AllComments { get; set; }

        public ICollection<StoryViewModel> Stories { get; set; }

        public ICollection<GoUserViewModel> ParticipantsKnowSomeone { get; set; }

        public ICollection<GoUserViewModel> ParticipantsNotKnowAnyone { get; set; }
        //public string Id { get; set; }
        //
        //public byte[] Image { get; set; }
        //
        //public LevelOfDifficulty Level { get; set; }
        //
        //public string Naame { get; set; }
        //
        //public string Description { get; set; }
        //
        //public DateTime StartDate { get; set; }
        //
        //public DateTime EndDate { get; set; }
        //
        //public DateTime EndDateToJoin { get; set; }
        //
        //public string CreatorId { get; set; }
        //public GoUser Creator { get; set; }
        //
        //public ICollection<Acsesoar> Acsesoaries { get; set; }
        //
        //public ICollection<Comment> Comments { get; set; }
        //
        //public ICollection<DestinationsUsers> Participants { get; set; }
        //
        //public ICollection<DestinationPhoto> Photos { get; set; }
        //
        //public ICollection<Story> Stories { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    //configuration.CreateMap<Destination, DestDetailsViewModel>()
        //    //    .ForMember(x => x.Creator, x => x.MapFrom(d => d.Creator.FirstName))
        //    //    .ForMember(x=> x.Stories, x=>x.MapFrom(d=>d.Stories));
        //}
    }
}
