using Notes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data.Abstract
{
    public interface INoteRepository : IEntityBaseRepository<Note>
    {
        bool IsOwner(string noteId, string userId);
    }
}
