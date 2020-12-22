using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Model.Entities
{
    public class Note : IEntityBase
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public long CreationTime { get; set; }
        public long LastEditTime { get; set; }

        public User Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
