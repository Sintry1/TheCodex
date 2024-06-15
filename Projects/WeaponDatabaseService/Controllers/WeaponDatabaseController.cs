using Microsoft.AspNetCore.Mvc;
using WeaponModel;
using Sentry;
using Microsoft.Extensions.Logging;

namespace WeaponDatabaseService
{
    [ApiController]
    [Route("[controller]")]
    public class WeaponDatabaseController : ControllerBase
    {

        private readonly IHub _sentryHub;
        private WeaponDatabaseServices WDBS = new WeaponDatabaseServices();
        private readonly ILogger<WeaponDatabaseController> _logger;

        public WeaponDatabaseController(ILogger<WeaponDatabaseController> logger, IHub sentryHub)
        {
            _sentryHub = sentryHub;
            _logger = logger;
        }

        //calls WeaponDatabaseServices.AddWeapon() to add a weapon to the database
        //returns true if the weapon was added, false if not
        // returns 500 if an error occurs
        [HttpPost]
        public IActionResult CreateWeapon([FromBody] Weapon weapon)
        {

            var childSpan = _sentryHub.GetSpan()?.StartChild("AddWeapon");
            try
            {

                bool result = WDBS.AddWeapon(weapon);
                if (!result)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to add weapon" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(new { Success = true, Message = "Weapon added successfully" });
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

        //calls WeaponDatabaseServices.UpdateWeapon() to update a weapon in the database
        //retyrns true if the weapon was updated, false if not
        //returns 500 if an error occurs
        [HttpPut]
        public IActionResult UpdateWeapon([FromBody] Weapon weapon)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateWeapon");

            try
            {
                bool result = WDBS.UpdateWeapon(weapon);

                if (!result)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to update weapon" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(new { Success = true, Message = "Weapon updated successfully" });
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

        //calls WeaponDatabaseServices.DeleteWeapon() to delete a weapon from the database
        //returns true if the weapon was deleted, false if not
        //returns 500 if an error occurs
        [HttpDelete("{id}")]
        public IActionResult DeleteWeapon(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteWeapon");

            try
            {
                bool result = WDBS.DeleteWeapon(id);
                if (!result)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to delete weapon" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(new { Success = true, Message = "Weapon deleted successfully" });
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

        //calls WeaponDatabaseServices.GetWeaponByType() to get a weapon by its type
        //returns the weapon with the specified type
        //returns 500 if an error occurs
        [HttpGet("type/{type}")]
        public IActionResult GetWeaponByType(string type)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeaponByType");

            try
            {
                List<Weapon> weapon = WDBS.GetWeaponsByType(type);

                if (weapon == null || !weapon.Any())
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to get weapons" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
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

        //calls WeaponDatabaseServices.GetAllWeapons() to get all weapons from the database
        //returns a list of all weapons
        //returns 500 if an error occurs
        [HttpGet]
        public IActionResult GetAllWeapons()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllWeapons");

            try
            {
                List<Weapon> weapon = WDBS.GetAllWeapons();

                if (weapon == null || !weapon.Any())
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to get weapon" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
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

        //calls WeaponDatabaseServices.GetWeaponById() to get a weapon by its id
        //returns the weapon with the specified id
        //returns 500 if an error occurs
        [HttpGet("id/{id}")]
        public IActionResult GetWeaponById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetWeaponById");

            try
            {
                Weapon weapon = WDBS.GetWeaponById(id);
                if (weapon == null)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to get weapon" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
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
