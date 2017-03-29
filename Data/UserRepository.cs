using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using UsersWebApi.Model;
using UsersWebApi.Interfaces;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace UsersWebApi.Data
{
    public class UserRepository : IUserRepository
    {
        #region Fields
        private readonly UserContext _context = null;
        #endregion

        #region Constructor
        public UserRepository(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }
        #endregion

        #region Methods
        public async Task AddUser(User item)
        {
            try
            {
                await _context.Users.InsertOneAsync(item);
            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            List<User> users = null;

            try
            {
                users = await _context.Users.Find(obj => true).ToListAsync();
            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return users;
        }

        public async Task<User> GetUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            User user = null;

            try
            {
                var filter = Builders<User>.Filter.Eq("Id", id);

                user = await _context.Users.Find(filter).FirstOrDefaultAsync();
            }
            catch(Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return user;
        }

        public async Task<DeleteResult> RemoveAllUsers()
        {
            try
            {
                return await _context.Users.DeleteManyAsync(new BsonDocument());
            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return null;
        }

        public async Task<DeleteResult> RemoveUser(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            try
            {
                var filter = Builders<User>.Filter.Eq("Id", id);

                return await _context.Users.DeleteOneAsync(filter);
            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return null;
        }

        public async Task<UpdateResult> UpdateUser(string id, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(role))
                return null;

            try
            {
                var filter = Builders<User>.Filter.Eq(s => s.Id, id);
                var update = Builders<User>.Update
                                .Set(s => s.Role, role);

                return await _context.Users.UpdateOneAsync(filter, update);
            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return null;
        }

        public async Task<ReplaceOneResult> UpdateUser(string id, User item)
        {
            if (string.IsNullOrEmpty(id) || item == null)
                return null;

            try
            {
                var filter = Builders<User>.Filter.Eq(s => s.Id, id);
                return await _context.Users.ReplaceOneAsync(filter, item, new UpdateOptions { IsUpsert = true });
            }
            catch(Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return null;
        }

        public async Task<ReplaceOneResult> UpdateUserDataAsync(string id, string name, string lastName, string role)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(role))
                return null;

            try
            {
                var item = await GetUser(id) ?? new User();

                item.Name = name;
                item.LastName = lastName;
                item.Role = role;

                return await UpdateUser(id, item);

            }
            catch (Exception e)
            {
                #if DEBUG
                Debug.WriteLine(e.Message);
                #endif
            }

            return null;
        }
        #endregion
    }
}
