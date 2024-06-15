using Microsoft.AspNetCore.Mvc;
using EffectsModel;

namespace EffectsDatabaseService
{
    [ApiController]
    [Route("[controller]")]
    public class EffectsDatabaseController : ControllerBase
    {

        //abbreviation for EffectsDatabaseServices
        private EffectsDatabaseServices EDBS = new EffectsDatabaseServices();
        private readonly IHub _sentryHub;
        private readonly ILogger<EffectsDatabaseController> _logger;

        //constructor for EffectsDatabaseController
        public EffectsDatabaseController(ILogger<EffectsDatabaseController> logger, IHub sentryHub)
        {
            _sentryHub = sentryHub;
            _logger = logger;
        }

        //calls EffectsDatabaseServices.AddEffects() to add a effect to the database
        //returns the true if it was added, false if not
        [HttpPost]
        public IActionResult CreateEffects([FromBody] Effects effect)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateEffect");

            try
            {
                bool result = EDBS.AddEffect(effect);
                if (!result)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to add effect" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(new { Success = true, Message = "Effect added successfully" });
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

        //calls EffectsDatabaseServices.UpdateEffect() to update a effect in the database
        //returns true if the effect was updated, false if not
        [HttpPut]
        public IActionResult UpdateEffects([FromBody] Effects effect)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateEffect");

            try
            {
                bool result = EDBS.UpdateEffect(effect);

                if (!result)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to update effect" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(new { Success = true, Message = "Effect updated successfully" });
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

        //calls EffectsDatabaseServices.DeleteEffect() to delete a effect from the database
        //returns the effect that was deleted
        [HttpDelete("{id}")]
        public IActionResult DeleteEffects(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteEffect");

            try
            {
                bool result = EDBS.DeleteEffect(id);
                if (!result)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return BadRequest(new { Success = false, Message = "Failed to delete effect" });
                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
                    return Ok(new { Success = true, Message = "Effect deleted successfully" });
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

        //calls EffectsDatabaseServices.GetEffectsById() to get a effect by its id
        //returns the effect with the specified id
        [HttpGet("{id}")]
        public IActionResult GetEffectsById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetEffectById");

            try
            {
                Effects result = EDBS.GetEffectById(id);
                if (result == null)
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    return NotFound();
                }

                childSpan?.Finish(SpanStatus.Ok);
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

        //calls EffectsDatabaseServices.GetAllEffectss() to get all Effectss from the database
        //returns a list of all Effectss
        [HttpGet]
        public IActionResult GetAllEffectss()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllEffects");

            try
            {
                List<Effects> result = EDBS.GetAllEffects();

                if (result == null || !result.Any())
                {
                    childSpan?.Finish(SpanStatus.InternalError);
                    //Return 404 if no feats are found
                    return NotFound("Feat not found");

                }
                else
                {
                    childSpan?.Finish(SpanStatus.Ok);
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
