using System;
using HelloNet.Model;
using Microsoft.AspNetCore.Mvc;

namespace HelloNet.DataAccesslayer
{
	public interface IAuthDL
	{
        public Task<NotesResponse> AddNotes(AddNotesRequest request);
        public Task<NotesResponse> UpdateNotes(int notesId, UpdateNotesRequest request);
        public Task<NotesResponse> DeleteNote(int notesId);
        public Task<List<NotesResponse>> GetAllNotes();
    }
}

