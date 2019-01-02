using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UnitTests.Builders
{
    internal class GameLevelParticipantRepositoryBuilder
    {
        public Mock<IRepository<GameLevelParticipant>> LevelParticipantRepoMock { get; }

        public GameLevelParticipantRepositoryBuilder()
        {
            this.LevelParticipantRepoMock = new Mock<IRepository<GameLevelParticipant>>();
        }

        internal GameLevelParticipantRepositoryBuilder WithAll()
        {
            var levelsParticipants = new List<GameLevelParticipant>
            {
                new GameLevelParticipant
                {
                    GameId = "7",
                    LevelId = "1",
                    ParticipantId = "9",
                    Participant = new GoUser { FirstName = "Pelin"},
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.SuccessfullyPassed
                },
                new GameLevelParticipant
                {
                    GameId = "7",
                    LevelId = "1",
                    ParticipantId = "10",
                    Participant = new GoUser { FirstName = "Saso"},
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.NotPassed
                },
                new GameLevelParticipant
                {
                    GameId = "7",
                    LevelId = "2",
                    ParticipantId = "10",
                    Participant = new GoUser { FirstName = "Saso"},
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.NotPassed
                },
                new GameLevelParticipant
                {
                    GameId = "7",
                    LevelId = "3",
                    ParticipantId = "10",
                    Participant = new GoUser { FirstName = "Saso"},
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.NotPassed
                },
                new GameLevelParticipant
                {
                    GameId = "9",
                    LevelId = "4",
                    ParticipantId = "7",
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.NotPassed
                },
                new GameLevelParticipant
                {
                    GameId = "9",
                    LevelId = "5",
                    ParticipantId = "7",
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.NotPassed
                },
                new GameLevelParticipant
                {
                    GameId = "9",
                    LevelId = "6",
                    ParticipantId = "7",
                    CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                    StatusLevel = StatusLevel.NotPassed
                },
            }.AsQueryable();

            LevelParticipantRepoMock.Setup(d => d.All())
                .Returns(levelsParticipants).Verifiable();

            return this;
        }

        internal IRepository<GameLevelParticipant> Build()
        {
            return LevelParticipantRepoMock.Object;
        }

        private static byte[] ConvertImageToByteArray(IFormFile image)
        {
            byte[] file = null;
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    file = ms.ToArray();
                }
            }

            return file;
        }

        internal static Mock<IFormFile> SetupFileMock()
        {
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock;
        }
    }
}
