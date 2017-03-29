using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UsersWebApi.Interfaces;
using UsersWebApi.Data;
using UsersWebApi.Model;

namespace UsersWebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DefaultGenerationDataController : Controller
    {
        private readonly IUserRepository _userRepository;

        public DefaultGenerationDataController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (string.IsNullOrEmpty(setting))
                return "String is Empty";

            if (setting == "init")
            {
                _userRepository.RemoveAllUsers();

                _userRepository.AddUser(new User { Id = "1", Name = "TestUser1", LastName = "TestLastName1", Role = "TestRole1" });
                _userRepository.AddUser(new User { Id = "2", Name = "TestUser2", LastName = "TestLastName2", Role = "TestRole2" });
                _userRepository.AddUser(new User { Id = "3", Name = "TestUser3", LastName = "TestLastName3", Role = "TestRole3" });
                _userRepository.AddUser(new User { Id = "4", Name = "TestUser4", LastName = "TestLastName4", Role = "TestRole4" });

                return "Done";
            }

            return "Unknown";
        }
    }
}