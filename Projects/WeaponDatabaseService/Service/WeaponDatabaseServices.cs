using System;
using WeaponModel;

namespace WeaponDatabaseService {
	public class WeaponDatabaseServices
	{

		private PreparedStatements ps = PreparedStatements.CreateInstance();
		public WeaponDatabaseServices()
		{

		}

        //Calls PreparedStatement InsertWeapon to add a weapon to the database
        //returns a true if successful and false if failed
        public bool AddWeapon(Weapon weapon)
		{

            try
			{
                //calls PreparedStatement InsertWeapon
                return ps.InsertWeapon(weapon);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

		//Calls PreparedStatement UpdateWeapon to update a weapon, given the ID, in the database
		//returns a true if successful and false if failed
		public bool UpdateWeapon(Weapon weapon)
		{
            try
			{
                //calls PreparedStatement UpdateWeapon
                return ps.UpdateWeapon(weapon);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

		//Calls PreparedStatement DeleteWeapon to delete a weapon from the database
		//returns a true if successful and false if failed
		public bool DeleteWeapon(int id)
		{
            try
			{
                //calls PreparedStatement DeleteWeapon
                return ps.DeleteWeapon(id);
            }
            catch (Exception e)
			{
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        //Calls PreparedStatement GetWeaponById to get a weapon by its ID
        //returns a weapon
        //If an error occurs, it returns null
        public Weapon GetWeaponById(int id)
        {
            try
            {
                //calls PreparedStatement GetWeaponById
                return ps.GetWeaponById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }


        //Calls PreparedStatement GetAllWeapons to get all weapons from the database
        //returns a list of all weapons
        //If an error occurs, it returns null
        public List<Weapon> GetAllWeapons()
        {
            try
            {
                //calls PreparedStatement GetAllWeapons
                return ps.GetAllWeapons();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        //Calls PreparedStatement GetWeaponsByType to get all weapons of a certain type from the database
        //returns a list of all weapons of a certain type
        //If an error occurs, it returns null
        public List<Weapon> GetWeaponsByType(string type)
		{
            try { 
				//calls PreparedStatement GetWeaponsByType
				return ps.GetWeaponsByType(type);
			}
			catch (Exception e)
			{
				Console.WriteLine($"Unexpected error: {e.Message}");
				return null;
			}
		}	

    }
}