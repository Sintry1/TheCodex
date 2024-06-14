using Microsoft.AspNetCore.Mvc;
using WeaponModel;

namespace WeaponService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponServiceController : ControllerBase
    {

        private readonly IHub _sentryHub;
        private WeaponServices WS = new WeaponServices();
        private readonly ILogger<WeaponServiceController> _logger;

        public WeaponServiceController(ILogger<WeaponServiceController> logger,IHub sentryHub)
        {
            _sentryHub = sentryHub;
            _logger = logger;
        }

        //Calls WeaponServices.AddWeaponAsync() to add a weapon to the database
        //return Ok if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpPost]
        public async Task<IActionResult> AddWeaponAsync([FromBody] Weapon weapon)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("AddWeapon");
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.UpdateWeaponAsync() to update a weapon, given the ID, in the database
        // returns Ok if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWeaponAsync(int id, [FromBody] Weapon weapon)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateWeapon");
            try
            {
                weapon.Id = id;
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs
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
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteWeapon");
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs
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
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeapons");

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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.GetWeaponByIdAsync() to get a weapon by its ID
        //returns ok with a weapon if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetWeaponByIdAsync(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeaponById");

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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //Calls WeaponServices.GetWeaponByTypeAsync() to get a weapon by its name
        //returns ok with a weapon if successful, bad request if failed
        //returns 500 if an error occurs
        //runs asynchronously
        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetWeaponByTypeAsync(string type)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeaponByType");
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }
    }
}
