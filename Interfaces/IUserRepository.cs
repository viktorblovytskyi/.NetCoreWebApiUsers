﻿using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersWebApi.Model;

namespace UsersWebApi.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(string id);
        Task AddUser(User item);
        Task<DeleteResult> RemoveUser(string id);
        Task<UpdateResult> UpdateUser(string id, string name);              
        Task<ReplaceOneResult> UpdateUserDataAsync(string id, string name, string lastName, string role);        
        Task<DeleteResult> RemoveAllUsers();
    }
}
