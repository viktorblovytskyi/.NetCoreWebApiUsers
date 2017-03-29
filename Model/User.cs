using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace UsersWebApi.Model
{
    public class User
    {
        [BsonId]
        [BsonElement("Id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("LastName")]
        public string LastName { get; set; }

        [BsonElement("Role")]
        public string Role { get; set; }
    }
}
