using Microsoft.AspNetCore.Mvc;

namespace WeaponService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponServiceController : ControllerBase
    {
       private WeaponServices WS = new WeaponServices();
        public WeaponServiceController()
        {
            
        }

        //Calls WeaponServices.AddWeaponAsync() to add a weapon to the database
        //return Ok if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpPost]
        public async Task<IActionResult> AddWeaponAsync(Weapon weapon)
        {
            try
            {
                bool result = await WS.AddWeaponAsync(weapon);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to add weapon" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Weapon added successfully" });
                }
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.UpdateWeaponAsync() to update a weapon, given the ID, in the database
        // returns Ok if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpPut]
        public async Task<IActionResult> UpdateWeaponAsync(Weapon weapon)
        {
            try
            {
                bool result = await WS.UpdateWeaponAsync(weapon);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update weapon" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Weapon updated successfully" });
                }
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.DeleteWeaponAsync() to delete a weapon from the database
        //returns Ok if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeaponAsync(int id)
        {
            try
            {
                bool result = await WS.DeleteWeaponAsync(id);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to delete weapon" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Weapon deleted successfully" });
                }
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.GetWeaponsAsync() to get all weapons from the database
        //returns ok with a list of weapons if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpGet]
        public async Task<IActionResult> GetWeaponsAsync()
        {
            try
            {
                var weapons = await WS.GetWeaponsAsync();
                if (weapons == null || !weapons.Any())
                {
                    return BadRequest(new { Success = false, Message = "Failed to get weapons" });
                }
                else
                {
                    return Ok(weapons);
                }
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.GetWeaponByIdAsync() to get a weapon by its ID
        //returns ok with a weapon if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWeaponByIdAsync(int id)
        {
            try
            {
                var weapon = await WS.GetWeaponByIdAsync(id);
                if (weapon == null)
                {
                    return BadRequest(new { Success = false, Message = "Failed to get weapon" });
                }
                else
                {
                    return Ok(weapon);
                }
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.GetWeaponByTypeAsync() to get a weapon by its name
        //returns ok with a weapon if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpGet("{type}")]
            public async Task<IActionResult> GetWeaponByTypeAsync(string type)
        {
            try
            {
                List<Weapon> weapon = await WS.GetWeaponsByTypeAsync(type);
                if (weapon == null || !weapon.Any())
                {
                    return BadRequest(new { Success = false, Message = "Failed to get weapon" });
                }
                else
                {
                    return Ok(weapon);
                }
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }
    }
}
