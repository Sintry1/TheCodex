using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CreatureModel
{
    public class Creature
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; }
        public float ChallengeRating { get; set; }
        public string Attacks { get; set; }
        public string Classification { get; set; }
        public int Con { get; set; }
        public int Str { get; set; }
        public int Int { get; set; }
        public int Dex { get; set; }
        public int Cha { get; set; }
        public int Wis { get; set; }
        public string Feat { get; set; }
        public int HP { get; set; }
        public int AC { get; set; }
        public int Speed { get; set; }


    }
}
