using Microsoft.AspNetCore.Mvc;
using CreatureModel;

namespace CreatureService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CreatureServiceController : ControllerBase
    {
        private CreatureServices _creatureServices = new CreatureServices();
        private readonly IHub _sentryHub;
        private readonly ILogger<CreatureServiceController> _logger;

        public CreatureServiceController(ILogger<CreatureServiceController> _logger, IHub sentryHub)
        {
            _sentryHub = sentryHub;
        }

        // Method for creating a new creature entry
        // Takes a creature object
        // Calls CreateCreature from CreatureServices
        // Returns ok if successful, bad request if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpPost]
        public IActionResult CreateCreature([FromBody] Creature creature)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateCreature");

            try
            {
                if (_creatureServices.CreateCreature(creature).Result)
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok();
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
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
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateCreature");
            try
            {
                if (_creatureServices.UpdateCreature(creature).Result)
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok();
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for deleting a creature entry
        // Takes the name of the creature to delete
        // Calls DeleteCreature from CreatureServices
        // Returns ok if successful, bad request if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpDelete("{creatureName}")]
        public IActionResult DeleteCreature(string creatureName)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteCreature");

            try
            {
                if (_creatureServices.DeleteCreature(creatureName).Result)
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok();
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for deleting a creature entry
        // Takes the _id of the creature to delete
        // Calls DeleteCreatureById from CreatureServices
        // Returns ok if successful, bad request if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpDelete("id/{id}")]
        public IActionResult DeleteCreatureById(string id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteCreatureById");

            try
            {
                if (_creatureServices.DeleteCreatureById(id).Result)
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok();
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
                return StatusCode(500);
            }
        }


        // Method for getting a creature entry
        // Takes the name of the creature to get
        // Calls GetCreature from CreatureServices
        // Returns the creature object if successful, null if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpGet("{creatureName}")]
        public IActionResult GetCreaturesByName(string creatureName)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetCreatures");
            try
            {
                Console.WriteLine("Getting creatures: " + creatureName);

                var creatures = _creatureServices.GetAllCreatures(creatureName).Result;
                if (creatures != null && creatures.Any())
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    Console.WriteLine("Returning creatures");
                    return Ok(creatures);
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
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
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllCreatures");

            try
            {
                var creatures = _creatureServices.GetAllCreatures().Result;
                if (creatures != null || creatures.Any())
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(creatures);
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        // Method for getting a creature entry
        // Takes the _id of the creature to get
        // Calls GetCreatureById from CreatureServices
        // Returns the creature object if successful, null if not
        // Returns error code 500 if an error occurs
        // Runs asynchronously
        [HttpGet("id/{id}")]
        public IActionResult GetCreatureById(string id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetCreatureById");
            try
            {
                Console.WriteLine("Getting creature: " + id);

                var creature = _creatureServices.GetCreatureById(id).Result;
                if (creature != null)
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    Console.WriteLine("Returning creature");
                    return Ok(creature);
                }
                else
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                // Return 500 if an error occurs
                return StatusCode(500);
            }
        }
    }
}
