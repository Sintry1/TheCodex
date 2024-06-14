using System;
using Sentry;
using WeaponModel;

namespace WeaponDatabaseService
{
    public class WeaponDatabaseServices
    {
        
        private PreparedStatements ps = PreparedStatements.CreateInstance();

        public WeaponDatabaseServices()
        {

        }

        public bool AddWeapon(Weapon weapon)
        {
            
            try
            {
                return ps.InsertWeapon(weapon);
            }
            catch (Exception e)
            {
                
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        public bool UpdateWeapon(Weapon weapon)
        {
            
            try
            {
                return ps.UpdateWeapon(weapon);
            }
            catch (Exception e)
            {
               
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        public bool DeleteWeapon(int id)
        {
            try
            {
                return ps.DeleteWeapon(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        public Weapon GetWeaponById(int id)
        {
            try
            {
                return ps.GetWeaponById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        public List<Weapon> GetAllWeapons()
        {
            try
            {
                return ps.GetAllWeapons();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        public List<Weapon> GetWeaponsByType(string type)
        {
            try
            {
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
