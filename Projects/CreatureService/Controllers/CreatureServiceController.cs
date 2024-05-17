using Microsoft.AspNetCore.Mvc;

namespace CreatureService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureServiceController : ControllerBase
    {
        private CreatureServices _creatureServices = new CreatureServices();

        public CreatureServiceController() { }

        // Method for creating a new creature entry
        // Takes a creature object
        // Calls CreateCreature from CreatureServices
        // Returns ok if successful, bad request if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpPost]
        public IActionResult CreateCreature([FromBody] Creature creature)
        {
            try
            {
                if (_creatureServices.CreateCreature(creature).Result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                return StatusCode(500);
            }
        }

        // Method for updating an existing creature entry
        // Takes a creature object
        // Calls UpdateCreature from CreatureServices
        // Returns ok if successful, bad request if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpPut]
        public IActionResult UpdateCreature([FromBody] Creature creature)
        {
            try
            {
                if (_creatureServices.UpdateCreature(creature).Result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                return StatusCode(500);
            }
        }

        // Method for deleting a creature entry
        // Takes the name of the creature to delete
        // Calls DeleteCreature from CreatureServices
        // Returns ok if successful, bad request if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpDelete]
        public IActionResult DeleteCreature(string creatureName)
        {
            try
            {
                if (_creatureServices.DeleteCreature(creatureName).Result)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                return StatusCode(500);
            }
        }

        // Method for getting a creature entry
        // Takes the name of the creature to get
        // Calls GetCreature from CreatureServices
        // Returns the creature object if successful, null if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpGet]
        public IActionResult GetCreature(string creatureName)
        {
            try
            {
                var creature = _creatureServices.GetCreature(creatureName).Result;
                if (creature != null)
                {
                    return Ok(creature);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                return StatusCode(500);
            }
        }

        // Method for getting all creatures
        // Calls GetAllCreatures from CreatureServices
        // Returns a list of all creatures, and null if no success code is returned
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpGet]
        public IActionResult GetAllCreatures()
        {
            try
            {
                var creatures = _creatureServices.GetAllCreatures().Result;
                if (creatures != null || creatures.Any())
                {
                    return Ok(creatures);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                return StatusCode(500);
            }
        }
    }
}
