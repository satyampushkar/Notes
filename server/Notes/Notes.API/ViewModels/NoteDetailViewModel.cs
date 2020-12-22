using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.API.ViewModels
{
    public class NoteDetailViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public string OwnerId { get; set; }
        public string OwnerUsername { get; set; }
    }
}
