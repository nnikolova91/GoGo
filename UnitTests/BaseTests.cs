using AutoMapper;
using GoGo.Models;
using Mapping;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTests
{
    public class BaseTests : IDisposable
    {
        protected IMapper Mapper { get; }

        protected Mock<UserManager<GoUser>> UserManager { get; }

        public BaseTests()
        {
            this.UserManager = MockUserManager.GetUserManager<GoUser>();
            //this.userManager = userManager;


            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });

            Mapper = mappingConfig.CreateMapper();
        }

        public void Dispose()
        {
        }
    }

    public static class MockUserManager
    {
        public static Mock<UserManager<TUser>> GetUserManager<TUser>()
            where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var passwordHasher = new Mock<IPasswordHasher<TUser>>();
            IList<IUserValidator<TUser>> userValidators = new List<IUserValidator<TUser>>
        {
            new UserValidator<TUser>()
        };
            IList<IPasswordValidator<TUser>> passwordValidators = new List<IPasswordValidator<TUser>>
        {
            new PasswordValidator<TUser>()
        };
            userValidators.Add(new UserValidator<TUser>());
            passwordValidators.Add(new PasswordValidator<TUser>());
            var userManager = new Mock<UserManager<TUser>>(store.Object, null, passwordHasher.Object, userValidators, passwordValidators, null, null, null, null);
            return userManager;
        }
    }
}