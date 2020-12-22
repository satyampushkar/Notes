using Notes.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Data.Abstract
{
    public interface IUserRepository : IEntityBaseRepository<User>
    {
        bool IsUsernameUnique(string username);
        bool IsEmailUnique(string email);
    }
}
