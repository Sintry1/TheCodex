using System;

namespace WeaponDatabaseService.service {
	public class WeaponDatabaseServices
	{
		public WeaponDatabaseServices()
		{
		}

		//Calls PreparedStatement GetAllWeapons to get all weapons from the database
		//returns a list of all weapons
		//If an error occurs, it returns null
		public List<Weapon> GetAllWeapons()
		{
			try
			{
				//calls PreparedStatement GetAllWeapons
				return PreparedStatement.GetAllWeapons();
			}
			catch (Exception e)
			{
				Console.WriteLine($"Unexpected error: {e.Message}");
				return null;
			}
			return new List<Weapon>;
		}

		//Calls PreparedStatement GetWeaponById to get a weapon by its ID
		//returns a weapon
		//If an error occurs, it returns null
		public Weapon GetWeaponById(int id)
		{
            try
			{
                //calls PreparedStatement GetWeaponById
                return PreparedStatement.GetWeaponById(id);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
            return new Weapon;
        }

		//Calls PreparedStatement AddWeapon to add a weapon to the database
		//returns a true if successful and false if failed
		public bool AddWeapon(Weapon weapon)
		{
            try
			{
                //calls PreparedStatement AddWeapon
                return PreparedStatement.AddWeapon(weapon);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
            return true;
        }

		//Calls PreparedStatement UpdateWeapon to update a weapon, given the ID, in the database
		//returns a true if successful and false if failed
		public bool UpdateWeapon(Weapon weapon)
		{
            try
			{
                //calls PreparedStatement UpdateWeapon
                return PreparedStatement.UpdateWeapon(weapon);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
            return true;
        }

		//Calls PreparedStatement DeleteWeapon to delete a weapon from the database
		//returns a true if successful and false if failed
		public bool DeleteWeapon(int id)
		{
            try
			{
                //calls PreparedStatement DeleteWeapon
                return PreparedStatement.DeleteWeapon(id);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
            return true;
        }

		//Calls PreparedStatement GetWeaponsByType to get all weapons of a certain type from the database
		//returns a list of all weapons of a certain type
		//If an error occurs, it returns null
		public List<Weapon> GetWeaponsByType(string type)
		{
			try
				//calls PreparedStatement GetWeaponsByType
				return PreparedStatement.GetWeaponsByType(type);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Unexpected error: {e.Message}");
				return null;
			}
		}	

    }
}