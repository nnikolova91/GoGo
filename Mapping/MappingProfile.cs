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
        public const string SpaceSeparetedUsersFirstAndLastName = " ";

        public MappingProfile()
        {
            CreateMap<DestViewModel, Destination>().ReverseMap();

            CreateMap<GoUserViewModel, GoUser>().ReverseMap()
                .ForMember(u => u.StatusParticitant, u => u.Ignore());

            CreateMap<CurrentUserViewModel, GoUser>().ReverseMap();

            CreateMap<DestDetailsViewModel, Destination>().ReverseMap();

            CreateMap<CourseViewModel, Course>().ReverseMap();

            CreateMap<CourseDetailsViewModel, Course>().ReverseMap()
                .ForMember(d => d.Participants, d => d.Ignore());

            CreateMap<GameViewModel, Game>().ReverseMap();

            CreateMap<GameLevelParticipantViewModel, GameLevelParticipant>().ReverseMap()
                .ForMember(x => x.Participant, x => x.MapFrom(t => t.Participant.FirstName +
                 SpaceSeparetedUsersFirstAndLastName + t.Participant.LastName));

            CreateMap<ThemDetailsViewModel, Theme>().ReverseMap()
                .ForMember(x => x.Author, x => x.MapFrom(t => t.Author.FirstName +
                 SpaceSeparetedUsersFirstAndLastName + t.Author.LastName));

            CreateMap<ThemCommentViewModel, ThemComment>().ReverseMap()
                .ForMember(x => x.Author, x => x.MapFrom(t => t.Author.FirstName +
                 SpaceSeparetedUsersFirstAndLastName + t.Author.LastName));

            CreateMap<UsersResultsViewModel, CoursesUsers>().ReverseMap()
                .ForMember(x => x.Result, x => x.MapFrom(d => d.StatusUser));

            CreateMap<CreateCourseViewModel, Course>()
                .ForMember(d => d.Image, d => d.Ignore())
                .ForMember(d => d.Creator, d => d.Ignore());

            CreateMap<CommentViewModel, Comment>().ReverseMap();

            CreateMap<LevelViewModel, Level>().ReverseMap();

            CreateMap<CreateDestinationViewModel, Destination>()
                .ForMember(d => d.Image, d => d.Ignore());

            CreateMap<CreateStoryViewModel, Story>().ReverseMap();

            CreateMap<StoryViewModel, Story>().ReverseMap()
                .ForMember(x => x.PeopleWhosLikeThis, x => x.MapFrom(d => d.PeopleWhosLikeThis.Count()))
                .ForMember(x => x.Author, x => x.MapFrom(d => d.Author.FirstName +
                 SpaceSeparetedUsersFirstAndLastName + d.Author.LastName));

            CreateMap<Destination, EditDestinationViewModel>()
                .ForMember(d => d.Image, d => d.Ignore());

            CreateMap<Destination, DestUserViewModel>().ReverseMap();

            CreateMap<Course, EditCourseViewModel>()
                .ForMember(d => d.Image, d => d.Ignore());

            CreateMap<Course, DeleteCourseViewModel>();
        }
    }
}
