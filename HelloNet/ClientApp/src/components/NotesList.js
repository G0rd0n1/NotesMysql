import React, { useEffect, useState } from 'react';
import axios from 'axios';

function NotesList() {
    const [notes, setNotes] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:7039/api/Auth/GetAllNotes/notes')
            .then(response => {
                setNotes(response.data);
            })
            .catch(error => {
                console.log(error);
            });
    }, []);

    return (
        <h1>Notes List</h1>
        <div className='notes-list'>
                {notes.map(note => (
                    <li key={note.notesId}>
                        <h3>{note.Title}</h3>
                        <p>{note.Content}</p>
                        <p>Created At: {note.CreatedAt}</p>
                    </li>
                ))}
        </div>
    );
}

export default NotesList;
