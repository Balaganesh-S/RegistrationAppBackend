using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication3.Models
{
    public class User
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public string country { get; set; } 
        public string zipCode { get; set; }
        public string phoneNumber { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime dateOfBirth { get; set; }

        public DateTime createdDate { get; set; }

        public string plan { get; set; }

        public int age { get; set; }
    }
}
