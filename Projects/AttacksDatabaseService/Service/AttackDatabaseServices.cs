using System;
using AttacksDatabaseService;

namespace AttacksDatabaseService
{
	public class AttackDatabaseServices
	{
		private PreparedStatements ps = PreparedStatements.CreateInstance();

		public AttackDatabaseServices()
		{
		}

		//method calls the InsertAttack method from PreparedStatements
		//takes an Attack object as parameter
		//returns true if the Attack was added successfully, false if not
		public bool AddAttack(Attack attack)
		{
            try
			{
                return ps.InsertAttack(attack);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//method calls the UpdateAttack method from PreparedStatements
		//takes an Attack object as parameter
		//returns true if the Attack was updated successfully, false if not
		public bool UpdateAttack(Attack attack)
		{
            try
			{
                return ps.UpdateAttack(attack);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//method calls the DeleteAttack method from PreparedStatements
		//takes an int id as parameter
		//returns true if the Attack was deleted successfully, false if not
		public bool DeleteAttack(int id)
		{
            try
			{
                return ps.DeleteAttack(id);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//method calls the GetAttack method from PreparedStatements
		//takes an int id as parameter
		//returns an Attack object
		public Attack GetAttack(int id)
		{
            try
			{
                return ps.GetAttackById(id);
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return null;
            }
        }

		//method calls the GetAllAttacks method from PreparedStatements
		//returns a list of Attack objects
		public List<Attack> GetAllAttacks()
		{
            try
			{
                return ps.GetAllAttacks();
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return null;
            }
        }

		

	}
}