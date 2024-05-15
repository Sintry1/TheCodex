using Microsoft.AspNetCore.Mvc;

namespace WeaponDatabaseService
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponDatabaseController : ControllerBase
    {
        public WeaponDatabaseController()
        {
            
        }

        //calls WeaponDatabaseServices.GetAllWeapons() to get all weapons from the database
        //returns a list of all weapons
        [HttpGet]
        public List<Weapon> GetAllWeapons()
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.GetAllWeapons();
        }

        //calls WeaponDatabaseServices.GetWeaponById() to get a weapon by its id
        //returns the weapon with the specified id
        [HttpGet("{id}")]
        public Weapon GetWeaponById(int id)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.GetWeaponById(id);
        }

        //calls WeaponDatabaseServices.AddWeapon() to add a weapon to the database
        //returns true if the weapon was added, false if not
        [HttpPost]
        public bool AddWeapon(Weapon weapon)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.AddWeapon(weapon);
        }

        //calls WeaponDatabaseServices.UpdateWeapon() to update a weapon in the database
        //retyrns true if the weapon was updated, false if not
        [HttpPut]
        public bool UpdateWeapon(Weapon weapon)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.UpdateWeapon(weapon);
        }

        //calls WeaponDatabaseServices.DeleteWeapon() to delete a weapon from the database
        //returns true if the weapon was deleted, false if not
        [HttpDelete("{id}")]
        public bool DeleteWeapon(int id)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.DeleteWeapon(id);
        }

        //calls WeaponDatabaseServices.GetWeaponByType() to get a weapon by its type
        //returns the weapon with the specified type
        [HttpGet("{type}")]
        public List<Weapon> GetWeaponByType(string type)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.GetWeaponsByType(type);
        }
    }
}
