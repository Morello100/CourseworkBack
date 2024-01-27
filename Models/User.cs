using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace mongodb_dotnet_example.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required(ErrorMessage = "Поле 'FirstName' є обов'язковим.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле 'LastName' є обов'язковим.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле 'Email' є обов'язковим.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле 'Phone' є обов'язковим.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Поле 'Gender' є обов'язковим.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Поле 'Region' є обов'язковим.")]
        public string Region { get; set; }

        [Required(ErrorMessage = "Поле 'Password' є обов'язковим.")]
        public string Password { get; set; }

        [DataType(DataType.Text)]
        public string DateOfBirth { get; set; }

        [DataType(DataType.Text)]
        public string DesiredRegion { get; set; }

        public string Role { get; set; }

        public const string UserRole = "user";
        public const string AdminRole = "admin";
        public const string CourierRole = "courier";
    }

    public class UserLogin
{
    public string Email { get; set; }
    public string Password { get; set; }
}

}