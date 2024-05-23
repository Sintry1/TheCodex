using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CreatureModel;

namespace CreatureDatabaseService.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CreatureDatabaseController : ControllerBase
    {

        private CreatureDatabaseServices CDBS = new CreatureDatabaseServices();
        private readonly IHub _sentryHub;

        // Constructor for CreatureDatabaseController
        public CreatureDatabaseController(IHub sentryHub)
        {
            _sentryHub = sentryHub;
        }


        // Method for creating a new creature entry
        // Takes a creature object
        // calls CreateCreature from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpPost]
        public IActionResult CreateCreature([FromBody] Creature creature)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateCreature");
            try {  
                if (CDBS.CreateCreature(creature))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            } catch (Exception e)
            {
                //Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                //Return 500 if an error occurs
                return StatusCode(500);
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
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateCreature");

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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

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
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteCreature");
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for deleting an existing creature entry by id
        // Takes an id of the creature to delete
        // calls DeleteCreatureById from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        //returns error code 500 if an error occurs
        [HttpDelete("id/{id}")]
        public IActionResult DeleteCreatureById(string id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteCreatureById");
            try
            {
                if (CDBS.DeleteCreatureById(id))
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

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
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllCreatures");
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
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

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
        public IActionResult GetCreatures(string creatureName)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetCreatures");
            Console.WriteLine("Getting creatures: " + creatureName + " from database.");

            try
            {
                var creatures = CDBS.GetCreaturesByName(creatureName);

                if (creatures != null && creatures.Any())
                {
                    Console.WriteLine("returning creatures");
                    return Ok(creatures);
                }
                else
                {
                    Console.WriteLine("no creatures found");
                    return BadRequest();
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

        // Method for getting a creature by _id
        // Takes an _id of the creature to get
        // calls GetCreature from CreatureDatabaseServices
        // returns ok if successful, bad request if not
        // returns error code 500 if an error occurs
        [HttpGet("id/{id}")]
        public IActionResult GetCreatureById(string id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetCreatureById");
            Console.WriteLine("Getting creature: " + id + " from database.");

            try
            {
                var creature = CDBS.GetCreatureById(id);

                if (creature != null)
                {
                    Console.WriteLine("returning creature");
                    return Ok(creature);
                }
                else
                {
                    Console.WriteLine("no creature found");
                    return BadRequest();
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
