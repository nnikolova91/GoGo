using GoGo.Data;
using GoGo.Models;
using GoGo.Services.Contracts;
using ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoGo.Data.Common;

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

        public void AddComment(string comment, string destinationId, GoUser user)
        {
            var commentt = new Comment
            {
                Comentator = user,
                ComentatorId = user.Id,
                Destination = this.destinationsRepository.All().FirstOrDefault(x => x.Id == destinationId),
                DestinationId = destinationId,
                Content = comment
            };

            this.commentsRepository.AddAsync(commentt);
            this.commentsRepository.SaveChangesAsync();
        }
    }
}
