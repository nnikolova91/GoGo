using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnitTests.Builders
{
    internal class DestinationsUsersRepositoryBuilder
    {
        public Mock<IRepository<DestinationsUsers>> DestUsersRepoMock { get; }

        public DestinationsUsersRepositoryBuilder()
        {
            this.DestUsersRepoMock = new Mock<IRepository<DestinationsUsers>>();
        }

        internal DestinationsUsersRepositoryBuilder WithAll()
        {
            var destinatUsers = new List<DestinationsUsers>
            {
                new DestinationsUsers
                {
                    DestinationId = "1",
                    ParticipantId = "1",
                    Socialization = (Socialization)1
                },
                new DestinationsUsers
                {
                    DestinationId = "1",
                    ParticipantId = "2",
                    Socialization = (Socialization)2
                },
                new DestinationsUsers
                {
                    DestinationId = "1",
                    ParticipantId = "3",
                    Socialization = (Socialization)2
                },
                new DestinationsUsers
                {
                    DestinationId = "1",
                    ParticipantId = "4",
                    
                    Socialization = (Socialization)2
                },

                new DestinationsUsers
                {
                    DestinationId = "2",
                    ParticipantId = "7",
                    Participant = new GoUser{ Id = "7", FirstName = "Slavqna"},
                    Socialization = Socialization.KnowSomeone
                },
                new DestinationsUsers
                {
                    DestinationId = "2",
                    ParticipantId = "8",
                    Participant = new GoUser{ Id = "8", FirstName = "Niki"},
                    Socialization = Socialization.KnowSomeone
                },
                new DestinationsUsers
                {
                    DestinationId = "2",
                    ParticipantId = "9",
                    Participant = new GoUser{ Id = "9", FirstName = "Pelin"},
                    Socialization = Socialization.NotKnowAnyone
                },
                new DestinationsUsers
                {
                    DestinationId = "2",
                    ParticipantId = "10",
                    Participant = new GoUser{ Id = "10", FirstName = "Saso"},
                    Socialization = Socialization.NotKnowAnyone
                },
                new DestinationsUsers
                {
                    DestinationId = "2",
                    ParticipantId = "11",
                    Participant = new GoUser{ Id = "11", FirstName = "Koni"},
                    Socialization = Socialization.NotKnowAnyone
                }
            }.AsQueryable();

            DestUsersRepoMock.Setup(d => d.All())
                .Returns(destinatUsers).Verifiable();

            return this;
        }

        internal IRepository<DestinationsUsers> Build()
        {
            return DestUsersRepoMock.Object;
        }

        
    }
}
