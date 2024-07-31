
using Basis.Bookstore.Core.Application.UseCases.Author.GetById;
using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;

namespace Basis.Bookstore.Tests
{

    public class FindByIdAuthorHandlerTests
    {
        private readonly Mock<IAuthorRepository> _repositoryMock;
        private readonly Mock<ILogger<FindByIdAuthorHandler>> _loggerMock;
        private readonly FindByIdAuthorHandler _handler;

        public FindByIdAuthorHandlerTests()
        {
            _repositoryMock = new Mock<IAuthorRepository>();
            _loggerMock = new Mock<ILogger<FindByIdAuthorHandler>>();
            _handler = new FindByIdAuthorHandler(_repositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnAuthor_WhenAuthorExists()
        {
            // Arrange
            var authorId = 1;
            var authorName = "John Doe";
            var command = new FindByIdAuthorCommand { Id = authorId };
            var author = new Author { Id = authorId, Name = authorName };
            _repositoryMock.Setup(repo => repo.GetById(authorId)).Returns(author);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Data);
            Assert.Equal(authorName, ((AuthorResult)result.Data).Name);
            Assert.Equal(authorId, ((AuthorResult)result.Data).Id);
        }

        [Fact]
        public async Task Handle_ShouldLogErrorAndReturnNotification_WhenExceptionOccurs()
        {
            // Arrange
            var command = new FindByIdAuthorCommand { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
            Assert.True(result.Notifications.Count > 0);
        }
    }

}