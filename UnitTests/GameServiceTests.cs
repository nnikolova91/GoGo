using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;
using UnitTests.Comparers;
using ViewModels;
using Xunit;

namespace UnitTests
{
    public class GameServiceTests : BaseTests
    {
        [Fact]
        public async Task AddGame_ShouldAddNewGameCorrectlyWithCorrectData()
        {
            var gamesRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gamesRepoBuilder
                .WithAll()
                .Build();


            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, levelRepo, null, null, Mapper);

            var createGameModel = new CreateGameViewModel
            {
                Name = "Haide na planina",
                Description = "planinaaaaaaaaa",
                Level1 = new CreateLevelViewModel
                { Description = "level1Description", NumberInGame = 1, Points = 3, Image = SetupFileMock().Object },
                Level2 = new CreateLevelViewModel
                { Description = "level2Description", NumberInGame = 2, Points = 5, Image = SetupFileMock().Object },
                Level3 = new CreateLevelViewModel
                { Description = "level3Description", NumberInGame = 3, Points = 7, Image = SetupFileMock().Object }
            };

            var user = new GoUser { Id = "7" };

            await sut.AddGame(createGameModel, user);

            gamesRepoBuilder.GamesRepoMock.Verify(r => r.AddAsync(It.IsAny<Game>()), Times.Once);
            gamesRepoBuilder.GamesRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);

            levelsRepoBuilder.LevelsRepoMock.Verify(r => r.AddAsync(It.IsAny<Level>()), Times.Exactly(3));
            levelsRepoBuilder.LevelsRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public void/*async Task*/ GetAllGames_ShouldReturn_All_GameViewModels()
        {
            var gameRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gameRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, levelRepo, null, null, Mapper);

            var actual = sut.GetAllGames();
            var expected = new List<GameViewModel>
            {
                new GameViewModel
                {
                    Id = "7",
                    Name = "Nameri mqstoto",
                    Image = levelRepo.All().FirstOrDefault(l=>l.GameId == "7" && l.NumberInGame == 3).Image
                },
                 new GameViewModel
                {
                    Id = "9",
                    Name = "Na more",
                    Image = levelRepo.All().FirstOrDefault(l=>l.GameId == "9" && l.NumberInGame == 3).Image
                }
            }.AsQueryable();

            Assert.Equal(expected, actual, new GameViewModelComparer());

            gameRepoBuilder.GamesRepoMock.Verify();
            //destRepoMock.Verify();
        }

        [Fact]
        public void/*async Task*/ GetDetails_ShouldReturn_GameDetailsViewModel()
        {
            var gameRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gameRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, levelRepo, levelsParticipantsRepo, userRepo, Mapper);

            var actual = sut.GetDetails("7");
            var expected = new GameDetailsViewModel
            {
                Id = "7",
                Name = "Nameri mqstoto",
                Description = "nameriiiiiiiiiii",
                Creator = "Niki",
                Level1 = new LevelViewModel
                {
                    Id = "1",
                    Description = "mmmmm",
                    Points = 1,
                    GameId = "7",
                    NumberInGame = 1,
                    Image = levelRepo.All().FirstOrDefault(x => x.Id == "1").Image
                },
                Level2 = new LevelViewModel
                {
                    Id = "2",
                    Description = "nnnnnnnnnnnnnnnn",
                    Points = 3,
                    GameId = "7",
                    NumberInGame = 2,
                    Image = levelRepo.All().FirstOrDefault(x => x.Id == "2").Image
                },
                Level3 = new LevelViewModel
                {
                    Id = "3",
                    Description = "rrrrrrrrrrrrrrrrrr",
                    Points = 5,
                    GameId = "7",
                    NumberInGame = 3,
                    Image = levelRepo.All().FirstOrDefault(x => x.Id == "3").Image
                },
                GameParticipantsLevel1 = new List<GameLevelParticipantViewModel>
                {
                    new GameLevelParticipantViewModel
                    {
                        GameId = "7",
                        LevelId = "1",
                        ParticipantId = "10",
                        Participant = "Saso ",
                        CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                        StatusLevel = StatusLevel.NotPassed
                    }
                },
                GameParticipantsLevel2 = new List<GameLevelParticipantViewModel>
                {
                    new GameLevelParticipantViewModel
                    {
                        GameId = "7",
                        LevelId = "2",
                        ParticipantId = "10",
                        Participant = "Saso ",
                        CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                        StatusLevel = StatusLevel.NotPassed
                    }
                },
                GameParticipantsLevel3 = new List<GameLevelParticipantViewModel>
                {
                    new GameLevelParticipantViewModel
                    {
                        GameId = "7",
                        LevelId = "3",
                        ParticipantId = "10",
                        Participant = "Saso ",
                        CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                        StatusLevel = StatusLevel.NotPassed
                    }
                }

            };

            Assert.Equal(expected, actual, new GameDetailsViewModelComparer());
            Assert.Equal(expected.Level1, actual.Level1, new LevelViewModelComparer());
            Assert.Equal(expected.Level2, actual.Level2, new LevelViewModelComparer());
            Assert.Equal(expected.Level3, actual.Level3, new LevelViewModelComparer());
            Assert.Equal(expected.GameParticipantsLevel1, actual.GameParticipantsLevel1, new GameLevelParticipantViewModelComparer());
            Assert.Equal(expected.GameParticipantsLevel2, actual.GameParticipantsLevel2, new GameLevelParticipantViewModelComparer());
            Assert.Equal(expected.GameParticipantsLevel3, actual.GameParticipantsLevel3, new GameLevelParticipantViewModelComparer());

            gameRepoBuilder.GamesRepoMock.Verify();
        }

        [Fact]
        public void/*async Task*/ GetDetails_ShouldThrow_IfGameNotExist()
        {
            var gameRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gameRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, null, null, null, Mapper);
            
            var ex = Assert.Throws<ArgumentException>(() => sut.GetDetails("17"));

            Assert.Equal("Game do not exist!", ex.Message);
        }

        [Fact]
        public async Task UserStartGame_ShouldAdduserToGameCorrect()
        {
            var gameRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gameRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();
            
            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, levelRepo, levelsParticipantsRepo, null, Mapper);

            var user = new GoUser { Id = "1" };

            await sut.UserStartGame("7", user);

            gameRepoBuilder.GamesRepoMock.Verify();
            levelsRepoBuilder.LevelsRepoMock.Verify();
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();

            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.AddRangeAsync(It.IsAny<List<GameLevelParticipant>>()), Times.Once);
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UserStartGame_ShouldThrowIfGameNotExist()
        {
            var gameRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gameRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, levelRepo, levelsParticipantsRepo, null, Mapper);

            var user = new GoUser { Id = "1" };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.UserStartGame("11", user));

            Assert.Equal("Game do not exist!", ex.Message);

            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.AddRangeAsync(It.IsAny<List<GameLevelParticipant>>()), Times.Never);
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);

            gameRepoBuilder.GamesRepoMock.Verify();
            levelsRepoBuilder.LevelsRepoMock.Verify();
        }

        [Fact]
        public async Task UserStartGame_ShouldDoNotAddUserToGame_IfUserAureadyExistInThisGame()
        {
            var gameRepoBuilder = new GameRepositoryBuilder();
            var gameRepo = gameRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(gameRepo, levelRepo, levelsParticipantsRepo, null, Mapper);

            var user = new GoUser { Id = "10" };

            await sut.UserStartGame("7", user);
            
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.AddRangeAsync(It.IsAny<List<GameLevelParticipant>>()), Times.Never);
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);

            gameRepoBuilder.GamesRepoMock.Verify();
            levelsRepoBuilder.LevelsRepoMock.Verify();
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();
        }

        [Fact]
        public async Task UserAddImageToLevel_ShouldAddImageCorrect()
        {
            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(null, null, levelsParticipantsRepo, null, Mapper);

            var user = new GoUser { Id = "10" };

            await sut.UserAddImageToLevel("7", user, "1", SetupFileMock().Object);
            
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UserAddImageToLevel_ShouldDoNotAddItIfUserIsNotInThisGame()
        {
            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(null, null, levelsParticipantsRepo, null, Mapper);

            var user = new GoUser { Id = "6" };

            await sut.UserAddImageToLevel("7", user, "1", SetupFileMock().Object);

            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task UserAddImageToLevel_ShouldThrowIfUserIsPassedThisLevelSuccessffuly()
        {
            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(null, null, levelsParticipantsRepo, null, Mapper);

            var user = new GoUser { Id = "9" };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.UserAddImageToLevel("7", user, "1", SetupFileMock().Object));

            Assert.Equal("You are auready pass successfully this level!", ex.Message);

            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();

            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddLevelResult_ShouldAddResultToGameLevelUserCorrect()
        {
            var userRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = userRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(null, levelRepo, levelsParticipantsRepo, userRepo, Mapper);

            var user = new GoUser { Id = "1" };

            var gameLevelUserViewModel = new GameLevelParticipantViewModel
            {
                GameId = "7",
                LevelId = "3",
                ParticipantId = "10",
                Participant = "Saso",
                CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                StatusLevel = StatusLevel.SuccessfullyPassed
            };

            await sut.AddLevelResult(gameLevelUserViewModel, user);

            userRepoBuilder.UsersRepoMock.Verify();
            levelsRepoBuilder.LevelsRepoMock.Verify();
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();

            userRepoBuilder.UsersRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddLevelResult_ShouldThrow_IfGameLevelParticipantNotExist()
        {
            var userRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = userRepoBuilder
                .WithAll()
                .Build();

            var levelsRepoBuilder = new LevelRepositoryBuilder();
            var levelRepo = levelsRepoBuilder
                .WithAll()
                .Build();

            var levelsParticipantsRepoBuilder = new GameLevelParticipantRepositoryBuilder();
            var levelsParticipantsRepo = levelsParticipantsRepoBuilder
                .WithAll()
                .Build();

            var sut = new GamesService(null, levelRepo, levelsParticipantsRepo, userRepo, Mapper);

            var user = new GoUser { Id = "1" };

            var gameLevelUserViewModel = new GameLevelParticipantViewModel
            {
                GameId = "7",
                LevelId = "6",
                ParticipantId = "10",
                Participant = "Saso",
                CorrespondingImage = ConvertImageToByteArray(SetupFileMock().Object),
                StatusLevel = StatusLevel.SuccessfullyPassed
            };
            
            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddLevelResult(gameLevelUserViewModel, user));

            Assert.Equal("This GameLeveelParticipant not exist!", ex.Message);
            
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify();

            userRepoBuilder.UsersRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
            levelsParticipantsRepoBuilder.LevelParticipantRepoMock.Verify(r => r.SaveChangesAsync(), Times.Never);
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
