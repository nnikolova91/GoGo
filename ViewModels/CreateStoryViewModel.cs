
using GoGo.Models;

using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class CreateStoryViewModel //: IMapFrom<Story>, IHaveCustomMappings
    {
        //public string Id { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
        public string AuthorId { get; set; }
        public string Author { get; set; }
        public string DestinationId { get; set; }
        //public int PeopleWhosLikeThis { get; set; }

        //public void CreateMappings(IMapperConfigurationExpression configuration)
        //{
        //    configuration.CreateMap<Story, StoryViewModel>()
        //        .ForMember(x => x.Author, x => x.MapFrom(d => d.Author.FirstName))
        //        .ForMember(x => x.PeopleWhosLikeThis, x => x.MapFrom(d => d.PeopleWhosLikeThis.Count));
        //}
    }
}
