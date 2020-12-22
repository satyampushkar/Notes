using Notes.Data.Abstract;
using Notes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data.Repositories
{
    public class UserRepository : EntityBaseRepository<User>, IUserRepository
    {
        public UserRepository(NotesContext context) : base(context)
        {}

        public bool IsEmailUnique(string email)
        {
            var user = this.GetSingle(u => u.Email == email);
            return user == null;
        }

        public bool IsUsernameUnique(string username)
        {
            var user = this.GetSingle(u => u.Username == username);
            return user == null;
        }
    }
}
