using HelloNet.Model;
using MySql.Data.MySqlClient;


namespace HelloNet.DataAccesslayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;

        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnection"]);
        }


        public async Task<NotesResponse> AddNotes(AddNotesRequest request)
        {
            NotesResponse response = new NotesResponse();
            response.IsSuccess = true;
            response.Message = "Note added successfully";

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }
                
                string sqlQuery = @"INSERT INTO GGAssessment.notedata (Title, Content, CreatedAt) 
                                    VALUES (@Title, @Content, @CreatedAt);";

                using (MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@Title", request.Title);
                    sqlCommand.Parameters.AddWithValue("@Content", request.Content);
                    sqlCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }

        public async Task<NotesResponse> DeleteNote(int notesId)
        {
            NotesResponse response = new NotesResponse();
            response.IsSuccess = true;
            response.Message = "Note deleted successfully";

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string sqlQuery = @"DELETE FROM GGAssessment.notedata WHERE notesId = @notesId";

                using (MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@notesId", notesId);

                    int status = await sqlCommand.ExecuteNonQueryAsync();
                    if (status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Note not found";
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }

        public async Task<NotesResponse> UpdateNotes(int notesId, UpdateNotesRequest request)
        {
            NotesResponse response = new NotesResponse();
            response.IsSuccess = true;
            response.Message = "Note updated successfully";

            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string sqlQuery = @"UPDATE GGAssessment.notedata SET Title = @Title, Content = @Content, CreatedAt = @CreatedAt WHERE notesId = @notesId;";

                using (MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@notesId", notesId);
                    sqlCommand.Parameters.AddWithValue("@Title", request.Title);
                    sqlCommand.Parameters.AddWithValue("@Content", request.Content);
                    sqlCommand.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Note not found";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }

        public async Task<List<NotesResponse>> GetAllNotes()
{
    List<NotesResponse> notes = new List<NotesResponse>();

    try
    {
        if (_mySqlConnection.State != System.Data.ConnectionState.Open)
        {
            await _mySqlConnection.OpenAsync();
        }

        string sqlQuery = @"SELECT notesId, Title, Content, CreatedAt 
                            FROM GGAssessment.notedata;";

        using (MySqlCommand sqlCommand = new MySqlCommand(sqlQuery, _mySqlConnection))
        {
            sqlCommand.CommandType = System.Data.CommandType.Text;
            sqlCommand.CommandTimeout = 180;

            using (MySqlDataReader reader = (MySqlDataReader)await sqlCommand.ExecuteReaderAsync())
            {
                while (reader.Read())
                {
                    NotesResponse note = new NotesResponse
                    {
                        NotesId = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Content = reader.GetString(2),
                        CreatedAt = reader.GetDateTime(3)
                    };

                    notes.Add(note);
                }
            }
        }
    }
    catch (Exception ex)
    {
        
    }
    finally
    {
        
    }

    return notes;
}


    }
}

