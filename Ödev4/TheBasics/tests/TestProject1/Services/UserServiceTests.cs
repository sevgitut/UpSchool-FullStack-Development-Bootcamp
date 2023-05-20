using FakeItEasy;
using System.Linq.Expressions;
using System.Threading;
using UpSchool.Domain.Data;
using UpSchool.Domain.Entities;
using UpSchool.Domain.Services;

namespace UpSchool.Domain.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task GetUser_ShouldGetUserWithCorrectId()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var cancellationSource = new CancellationTokenSource();

            var expectedUser = new User()
            {
                Id = userId
            };

            A.CallTo(() => userRepositoryMock.GetByIdAsync(userId, cancellationSource.Token))
                .Returns(Task.FromResult(expectedUser));

            IUserService userService = new UserManager(userRepositoryMock);

            var user = await userService.GetByIdAsync(userId, cancellationSource.Token);

            Assert.Equal(expectedUser, user);
        }

        [Fact]
        public async Task AddAsync_ShouldThrowException_WhenEmailIsEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();
            
            var userService = new UserManager(userRepositoryMock);

            var cancellationSource = new CancellationTokenSource();

            
            await Assert.ThrowsAsync<ArgumentException>(() => userService.AddAsync("Sevgi", "Tut", 25, "", cancellationSource.Token));

            await Assert.ThrowsAsync<ArgumentException>(() => userService.AddAsync("Merve", "Tut", 20, null, cancellationSource.Token));

        }

        [Fact]
        public async Task AddAsync_ShouldReturn_CorrectUserId()
        {

            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            Guid userId = new Guid("8f319b0a-2428-4e9f-b7c6-ecf78acf00f9");

            var expectedUser = new User()
            {
                Id = userId
            };

            //A.CallTo(() => userRepositoryMock.AddAsync(expectedUser, cancellationSource.Token))
            //    .Returns(Task.CompletedTask);

            //IUserService userService = new UserManager(userRepositoryMock);

            //var userId = await userService.AddAsync("Sevgi", "Tut", 25, "sevgitut.07@gmail.com", cancellationSource.Token);


        }

        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            Guid userId = Guid.NewGuid();

            // A.CallTo(() => userRepositoryMock.DeleteAsync(  ?? , cancellationSource.Token)).Returns(Task.FromResult( ??));

            var result = await userService.DeleteAsync(userId, cancellationSource.Token);

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowException_WhenUserDoesntExists()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            Guid userId = Guid.NewGuid();

            await Assert.ThrowsAsync<ArgumentException>(() => userService.DeleteAsync(Guid.Empty, cancellationSource.Token));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserIdIsEmpty()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            var user = new User()
            {
                Id= Guid.Empty,
                FirstName="Sevgi",
                LastName="Tut",
                Age=25,
                Email="sevgitut.07@gmail.com"
            };

            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(user, cancellationSource.Token));
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenUserEmailEmptyOrNull()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            IUserService userService = new UserManager(userRepositoryMock);

            var userEmailNull = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Sevgi",
                LastName = "Tut",
                Age = 25,
                Email = null,
            };

            var userEmailEmpty = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Sevgi",
                LastName = "Tut",
                Age = 25,
                Email = "",
            };

            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(userEmailNull, cancellationSource.Token));
            await Assert.ThrowsAsync<ArgumentException>(() => userService.UpdateAsync(userEmailEmpty, cancellationSource.Token));
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn_UserListWithAtLeastTwoRecords()
        {
            var userRepositoryMock = A.Fake<IUserRepository>();

            var cancellationSource = new CancellationTokenSource();

            List<User> userList = new List<User>()
            {
                new User() {Id= Guid.NewGuid(), FirstName="Sevgi", LastName="Tut", Age=25,Email="sevgitut.07@gmail.com"},
                new User() {Id= Guid.NewGuid(), FirstName="Merve", LastName="Tut", Age=20,Email="mervetut@gmail.com"},
            };

            A.CallTo(() => userRepositoryMock.GetAllAsync(cancellationSource.Token))
               .Returns(Task.FromResult(userList));

            IUserService userService = new UserManager(userRepositoryMock);

            var result = await userService.GetAllAsync(cancellationSource.Token);

            Assert.True(userList.Count >= 2);
        }
    }

}