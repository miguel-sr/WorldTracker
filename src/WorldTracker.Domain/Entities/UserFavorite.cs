using Amazon.DynamoDBv2.DataModel;
using System.Text.Json.Serialization;
using WorldTracker.Domain.ValueObjects;

namespace WorldTracker.Domain.Entities
{

    [DynamoDBTable("UserFavorites")]
    public class UserFavorite
    {
        [DynamoDBHashKey] 
        public string UserId { get; set; }

        [DynamoDBRangeKey]
        [JsonIgnore]
        public string FavoriteIdRaw { get; set; }

        [DynamoDBIgnore]
        public FavoriteId FavoriteId
        {
            get => FavoriteId.Parse(FavoriteIdRaw);
            set => FavoriteIdRaw = value.ToString();
        }

        [DynamoDBProperty]
        public string FavoriteType { get; set; }

        [DynamoDBProperty]
        public string Name { get; set; }

        [DynamoDBProperty]
        public DateTime DateAdded { get; private set; } = DateTime.UtcNow;
    }
}
