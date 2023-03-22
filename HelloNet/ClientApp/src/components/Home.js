import React, { Component, useEffect, useState } from 'react';
import axios, { AxiosResponse } from 'axios';
import { FaTrash, FaPen, FaCheck } from 'react-icons/fa';
import './Notes.css'

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);

        this.state = {
            notes: [],
            newNoteTitle: '',
            newNoteContent: '',
            editingNoteId: null,
            editingNoteTitle: '',
            editingNoteContent: '',
        };

        this.handleDeleteNote = this.handleDeleteNote.bind(this);
        this.handleAddNote = this.handleAddNote.bind(this);
        this.handleTitleChange = this.handleTitleChange.bind(this);
        this.handleContentChange = this.handleContentChange.bind(this);
    }

    componentDidMount() {
        this.fetchNotes();
    }

    async fetchNotes() {
        try {
            const response = await axios.get('https://localhost:7039/api/Auth/GetAllNotes');
            this.setState({
                notes: response.data
            });
        } catch (error) {
            console.log(error);
        }
    }

    async handleAddNote() {
        try {
            const response = await axios.post('https://localhost:7039/api/Auth/AddNotes', {
                title: this.state.newNoteTitle,
                content: this.state.newNoteContent,
            });
            console.log(response.data);
            this.setState({
                newNoteTitle: '',
                newNoteContent: '',
            });
            this.fetchNotes();
        } catch (error) {
            console.error(error);
        }
    }


    async handleDeleteNote(noteId) {
        try {
            const response = await axios.delete(`https://localhost:7039/api/Auth/DeleteNote/notes/${noteId}`);
            console.log(response);
            const notes = this.state.notes.filter(note => note.notesId !== noteId);
            this.setState({ notes });
            this.fetchNotes();
        } catch (error) {
            console.log(error);
        }
    }

    handleEditNoteStart(note) {
        this.setState({
            editingNoteId: note.notesId,
            editingNoteTitle: note.title,
            editingNoteContent: note.content,
        });
    }

    async handleUpdateNoteConfirm(note) {
        try {
            const response = await axios.put(`https://localhost:7039/api/Auth/UpdateNotes/${note.notesId}`, {
                title: this.state.editingNoteTitle,
                content: this.state.editingNoteContent,
            });
            console.log(response.data);
            this.setState({
                editingNoteId: null,
                editingNoteTitle: '',
                editingNoteContent: '',
            });
            this.fetchNotes();
        } catch (error) {
            console.error(error);
        }
    }


    handleTitleChange(event) {
        this.setState({ newNoteTitle: event.target.value });
    }

    handleContentChange(event) {
        this.setState({ newNoteContent: event.target.value });
    }


    render() {

        const { notes, newNoteTitle, newNoteContent } = this.state;

        return (
            <div className="container">
                <div className="row">
                    <div className="col-md-12">
                        <img src="https://igamingbusiness.com/wp-content/uploads/2022/12/GG-Website_White.png" className="img-fluid" style={{ paddingBottom: '20px' }}/>
                    </div>

                    <div className="col-md-12">
                        <div className="row">
                            <div className="col-md-12">
                                <form className="input-section">
                                    <div className="form-group">
                                        <input type="text" className="form-control mb-2" placeholder="Note Title" value={newNoteTitle} onChange={this.handleTitleChange} />
                                    </div>
                                    <div className="form-group">
                                        <textarea className="form-control mb-2" placeholder="Note Content" value={newNoteContent} onChange={this.handleContentChange}></textarea>
                                    </div>
                                    <button type="submit" className="btn btn-success col-md-12 mt-4" onClick={this.handleAddNote}>Add Note</button>
                                </form>
                            </div>
                            <div className="col-md-12 mt-4">
                                <div className="col-md-12 mb-4">
                                    <h1 className="text-center">Notes</h1>
                                </div>
                                <table className="table notes-list table-hover">
                                    <thead>
                                        <tr>
                                            <th style={{ textAlign: 'left' }}>Title</th>
                                            <th style={{ textAlign: 'left' }}>Content</th>
                                            <th style={{ textAlign: 'left' }}>Date &amp; Time</th>
                                            <th style={{ textAlign: 'left' }}>Edit/Delete</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {notes.map((note) => (
                                            <tr key={note.notesId}>
                                                <td>
                                                    {this.state.editingNoteId === note.notesId ?
                                                        <input type="text" value={this.state.editingNoteTitle} onChange={(event) => this.setState({ editingNoteTitle: event.target.value })} className="form-control" />
                                                        :
                                                        note.title
                                                    }
                                                </td>
                                                <td>
                                                    {this.state.editingNoteId === note.notesId ?
                                                        <textarea value={this.state.editingNoteContent} onChange={(event) => this.setState({ editingNoteContent: event.target.value })} className="form-control"></textarea>
                                                        :
                                                        note.content
                                                    }
                                                </td>
                                                <td style={{ fontStyle: 'italic', fontWeight: 'bold' }}>
                                                    {new Date(note.createdAt).toLocaleString([], { year: 'numeric', month: '2-digit', day: '2-digit', hour: 'numeric', minute: 'numeric', hour12: true })}
                                                </td>
                                                <td>
                                                    {this.state.editingNoteId === note.notesId ?
                                                        <button type="button" className="btn btn-success" onClick={() => this.handleUpdateNoteConfirm(note)}><FaCheck style={{ color: 'white' }} /></button>
                                                        :
                                                        <button type="button" className="btn btn-primary" onClick={() => this.handleEditNoteStart(note)}><FaPen style={{ color: 'white' }} /></button>
                                                    }
                                                    <button type="button" className="btn btn-danger" onClick={() => this.handleDeleteNote(note.notesId)}><FaTrash style={{ color: 'white' }} /></button>
                                                </td>
                                            </tr>
                                        ))}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );

    }
}