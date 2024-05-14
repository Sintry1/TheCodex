using Microsoft.AspNetCore.Mvc;

namespace WeaponService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponServiceController : ControllerBase
    {
       
        public WeaponServiceController()
        {
            
        }

        //Calls WeaponServices.GetWeaponsAsync() to get all weapons from the database
        //returns a list of all weapons
        [HttpGet]
        public async Task<List<Weapon>> GetWeaponsAsync()
        {
            WeaponServices weaponServices = new WeaponServices();
            return await weaponServices.GetWeaponsAsync();
        }

        //Calls WeaponServices.GetWeaponByIdAsync() to get a weapon by its ID
        //returns a weapon
        [HttpGet("{id}")]
        public async Task<Weapon> GetWeaponByIdAsync(int id)
        {
            WeaponServices weaponServices = new WeaponServices();
            return await weaponServices.GetWeaponByIdAsync(id);
        }

        //Calls WeaponServices.AddWeaponAsync() to add a weapon to the database
        //returns a true if successful and false if failed
        [HttpPost]
        public async Task<bool> AddWeaponAsync(Weapon weapon)
        {
            WeaponServices weaponServices = new WeaponServices();
            return await weaponServices.AddWeaponAsync(weapon);
        }

        //Calls WeaponServices.UpdateWeaponAsync() to update a weapon, given the ID, in the database
        //returns a true if successful and false if failed
        [HttpPut("{id}")]
        public async Task<bool> UpdateWeaponAsync(int id, Weapon weapon)
        {
            WeaponServices weaponServices = new WeaponServices();
            return await weaponServices.UpdateWeaponAsync(id, weapon);
        }

        //Calls WeaponServices.DeleteWeaponAsync() to delete a weapon from the database
        //returns a true if successful and false if failed
        [HttpDelete("{id}")]
        public async Task<bool> DeleteWeaponAsync(int id)
        {
            WeaponServices weaponServices = new WeaponServices();
            return await weaponServices.DeleteWeaponAsync(id);
        }
    }
}
