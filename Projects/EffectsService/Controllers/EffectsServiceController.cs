using Microsoft.AspNetCore.Mvc;
using System;
using EffectsModel;

namespace EffectsService.Controllers
{
	[ApiController]
	[Route("[controller]")]
    public class EffectsServiceController : ControllerBase
	{

        private EffectsServices ES = new EffectsServices();
        private readonly IHub _sentryHub;

        //Constructor for EffectsServiceController
        public EffectsServiceController(IHub sentryHub)
		{
            _sentryHub = sentryHub;
        }

        //Calls EffectsServices.CreateEffect() to add an effect to the database
        //Returns Success with true if the effect was added, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpPost]
        public async Task<IActionResult> CreateEffect([FromBody] Effects effect)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateEffect");

            try
            {
                bool result = await ES.AddEffect(effect);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to add effect" });
                }
                else
                {
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

        //Calls EffectsServices.UpdateEffect() to update an effect in the database
        //Returns Success with true if the effect was updated, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEffect(int id, [FromBody] Effects effect)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateEffect");

            try
            {
                effect.Id = id;
                bool result = await ES.UpdateEffect(effect);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update effect" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Effect updated successfully" });
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

        //Calls EffectsServices.DeleteEffect() to delete an effect from the database
        //Returns Success with true if the effect was deleted, BadRequest if not
        //returns 500 if an error occurs
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEffect(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteEffect");

            try
            {
                bool result = await ES.DeleteEffect(id);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to delete effect" });
                }
                else
                {
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

        //Calls EffectsServices.GetEffectById() to get an effect from the database
        //Returns the effect if it was found, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEffectById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetEffectById");

            try
            {
                Effects result = await ES.GetEffectById(id);
                if (result == null)
                {
                    return BadRequest(new { Success = false, Message = "Failed to get effect" });
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

        //Calls EffectsServices.GetEffects() to get all effects from the database
        //Returns the effects if they were found, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpGet]
        public async Task<IActionResult> GetAllEffects()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllEffects");

            try
            {
                List<Effects> result = await ES.GetAllEffects();
                if (result == null || !result.Any())
                {
                    return BadRequest(new { Success = false, Message = "Failed to get effects" });
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