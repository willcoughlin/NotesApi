using System;
using Xunit;
using Moq;
using NotesApi.Persistence;
using System.Collections.Generic;
using NotesApi.Models;
using NotesApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NotesApi.Exceptions;

namespace NotesApi.Tests.Controllers
{
    public class NotesControllerTests
    {
        private readonly static List<Note> _testNotes = new List<Note>
        {
            new Note(1, "Title 1", "Content 2"),
            new Note(1, "Title 2", "Content 2"),
            new Note(1, "Title 3", "Content 3"),
        };

        [Fact]
        public void GetAll_ReturnsAllNotes()
        {
            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.GetAll()).Returns(_testNotes);

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.GetAll().Result as OkObjectResult;
            var listOfNotes = controllerResponse.Value as IEnumerable<Note>;
            Assert.Equal(_testNotes, listOfNotes);
        }

        [Fact]
        public void Get_RepositoryReturnsNullResult_ReturnsNotFound()
        {
            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns((Note)null);

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.Get(1).Result as NotFoundObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, controllerResponse.StatusCode);
        }

        [Fact]
        public void Get_NoteReturnedByRepository_ReturnsNote()
        {
            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Get(It.IsAny<int>())).Returns(_testNotes[0]);

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.Get(1);
            var returnedNote = controllerResponse.Value as Note;

            Assert.Equal(_testNotes[0].Id, returnedNote.Id);
            Assert.Equal(_testNotes[0].Title, returnedNote.Title);
            Assert.Equal(_testNotes[0].Contents, returnedNote.Contents);
        }

        [Fact]
        public void Create_ReturnsIdOfNewNote()
        {
            int expectedNewNoteId = 2;
            var newNoteRequest = new NoteRequest
            {
                Title = "Test title",
                Contents = "Test contents"
            };

            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Create(It.IsAny<Note>())).Returns(expectedNewNoteId);

            var controller = new NotesController(mockRepository.Object);
            var controllerResponse = controller.Create(newNoteRequest);
            Assert.Equal(expectedNewNoteId, controllerResponse.Id);
        }

        [Fact]
        public void Edit_RepositoryThrowsNotFoundException_ReturnsNotFound()
        {
            var editNoteRequest = new NoteRequest
            {
                Title = "New title",
                Contents = "New contents"
            };

            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Edit(It.IsAny<Note>())).Throws<ResourceNotFoundException>();

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.Edit(1, editNoteRequest) as NotFoundObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, controllerResponse.StatusCode);
        }

        [Fact]
        public void Edit_RepositoryDoesNotThrow_ReturnsOk()
        {
            var editNoteRequest = new NoteRequest
            {
                Title = "New title",
                Contents = "New contents"
            };

            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Edit(It.IsAny<Note>()));

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.Edit(1, editNoteRequest) as OkResult;
            Assert.Equal(StatusCodes.Status200OK, controllerResponse.StatusCode);
        }

        [Fact]
        public void Delete_RepositoryThrowsNotFoundException_ReturnsNotFound()
        {
            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Delete(It.IsAny<int>())).Throws<ResourceNotFoundException>();

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.Delete(1) as NotFoundObjectResult;
            Assert.Equal(StatusCodes.Status404NotFound, controllerResponse.StatusCode);
        }

        [Fact]
        public void Delete_RepositoryDoesNotThrow_ReturnsOk()
        {
            var mockRepository = new Mock<INotesRepository>();
            mockRepository.Setup(m => m.Delete(It.IsAny<int>()));

            var controller = new NotesController(mockRepository.Object);

            var controllerResponse = controller.Delete(1) as OkResult;
            Assert.Equal(StatusCodes.Status200OK, controllerResponse.StatusCode);
        }
    }
}
