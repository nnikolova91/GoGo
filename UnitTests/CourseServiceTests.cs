using GoGo.Data.Common;
using GoGo.Models;
using GoGo.Models.Enums;
using GoGo.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Shouldly;
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
    public class CourseServiceTests : BaseTests
    {
        [Fact]
        public async Task AddCourse_ShouldAddNewCourse()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            await sut.AddCourse(new ViewModels.CreateCourseViewModel()
            {
                Title = "New course",
                Image = SetupFileMock().Object,
                Description = "Test create course",
                MaxCountParticipants = 11,
                StartDate = DateTime.Now.AddDays(1),
                DurationOfDays = 7
            },
            new GoUser { Id = "7" });

            coursesRepoBuilder.CoursesRepoMock.Verify(r => r.AddAsync(It.IsAny<Course>()), Times.Once);
            coursesRepoBuilder.CoursesRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddResultToUsersCourses_ShouldAddResultCorrect_IfCourseExist()
        {
            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var courseUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(courseUserRepo, courseRepo, null, Mapper);

            var user = new GoUser { Id = "7" };

            var courseUser = new UsersResultsViewModel
            {
                CourseId = "1",
                Course = new CourseViewModel
                {
                    Id = "1",
                    Image = new byte[0],
                    Title = "Drun",
                    Description = "Drunnnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 4,
                    StartDate = DateTime.Now.AddDays(3),
                    DurationOfDays = 3,
                    CreatorId = "7",
                    Creator = new GoUserViewModel { Id = "7" },
                    Status = Status.Practically,
                    Category = Category.Climbing
                },
                ParticipantId = "8",
                Result = StatusParticitant.Successfully
            };

            await sut.AddResultToUsersCourses(courseUser, user);

            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(r => r.AddAsync(It.IsAny<CoursesUsers>()), Times.Exactly(0));
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddResultToUsersCourses_ShouldThrow_IfCourseNotExist()
        {
            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var courseUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(courseUserRepo, courseRepo, null, Mapper);

            var user = new GoUser { Id = "7" };

            var courseUser = new UsersResultsViewModel
            {
                CourseId = "7",
                Course = new CourseViewModel
                {
                    Id = "7",
                    Image = new byte[0],
                    Title = "Drun",
                    Description = "Drunnnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 4,
                    StartDate = DateTime.Now.AddDays(3),
                    DurationOfDays = 3,
                    CreatorId = "7",
                    Creator = new GoUserViewModel { Id = "7" },
                    Status = Status.Practically,
                    Category = Category.Climbing
                },
                ParticipantId = "8",
                Result = StatusParticitant.Successfully
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddResultToUsersCourses(courseUser, user));

            ex.Message.ShouldBe("This userCourse not exist!");
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify();
            coursesRepoBuilder.CoursesRepoMock.Verify();
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddResultToUsersCourses_ShouldThrow_IfUserIsNotCreator()
        {
            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var courseUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(courseUserRepo, courseRepo, null, Mapper);

            var user = new GoUser { Id = "3" };

            var courseUser = new UsersResultsViewModel
            {
                CourseId = "1",
                Course = new CourseViewModel
                {
                    Id = "1",
                    Image = new byte[0],
                    Title = "Drun",
                    Description = "Drunnnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 4,
                    StartDate = DateTime.Now.AddDays(3),
                    DurationOfDays = 3,
                    CreatorId = "7",
                    Creator = new GoUserViewModel { Id = "7" },
                    Status = Status.Practically,
                    Category = Category.Climbing
                },
                ParticipantId = "8",
                Result = StatusParticitant.Successfully
            };

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await sut.AddResultToUsersCourses(courseUser, user));

            ex.Message.ShouldBe("You can do not add results!");
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify();
            coursesRepoBuilder.CoursesRepoMock.Verify();
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task AddUserToCourse_ShouldAddNew_CourseUser()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var courseUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(courseUserRepo, courseRepo, null, Mapper);

            var user = new GoUser { Id = "7" };

            await sut.AddUserToCourse("1", user);

            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(r => r.AddAsync(It.IsAny<CoursesUsers>()), Times.Once);
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddUserToCourse_ShouldNotAddNew_CourseUser_WhenMaxCountParticipantsIsFully()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var courseUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(courseUserRepo, courseRepo, null, Mapper);

            var user = new GoUser { Id = "7" };
            var user1 = new GoUser { Id = "8" };

            await sut.AddUserToCourse("1", user);
            await sut.AddUserToCourse("1", user1);

            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(r => r.AddAsync(It.IsAny<CoursesUsers>()), Times.Exactly(1));
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify(r => r.SaveChangesAsync(), Times.Exactly(1));
        }

        [Fact]
        public async Task DeleteCourse_ShouldDeleteCourseCorrectly_IfCourseexist_AndUserIsCreator()
        {
            Course deletedCourse = null;

            var courseRepoBuilder = new CoursesRepositoryBuilder();
            courseRepoBuilder.CoursesRepoMock.Setup(r => r.Delete(It.IsAny<Course>()))
                .Callback<Course>(c => deletedCourse = c);

            var courseRepo = courseRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var user = new GoUser { Id = "9" };

            await sut.DeleteCourse("2", user);

            Assert.Equal("2", deletedCourse.Id);
            courseRepoBuilder.CoursesRepoMock.Verify();

            courseRepoBuilder.CoursesRepoMock.Verify(d => d.Delete(It.IsAny<Course>()), Times.Once);
            courseRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCourse_ShouldNotDeleteCourse_WhenCourseNotExist()
        {
            Course deletedCourse = null;

            var courseRepoBuilder = new CoursesRepositoryBuilder();
            courseRepoBuilder.CoursesRepoMock.Setup(r => r.Delete(It.IsAny<Course>()))
                .Callback<Course>(c => deletedCourse = c);

            var courseRepo = courseRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var user = new GoUser { Id = "9" };

            await sut.DeleteCourse("7", user);

            Assert.Null(deletedCourse);
            courseRepoBuilder.CoursesRepoMock.Verify();

            courseRepoBuilder.CoursesRepoMock.Verify(d => d.Delete(It.IsAny<Course>()), Times.Exactly(0));
            courseRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Exactly(0));
        }

        [Fact]
        public async Task DeleteCourse_ShouldNotDeleteCourse_WhenUserIsNotCreator()
        {
            var user = new GoUser { Id = "6" };

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            await sut.DeleteCourse("2", user);

            coursesRepoBuilder.CoursesRepoMock.Verify(c => c.Delete(It.IsAny<Course>()), Times.Exactly(0));
            coursesRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Exactly(0));
        }

        [Fact]
        public async Task EditCourse_ShouldEditCourseCorrectly()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var editCourseViewModel = new EditCourseViewModel
            {
                Id = "2",
                Image = SetupFileMock().Object,
                Title = "Edit",
                Description = "Brr",
                MaxCountParticipants = 7,
                StartDate = DateTime.Now.AddDays(2),
                DurationOfDays = 5,
                Status = Status.Practically,
                Category = Category.Other
            };

            var user = new GoUser { Id = "9" };

            await sut.EditCourse(editCourseViewModel, user);

            coursesRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Once);

            var newdest = courseRepo.All().FirstOrDefault(x => x.Id == "2");

            var actual = Mapper.Map<EditCourseViewModel>(newdest);

            Assert.Equal(editCourseViewModel, actual, new EditCourseViewModelComparer());
        }

        [Fact]
        public async Task EditCourse_ShouldNotEditCourseIfCourseNotExist()
        {
            var user = new GoUser { Id = "9" };

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var editCourseViewModel = new EditCourseViewModel
            {
                Id = "7",
                Image = SetupFileMock().Object,
                Title = "Edit",
                Description = "Brr",
                MaxCountParticipants = 7,
                StartDate = DateTime.Now.AddDays(2),
                DurationOfDays = 5,
                Status = Status.Practically,
                Category = Category.Other
            };

            await sut.EditCourse(editCourseViewModel, user);

            coursesRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Exactly(0));
        }

        [Fact]
        public async Task EditCourse_ShouldNotEditCourseIf_UserIsNotCreator()
        {
            var user = new GoUser { Id = "2" };

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var editCourseViewModel = new EditCourseViewModel
            {
                Id = "2",
                Image = SetupFileMock().Object,
                Title = "Edit",
                Description = "Brr",
                MaxCountParticipants = 7,
                StartDate = DateTime.Now.AddDays(2),
                DurationOfDays = 5,
                Status = Status.Practically,
                Category = Category.Other
            };

            await sut.EditCourse(editCourseViewModel, user);

            coursesRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Exactly(0));
        }

        [Fact]
        public void FindCourse_ShouldWork_CorrectlyAndReturn_EditCourseViewModel()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var actual = sut.FindCourse("3");

            Assert.IsType<EditCourseViewModel>(actual);

            Assert.Equal("3", actual.Id);
        }

        [Fact]
        public void FindCourse_ShouldThrowExceptionIfCourseNotExist()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var ex = Assert.Throws<ArgumentException>(() => sut.FindCourse("10"));

            Assert.Equal("You can not edit this page", ex.Message);
        }

        [Fact]
        public void FindCourseForDelete_ShouldWork_CorrectlyAndReturn_DeleteCourseViewModel()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var actual = sut.FindCourseForDelete("3");

            Assert.IsType<DeleteCourseViewModel>(actual);

            Assert.Equal("3", actual.Id);
        }

        [Fact]
        public void FindCourseForDelete_ShouldThrowExceptionIfCourseNotExist()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var ex = Assert.Throws<ArgumentException>(() => sut.FindCourseForDelete("10"));

            Assert.Equal("You can not delete this page", ex.Message);
        }

        [Fact]
        public void/*async Task*/ GetAllCourses_ShouldReturn_All_CourseViewModels()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var actual = sut.GetAllCourses();
            var expected = new List<CourseViewModel>
            {
                new CourseViewModel
                {
                    Id = "1",
                    Image = new byte[0],
                    Title = "Drun",
                    Description = "Drunnnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 4,
                    StartDate = DateTime.Now.AddDays(3),
                    DurationOfDays = 3,
                    Status = Status.Practically,
                    Category = Category.Climbing
                },
                 new CourseViewModel
                {
                    Id = "2",
                    Image = new byte[0],
                    Title = "Brum",
                    Description = "Brummmmmmmmmmmmm",
                    MaxCountParticipants = 10,
                    StartDate = DateTime.Now.AddDays(2),
                    DurationOfDays = 5,
                    //Creator = "9",
                    Status = Status.Theoretical,
                    Category = Category.Cycling
                },
                  new CourseViewModel
                {
                    Id = "3",
                    Image = new byte[0],
                    Title = "Mrun",
                    Description = "Mrunnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 11,
                    StartDate = DateTime.Now.AddDays(1),
                    DurationOfDays = 7,
                    Status = Status.Practically,
                    Category = Category.Skiing
                }
            }.AsQueryable();

            Assert.Equal(expected, actual, new CourseViewModelComparer());

            coursesRepoBuilder.CoursesRepoMock.Verify();
            coursesRepoBuilder.CoursesRepoMock.Verify(d => d.AddAsync(It.IsAny<Course>()), Times.Never);
            coursesRepoBuilder.CoursesRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void/*async Task*/ GetAllCourses_ShouldReturn_EmptyCollection_IfIsEmpty()
        {
            var courses = new List<Course>().AsQueryable();

            var courseRepoMock = new Mock<IRepository<Course>>();
            courseRepoMock.Setup(d => d.All())
                .Returns(courses).Verifiable();

            var sut = new CoursesService(null, courseRepoMock.Object, null, Mapper);

            var actual = sut.GetAllCourses();
            var expected = new List<CourseViewModel>().AsQueryable();

            Assert.Equal(expected, actual, new CourseViewModelComparer());

            courseRepoMock.Verify();
            courseRepoMock.Verify(d => d.AddAsync(It.IsAny<Course>()), Times.Never);
            courseRepoMock.Verify(d => d.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public void/*async Task*/ GetAllParticipants_ShouldReturn_All_UsersResultsViewModels()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var coursUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(coursUserRepo, courseRepo, userRepo, Mapper);

            var user = new GoUser { Id = "7" };

            var actual = sut.GetAllParticipants("1", user);

            var expected = new List<UsersResultsViewModel>
            {
                new UsersResultsViewModel
                {
                    CourseId = "1",
                    ParticipantId = "8",
                    Participant = new GoUserViewModel { Id = "8", FirstName = "Niki", },
                    Result = StatusParticitant.Successfully
                },
                 new UsersResultsViewModel
                {
                    CourseId = "1",
                    ParticipantId = "9",
                    Participant = new GoUserViewModel { Id = "9", FirstName = "Pelin"},
                    Result = StatusParticitant.Successfully
                },
                 new UsersResultsViewModel
                {
                    CourseId = "1",
                    ParticipantId = "11",
                    Participant = new GoUserViewModel { Id = "11", FirstName = "Koni"},
                    Result = StatusParticitant.Unsuccessfully
                }
            }.AsQueryable();

            var usersFromActual = actual.Select(x => x.Participant).ToList();
            var usersFromExpected = expected.Select(x => x.Participant).ToList();

            Assert.Equal(expected, actual, new UsersResultsViewModelComparer());
            Assert.Equal(usersFromExpected, usersFromActual, new GoUserViewModelComparer());

            coursesRepoBuilder.CoursesRepoMock.Verify();
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify();
            usersRepoBuilder.UsersRepoMock.Verify();
        }

        [Fact]
        public void/*async Task*/ GetAllParticipants_ShouldThrow_IfUserIsNotCreator()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var coursUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(coursUserRepo, courseRepo, null, Mapper);

            var user = new GoUser { Id = "9" };

            var ex = Assert.Throws<ArgumentException>(() => sut.GetAllParticipants("1", user));

            Assert.Equal("You can not add results!", ex.Message);

            coursesRepoBuilder.CoursesRepoMock.Verify();
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify();
        }

        [Fact]
        public void/*async Task*/  GetDetails_ShouldReturn_CourseDetailsViewModel()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var coursUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var usersRepoBuilder = new GoUserRepositoryBuilder();
            var userRepo = usersRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(coursUserRepo, courseRepo, userRepo, Mapper);

            var actual = sut.GetDetails(1, "2");

            var expected = new CourseDetailsViewModel
            {
                Id = "2",
                Image = new byte[0],
                Title = "Brum",
                Description = "Brummmmmmmmmmmmm",
                MaxCountParticipants = 10,
                StartDate = DateTime.Now.AddDays(2),
                DurationOfDays = 5,
                Creator = new GoUserViewModel { Id = "9", FirstName = "Pelin", Image = new byte[0] },
                FreeSeats = 9,
                Status = Status.Theoretical,
                Category = Category.Cycling
            };

            Assert.Equal(expected, actual, new CourseDetailsViewModelComparer());
            Assert.Equal(expected.Creator, actual.Creator, new GoUserViewModelComparer());
            Assert.True(1 == actual.Participants.Count());

            coursesRepoBuilder.CoursesRepoMock.Verify();
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify();
            usersRepoBuilder.UsersRepoMock.Verify();
        }

        [Fact]
        public void/*async Task*/ GetDetails_ShouldThrow_IfCourseNotExist()
        {
            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(null, courseRepo, null, Mapper);

            var ex = Assert.Throws<ArgumentException>(() => sut.GetDetails(1, "7"));

            Assert.Equal("Course not exist!", ex.Message);

            coursesRepoBuilder.CoursesRepoMock.Verify();
        }

        [Fact]
        public void/*async Task*/ GetMyCourses_ShouldReturn_CorrectListOf_CourseViewModels()
        {
            var coursesUsersRepoBuilder = new CoursesUsersRepositoryBuilder();
            var coursUserRepo = coursesUsersRepoBuilder
                .WithAll()
                .Build();

            var coursesRepoBuilder = new CoursesRepositoryBuilder();
            var courseRepo = coursesRepoBuilder
                .WithAll()
                .Build();

            var sut = new CoursesService(coursUserRepo, courseRepo, null, Mapper);

            var actual = sut.GetMyCourses("8");

            var expected = new List<CourseViewModel>
            {
                new CourseViewModel
                {
                    Id = "1",
                    Image = new byte[0],
                    Title = "Drun",
                    Description = "Drunnnnnnnnnnnnnnnnnn",
                    MaxCountParticipants = 4,
                    StartDate = DateTime.Now.AddDays(3),
                    Creator = new GoUserViewModel {Id = "7",  FirstName = "Slavqna"},
                    DurationOfDays = 3,
                    Status = Status.Practically,
                    Category = Category.Climbing
                }
            }.AsQueryable();

            Assert.Equal(expected, actual, new CourseViewModelComparer());
            
            coursesUsersRepoBuilder.CoursesUsersRepoMock.Verify();
        }

        private static Mock<IFormFile> SetupFileMock()
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
