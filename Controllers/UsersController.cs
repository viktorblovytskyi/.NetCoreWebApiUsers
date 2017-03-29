using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersWebApi.Interfaces;
using UsersWebApi.Model;

namespace UsersWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        #region Fields
        private readonly IUserRepository _userRepository;
        #endregion

        #region Constructor
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region Private methods
        private async Task<IEnumerable<User>> GetUsersInternal()
        {
            return await _userRepository.GetAllUsers();            
        }

        private async Task<User> GetUserByIdInternal(string id)
        {
            return await _userRepository.GetUser(id) ?? new User();
        }
        #endregion

        #region Public methods (HTTP)
        [HttpGet]
        public Task<IEnumerable<User>> Get()
        {
            return GetUsersInternal();
        }

        [HttpGet("{id}")]
        public Task<User> Get(string id)
        {   
            return GetUserByIdInternal(id);
        }

        [HttpPost]
        public void Post([FromForm] string id, [FromForm] string name, [FromForm] string lastName, [FromForm] string role)
        {
            _userRepository.AddUser(new User { Id = id, Name = name, LastName = lastName, Role = role });
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _userRepository.RemoveUser(id);
        }

        [HttpPut("{id}")]
        public void Put(string id, [FromForm]string role)
        {
            _userRepository.UpdateUser(id, role);
        }
        #endregion
    }
}