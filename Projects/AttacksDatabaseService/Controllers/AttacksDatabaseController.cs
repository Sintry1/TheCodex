using AttacksDatabaseService;
using Microsoft.AspNetCore.Mvc;

namespace AttacksDatabaseService
{
    [ApiController]
    [Route("[controller]")]
    public class AttackDatabaseController : ControllerBase
    {

        //abbreviation for EffectsDatabaseServices
        private AttackDatabaseServices ADBS = new AttackDatabaseServices();
        private readonly IHub _sentryHub;

        //constructor for EffectsDatabaseController
        public AttackDatabaseController(IHub sentryHub)
        {
            _sentryHub = sentryHub;

        }

        //Calls AttackDatabaseServices.AddAttack() to add a attack to the database
        //returns the true if it was added, false if not
        [HttpPost]
        public IActionResult CreateAttack(Attack attack)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateAttack");

            try
            {
                bool result = ADBS.AddAttack(attack);
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
        [HttpPut]
        public IActionResult UpdateAttack(Attack attack)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateAttack");

            try
            {
                bool result = ADBS.UpdateAttack(attack);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update attack" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Attack updated successfully" });
                };
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

        //Calls AttackDatabaseServices.DeleteAttack() to delete a attack from the database
        //returns the attack that was deleted
        [HttpDelete("{id}")]
        public IActionResult DeleteAttack(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteAttack");
            try
            {
                bool result = ADBS.DeleteAttack(id);
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

        //Calls AttackDatabaseServices.GetAttack() to get a attack by its id
        //returns the attack with the specified id
        [HttpGet("{id}")]
        public IActionResult GetAttack(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAttackById");

            try
            {
                Attack result = ADBS.GetAttack(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
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

        //Calls AttackDatabaseServices.GetAllAttacks() to get all Attacks from the database
        //returns a list of all Attacks
        [HttpGet]
        public IActionResult GetAllAttacks()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAttacks");

            try
            {
                List<Attack> result = ADBS.GetAllAttacks();

                if (result == null || !result.Any())
                {
                    //Return 404 if no feats are found
                    return NotFound();

                }
                else
                {
                    return Ok(result);
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
