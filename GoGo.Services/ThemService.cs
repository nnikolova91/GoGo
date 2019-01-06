using AutoMapper;
using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace GoGo.Services
{
    public class ThemService : IThemService
    {
        public const string ThemeNotExist = "Theme not exist!";
        public const string SpaceSeparetedUsersFirstAndLastName = " ";

        private readonly IRepository<GoUser> usersRepository;
        private readonly IRepository<Theme> themsRepository;
        private readonly IRepository<ThemComment> themCommentsRepository;
        private readonly IMapper mapper;

        public ThemService(IRepository<Theme> themsRepository,
                            IRepository<GoUser> usersRepository,
                            IRepository<ThemComment> themCommentsRepository,
                            IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.themsRepository = themsRepository;
            this.themCommentsRepository = themCommentsRepository;
            this.mapper = mapper;
        }

        public async Task AddCommentToThem(string themId, string currentComment, GoUser user)
        {
            var them = themsRepository.All().FirstOrDefault(x => x.Id == themId);

            if (them == null)
            {
                throw new ArgumentException(ThemeNotExist);
            }

            var comment = new ThemComment
            {
                Content = currentComment,
                Date = DateTime.Now,
                ThemId = themId,
                Author = user,
                AuthorId = user.Id
            };

            await this.themCommentsRepository.AddAsync(comment);
            await this.themCommentsRepository.SaveChangesAsync();
        }

        public async Task AddThem(CreateThemViewModel model, GoUser user)
        {
            var them = new Theme
            {
                Description = model.Description,
                Date = DateTime.Now,
                Author = user,
                AuthorId = user.Id
            };

            await this.themsRepository.AddAsync(them);
            await this.themsRepository.SaveChangesAsync();
        }

        public ICollection<ThemDetailsViewModel> GetAllThems()
        {
            var thems = this.themsRepository.All().Select(x => mapper.Map<ThemDetailsViewModel>(x)).ToList();
            thems.ForEach(x => x.Author = this.usersRepository.All().FirstOrDefault(u => u.Id == x.AuthorId).FirstName);

            thems.ForEach(x => x.Comments = this.themCommentsRepository.All().Where(c => c.ThemId == x.Id)
            .Select(c => mapper.Map<ThemCommentViewModel>(c)).ToList());

            return thems;
        }

        public ThemDetailsViewModel GetDetails(string themId, GoUser user)
        {
            var them = this.themsRepository.All().FirstOrDefault(x => x.Id == themId);

            if (them == null)
            {
                throw new ArgumentException(ThemeNotExist);
            }

            var themModel = mapper.Map<ThemDetailsViewModel>(them);

            var author = this.usersRepository.All().FirstOrDefault(x => x.Id == them.AuthorId);

            themModel.Author = author.FirstName + SpaceSeparetedUsersFirstAndLastName + author.LastName;

            var themComments = this.themCommentsRepository.All().Where(x => x.ThemId == themId)
                                     .Select(x => mapper.Map<ThemCommentViewModel>(x)).ToList();

            themComments.ForEach(x => x.Author = this.usersRepository.All().FirstOrDefault(u => u.Id == x.AuthorId).FirstName +
                                     SpaceSeparetedUsersFirstAndLastName + this.usersRepository.All()
                                    .FirstOrDefault(u => u.Id == x.AuthorId).LastName);

            themModel.Comments = themComments;

            return themModel;
        }
    }
}
