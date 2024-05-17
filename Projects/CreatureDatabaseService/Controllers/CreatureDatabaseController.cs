using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CreatureDatabaseService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CreatureDatabaseController : ControllerBase
    {

        private CreatureDatabaseServices CDBS = new CreatureDatabaseServices();

        // Constructor for CreatureDatabaseController
        public CreatureDatabaseController()
        {
            
        }


        // Method for creating a new creature entry
        // Takes a creature object
        // calls CreateCreature from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpPost]
        public IActionResult CreateCreature([FromBody] Creature creature)
        {
            if (CDBS.CreateCreature(creature))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // Method for updating an existing creature entry
        // Takes a creature object
        // calls UpdateCreature from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpPut]
        public IActionResult UpdateCreature([FromBody] Creature creature)
        {
            try
            {
                if (CDBS.UpdateCreature(creature))
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
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for deleting an existing creature entry
        // Takes a name of the creature to delete
        // calls DeleteCreature from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpDelete]
        public IActionResult DeleteCreature(string creatureName)
        {
            try
            {
                if (CDBS.DeleteCreature(creatureName))
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
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for getting all creatures
        // calls GetAllCreatures from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpGet]
        public IActionResult GetAllCreatures()
        {
            try
            {
                var creatures = CDBS.GetAllCreatures();
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
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for getting a creature by name
        // Takes a name of the creature to get
        // calls GetCreature from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpGet("{creatureName}")]
        public IActionResult GetCreature(string creatureName)
        {
            try
            {
                var creature = CDBS.GetCreature(creatureName);
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
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

    }
}
