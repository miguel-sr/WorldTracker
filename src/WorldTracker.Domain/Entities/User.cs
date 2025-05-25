using Amazon.DynamoDBv2.DataModel;
using WorldTracker.Common.Interfaces;

namespace WorldTracker.Domain.Entities
{
    [DynamoDBTable("Users")]
    public class User : IEntity<Guid>
    {
        [DynamoDBHashKey]
        public Guid Id { get; private set; }

        [DynamoDBProperty]
        public required string Name { get; set; }

        // Change to Value-Object
        [DynamoDBProperty]
        public required string Email { get; set; }

        // Change to Value-Object
        [DynamoDBProperty]
        public required string Password { get; set; }

        [DynamoDBProperty]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
