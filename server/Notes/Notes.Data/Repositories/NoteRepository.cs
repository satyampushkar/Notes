using Notes.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data.Repositories
{
    public class NoteRepository : EntityBaseRepository<Model.Entities.Note>, INoteRepository
    {
        public NoteRepository(NotesContext context) : base(context)
        { }

        public bool IsOwner(string noteId, string userId)
        {
            var note = this.GetSingle(noteId);
            return note.OwnerId == userId;
        }
    }
}
