using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using CreatureModel;

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
                // Insert the creature object into the database
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
                // Find the creature in the database and replace it with the updated creature
                var filter = Builders<Creature>.Filter.Eq(c => c.Name, updatedCreature.Name);
                // Replace the creature with the updated creature
                var result = _creaturesCollection.ReplaceOne(filter, updatedCreature);

                // Check if the creature was updated
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
            // Find the creature in the database and delete it
            var filter = Builders<Creature>.Filter.Eq(c => c.Name, name);
            
            // Delete the creature
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

            // Find all creatures in the database
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
        public List<Creature> ReadCreaturesByName(string name)
        {
            try
            {
                Console.WriteLine("finding creatures: " + name);
                // Find the creatures in the database
                var filter = Builders<Creature>.Filter.Regex(c => c.Name, new BsonRegularExpression(name, "i"));

                // Get the creatures
                var creatures = _creaturesCollection.Find(filter).ToList();

                Console.WriteLine("Creatures found.");

                // Add the creatures to a new list
                var creatureList = new List<Creature>(creatures);

                Console.WriteLine("Number of creatures found: " + creatures.Count);


                Console.WriteLine(creatureList);
                foreach (var creature in creatures)
                {
                       Console.WriteLine("Creature: " + creature.Name);
                }

                Console.WriteLine("returning creatures from db");
                // Return the list of creatures
                return creatureList;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading creatures: " + e.Message);
                return null;
            }
        }

        // Get a creature by id
        // Takes an id
        // Returns a creature object if found, null if not
        public Creature ReadCreatureById(string id)
        {
            try
            {
                // Find the creature in the database
                var filter = Builders<Creature>.Filter.Eq(c => c._id, id);
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


