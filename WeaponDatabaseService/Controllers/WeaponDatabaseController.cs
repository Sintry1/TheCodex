using Microsoft.AspNetCore.Mvc;

namespace WeaponDatabaService.Controllers
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
        //returns the weapon that was added
        [HttpPost]
        public Weapon AddWeapon(Weapon weapon)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.AddWeapon(weapon);
        }

        //calls WeaponDatabaseServices.UpdateWeapon() to update a weapon in the database
        //returns the weapon that was updated
        [HttpPut]
        public Weapon UpdateWeapon(Weapon weapon)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.UpdateWeapon(weapon);
        }

        //calls WeaponDatabaseServices.DeleteWeapon() to delete a weapon from the database
        //returns the weapon that was deleted
        [HttpDelete("{id}")]
        public Weapon DeleteWeapon(int id)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.DeleteWeapon(id);
        }

        //calls WeaponDatabaseServices.GetWeaponByType() to get a weapon by its type
        //returns the weapon with the specified type
        [HttpGet("{type}")]
        public Weapon GetWeaponByType(string type)
        {
            WeaponDatabaseServices weaponDatabaseServices = new WeaponDatabaseServices();
            return weaponDatabaseServices.GetWeaponByType(type);
        }
    }
}
