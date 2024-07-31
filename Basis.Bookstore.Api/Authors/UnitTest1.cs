using Basis.Bookstore.Core.Application.UseCases.Author.GetById;
using Basis.Bookstore.Core.Application.UseCases.Authors;
using Basis.Bookstore.Core.Application.UseCases.Authors.GetById;
using Basis.Bookstore.Core.Domain.Contracts.Repositories;
using Basis.Bookstore.Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;


namespace Basis.Bookstore.Tests.Authors
{

    [TestFixture]
    public class FindByIdAuthorHandlerTests
    {
        private Mock<IAuthorRepository> _repositoryMock;
        private Mock<ILogger<FindByIdAuthorHandler>> _loggerMock;
        private FindByIdAuthorHandler _handler;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IAuthorRepository>();
            _loggerMock = new Mock<ILogger<FindByIdAuthorHandler>>();
            _handler = new FindByIdAuthorHandler(_repositoryMock.Object, _loggerMock.Object);
        }

        [Test]
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
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(authorName, ((AuthorResult)result.Data).Name);
            Assert.AreEqual(authorId, ((AuthorResult)result.Data).Id);
        }

        [Test]
        public async Task Handle_ShouldLogErrorAndReturnNotification_WhenExceptionOccurs()
        {
            // Arrange
            var command = new FindByIdAuthorCommand { Id = 1 };
            _repositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Throws(new Exception("Database error"));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _loggerMock.Verify(logger => logger.LogError(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
            Assert.IsTrue(result.Notifications.Count > 0);
        }
    }
}