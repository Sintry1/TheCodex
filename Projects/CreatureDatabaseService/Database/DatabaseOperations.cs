using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace CreatureDatabaseService
{
    public class DatabaseOperations
    {
        private readonly IMongoCollection<Creature> _creaturesCollection;

        public DatabaseOperations()
        {
            var dbConnection = new MongoDBConnection();
            var database = dbConnection.GetDatabase();
            _creaturesCollection = database.GetCollection<Creature>("creatures");

        }

        // Create a new creature entry
        // Takes a creature object
        // returns true if successful, false if not
        public bool CreateCreature(Creature creature)
        {
            try
            {
                _creaturesCollection.InsertOne(creature);
                Console.WriteLine("Creature inserted successfully.");
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error inserting creature: " + e.Message);
                return false;
            }
        }

        // Update an existing creature entry
        // Takes a creature object
        // Returns true if successful, false if not
        // return false if an error occurs
        public bool UpdateCreature(Creature updatedCreature)
        {
            try
            {
                var filter = Builders<Creature>.Filter.Eq(c => c.Name, updatedCreature.Name);
                var result = _creaturesCollection.ReplaceOne(filter, updatedCreature);

                if (result.ModifiedCount > 0)
                {
                    Console.WriteLine("Creature updated successfully.");
                    return true;
                }
                else
                {
                    Console.WriteLine("No creature found with the given Id.");
                    return false;
                }
            } catch (Exception e)
            {
                Console.WriteLine("Error updating creature: " + e.Message);
                return false;
            }
        }

        // Delete a creature entry
        // Takes an id
        // Returns true if successful, false if not
        public bool DeleteCreature(string name)
        {
            var filter = Builders<Creature>.Filter.Eq(c => c.Name, name);
            var result = _creaturesCollection.DeleteOne(filter);

            if (result.DeletedCount > 0)
            {
                Console.WriteLine("Creature deleted successfully.");
                return true;
            }
            else
            {
                Console.WriteLine("No creature found with the given Id.");
                return false;
            }
        }

        // Get all creatures
        // Returns a list of all creatures
        public List<Creature> ReadAllCreatures()
        {
            try
            {
                return _creaturesCollection.Find(new BsonDocument()).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading creatures: " + e.Message);
                return null;
            }
        }

        // Get a creature by name
        // Takes a name
        // Returns a creature object if found, null if not
        //returns null if an error occurs
        public Creature ReadCreatureByName(string name)
        {
            try
            {
                var filter = Builders<Creature>.Filter.Eq(c => c.Name, name);
                return _creaturesCollection.Find(filter).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading creature: " + e.Message);
                return null;
            }
        }



    }
}


