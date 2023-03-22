using HelloNet.Controllers;
using HelloNet.DataAccesslayer;
using HelloNet.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests;

public class Tests
{
    [Test]
    public async Task AddNotes_ReturnsOkResult_WhenAddNotesRequestIsValid()
    {
        // Arrange
        var mockAuthDL = new Mock<IAuthDL>();
        var expectedResponse = new NotesResponse() { IsSuccess = true };
        mockAuthDL.Setup(x => x.AddNotes(It.IsAny<AddNotesRequest>())).ReturnsAsync(expectedResponse);
        var controller = new AuthController(mockAuthDL.Object);
        var request = new AddNotesRequest();

        // Act
        var result = await controller.AddNotes(request) as OkObjectResult;
        var actualResponse = result.Value as NotesResponse;

        // Assert
        Assert.NotNull(result);
        Assert.That(actualResponse, Is.EqualTo(expectedResponse));
        Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
    }

    [Test]
    public async Task UpdateNotes_Returns_Valid_Response()
    {
        // Arrange
        int notesId = 1;
        var request = new UpdateNotesRequest { Title = "Updated Title", Content = "Updated Content" };
        var mockAuthDL = new Mock<IAuthDL>();
        mockAuthDL.Setup(x => x.UpdateNotes(notesId, request)).ReturnsAsync(new NotesResponse { IsSuccess = true, Message = "Notes updated successfully." });
        var controller = new AuthController(mockAuthDL.Object);

        // Act
        var result = await controller.UpdateNotes(notesId, request);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsInstanceOf<NotesResponse>(okResult.Value);
        var response = okResult.Value as NotesResponse;
        Assert.That(response.IsSuccess, Is.True);
        Assert.That(response.Message, Is.EqualTo("Notes updated successfully."));
    }

    [Test]
    public async Task DeleteNote_Returns_Valid_Response()
    {
        // Arrange
        int notesId = 1;
        var mockAuthDL = new Mock<IAuthDL>();
        mockAuthDL.Setup(x => x.DeleteNote(notesId)).ReturnsAsync(new NotesResponse { IsSuccess = true, Message = "Note deleted successfully." });
        var controller = new AuthController(mockAuthDL.Object);

        // Act
        var result = await controller.DeleteNote(notesId);

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);
        var okResult = result.Result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.IsInstanceOf<NotesResponse>(okResult.Value);
        var response = okResult.Value as NotesResponse;
        Assert.IsTrue(response.IsSuccess);
        Assert.That(response.Message, Is.EqualTo("Note deleted successfully."));
    }

    [Test]
    public async Task GetAllNotes_ReturnsOkResultWithNotesList()
    {
        // Arrange
        var notes = new List<NotesResponse>
    {
        new NotesResponse
        {
            NotesId = 1,
            Title = "Note 1",
            Content = "Content of note 1",
            CreatedAt = DateTime.UtcNow
        },
        new NotesResponse
        {
            NotesId = 2,
            Title = "Note 2",
            Content = "Content of note 2",
            CreatedAt = DateTime.UtcNow
        }
    };

        var mockAuthDL = new Mock<IAuthDL>();
        mockAuthDL.Setup(m => m.GetAllNotes()).ReturnsAsync(notes);

        var controller = new AuthController(mockAuthDL.Object);

        // Act
        var result = await controller.GetAllNotes();

        // Assert
        Assert.IsInstanceOf<OkObjectResult>(result.Result);

        var okResult = result.Result as OkObjectResult;
        Assert.IsInstanceOf<List<NotesResponse>>(okResult.Value);

        var returnedNotes = okResult.Value as List<NotesResponse>;
        Assert.That(returnedNotes, Has.Count.EqualTo(notes.Count));

        for (int i = 0; i < notes.Count; i++)
        {
            Assert.That(returnedNotes[i].NotesId, Is.EqualTo(notes[i].NotesId));
            Assert.That(returnedNotes[i].Title, Is.EqualTo(notes[i].Title));
            Assert.That(returnedNotes[i].Content, Is.EqualTo(notes[i].Content));
            Assert.That(returnedNotes[i].CreatedAt, Is.EqualTo(notes[i].CreatedAt));
        }
    }

}

