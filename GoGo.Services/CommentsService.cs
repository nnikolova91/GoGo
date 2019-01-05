using GoGo.Models;
using GoGo.Services.Contracts;
using System.Linq;
using System.Threading.Tasks;
using GoGo.Data.Common;
using System;

namespace GoGo.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> commentsRepository;
        private readonly IRepository<Destination> destinationsRepository;

        public CommentsService(IRepository<Comment> commentsRepository,
                                IRepository<Destination> destinationsRepository)
        {
            this.commentsRepository = commentsRepository;
            this.destinationsRepository = destinationsRepository;
        }

        public async Task AddComment(string comment, string destinationId, GoUser user)
        {
            var commentt = new Comment
            {
                Comentator = user,
                ComentatorId = user.Id,
                Date = DateTime.Now,
                Destination = this.destinationsRepository.All().FirstOrDefault(x => x.Id == destinationId),
                DestinationId = destinationId,
                Content = comment
            };

           await this.commentsRepository.AddAsync(commentt);
           await this.commentsRepository.SaveChangesAsync();
        }
    }
}
