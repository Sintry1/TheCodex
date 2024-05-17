using Microsoft.AspNetCore.Mvc;

namespace AttacksService
{
    [ApiController]
    [Route("[controller]")]
    public class AttackController : ControllerBase
    {

        //abbreviation for EffectsDatabaseServices
        private AttackServices ADBS = new AttackServices();
        private readonly IHub _sentryHub;

        //constructor for EffectsDatabaseController
        public AttackController(IHub sentryHub)
        {
            _sentryHub = sentryHub;
        }

        //Calls AttackDatabaseServices.AddAttack() to add a attack to the database
        //returns the true if it was added, false if not
        //return 500 if an error occurs
        //Runs asynchronously
        [HttpPost]
        public async Task<IActionResult> CreateAttack(Attack attack)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateAttack");

            try
            {
                bool result = await ADBS.AddAttack(attack);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to add attack" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Attack added successfully" });
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

        //Calls AttackDatabaseServices.UpdateAttack() to update a attack in the database
        //returns true if the attack was updated, false if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpPut]
        public async Task<IActionResult> UpdateAttack(Attack attack)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateAttack");

            try
            {
                bool result = await ADBS.UpdateAttack(attack);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update attack" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Attack updated successfully" });
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

        //Calls AttackDatabaseServices.DeleteAttack() to delete a attack in the database
        //returns true if the attack was deleted, false if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpDelete]
        public async Task<IActionResult> DeleteAttack(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteAttack");

            try
            {
                bool result = await ADBS.DeleteAttack(id);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to delete attack" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Attack deleted successfully" });
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

        //Calls AttackDatabaseServices.GetAttackById() to get a attack by id from the database
        //returns an Ok with attack if successful, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttackById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAttackById");

            try
            {
                Attack attack = await ADBS.GetAttackById(id);

                if (attack == null)
                {
                    return BadRequest(new { Success = false, Message = "Failed to get attack" });
                }
                else
                {
                    return Ok(attack);
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

        //Calls AttackDatabaseServices.GetAttacks() to get all attacks from the database
        //returns an ok with the list of all attacks if successful, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpGet]
        public async Task<IActionResult> GetAttacks()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAttacks");
            try
            {
                List<Attack> attacks = await ADBS.GetAllAttacks();

                if (attacks == null || !attacks.Any())
                {
                    return BadRequest(new { Success = false, Message = "Failed to get attacks" });
                }
                else
                {
                    return Ok(attacks);
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
