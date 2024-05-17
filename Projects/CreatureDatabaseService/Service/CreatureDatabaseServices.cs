using CreatureDatabaseService;
using System;

public class CreatureDatabaseServices
{

	private DatabaseOperations _dbOperations;

	// Constructor for CreatureDatabaseServices
	public CreatureDatabaseServices()
	{
		_dbOperations = new DatabaseOperations();
	}

	//create a new creature entry
	//takes a creature object
	//returns true if successful, false if not
	//return false if an error occurs
	public bool CreateCreature(Creature creature)
	{
		try
		{
			//call CreateCreature from DatabaseOperations
			return _dbOperations.CreateCreature(creature);
		}
		catch (Exception e)
		{
			//log error message
			//Log error to more secure location
			Console.WriteLine("Error creating creature: " + e.Message);
			return false;
		}
    }

	//update an existing creature entry
	//takes a creature object
	//returns true if successful, false if not
	//return false if an error occurs
	public bool UpdateCreature(Creature updatedCreature)
	{
        try
		{
            //call UpdateCreature from DatabaseOperations
            return _dbOperations.UpdateCreature(updatedCreature);
        }
        catch (Exception e)
		{
            //log error message
            //Log error to more secure location
            Console.WriteLine("Error updating creature: " + e.Message);
            return false;
        }
    }

	//delete an existing creature entry
	//takes a name of the creature to delete
	//returns true if successful, false if not
	//return false if an error occurs
	public bool DeleteCreature(string creatureName)
	{
        try
		{
            //call DeleteCreature from DatabaseOperations
            return _dbOperations.DeleteCreature(creatureName);
        }
        catch (Exception e)
		{
            //log error message
            //Log error to more secure location
            Console.WriteLine("Error deleting creature: " + e.Message);
            return false;
        }
    }

	//get a creature entry
	//takes a name of the creature to get
	//returns the creature object if found, null if not
	//return null if an error occurs
	public Creature GetCreature(string creatureName)
	{
        try
		{
            //call GetCreature from DatabaseOperations
            return _dbOperations.ReadCreatureByName(creatureName);
        }
        catch (Exception e)
		{
            //log error message
            //Log error to more secure location
            Console.WriteLine("Error getting creature: " + e.Message);
            return null;
        }
    }

	//get all creature entries
	//returns a list of all creature objects
	//return an empty list if an error occurs
	public List<Creature> GetAllCreatures()
	{
        try
		{
            //call GetAllCreatures from DatabaseOperations
            return _dbOperations.ReadAllCreatures();
        }
        catch (Exception e)
		{
            //log error message
            //Log error to more secure location
            Console.WriteLine("Error getting all creatures: " + e.Message);
            return new List<Creature>();
        }
    }


}
