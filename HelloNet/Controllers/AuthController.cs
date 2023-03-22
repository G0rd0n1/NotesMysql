using Microsoft.AspNetCore.Mvc;
using HelloNet.DataAccesslayer;
using HelloNet.Model;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Ocsp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloNet.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthDL _authDL;
        public AuthController(IAuthDL authDL)
        {
            _authDL = authDL;
        }

        [HttpPost]
        public async Task<ActionResult> AddNotes(AddNotesRequest request)
        {
            NotesResponse response = new NotesResponse();
            try
            {
                response = await _authDL.AddNotes(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut("{notesId}")]
        public async Task<ActionResult<NotesResponse>> UpdateNotes(int notesId, UpdateNotesRequest request)
        {
            NotesResponse response = new NotesResponse();
            try
            {
                response = await _authDL.UpdateNotes(notesId, request);
                response.IsSuccess = true;
                response.Message = "Notes updated successfully.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }


        [HttpDelete("notes/{notesId}")]
        public async Task<ActionResult<NotesResponse>> DeleteNote(int notesId)
        {
            NotesResponse response = new NotesResponse();

            try
            {
                // Call the DeleteNote method from the data layer
                response = await _authDL.DeleteNote(notesId);

                // Check if the note was deleted successfully
                if (response.IsSuccess)
                {
                    // If the note was deleted successfully, return a success response
                    return Ok(response);
                }
                else
                {
                    // If the note was not deleted successfully, return an error response
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<NotesResponse>>> GetAllNotes()
        {
            List<NotesResponse> notes = await _authDL.GetAllNotes();

            return Ok(notes);
        }
    }
}

