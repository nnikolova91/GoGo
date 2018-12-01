using GoGo.Data;
using GoGo.Models;
using GoGo.Services.Contracts;
using GoGo.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoGo.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly GoDbContext context;

        public CommentsService(GoDbContext context)
        {
            this.context = context;
        }

        public void AddComment(string comment, string destinationId, GoUser user)
        {
            var commentt = new Comment
            {
                Comentator = user,
                ComentatorId = user.Id,
                Destination = this.context.Destinations.FirstOrDefault(x => x.Id == destinationId),
                DestinationId = destinationId,
                Content = comment
            };

            this.context.Comments.Add(commentt);
            this.context.SaveChanges();
        }
    }
}
