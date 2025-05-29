using Amazon.DynamoDBv2.DataModel;
using System.Text.Json.Serialization;
using WorldTracker.Common.Interfaces;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.Entities
{
    [DynamoDBTable("Users")]
    public class User : IEntity<Guid>
    {
        [DynamoDBHashKey]
        public Guid Id { get; private set; }

        [DynamoDBProperty]
        public required string Name { get; set; }

        [DynamoDBProperty]
        [JsonIgnore]
        public string EmailRaw { get; set; }

        [DynamoDBIgnore]
        public required Email Email
        {
            get => (Email)EmailRaw;
            set => EmailRaw = value;
        }

        [DynamoDBProperty]
        [JsonIgnore]
        private string PasswordRaw { get; set; }

        [DynamoDBIgnore]
        public required Password Password
        {
            get => new(PasswordRaw, true);
            set => PasswordRaw = value;
        }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
