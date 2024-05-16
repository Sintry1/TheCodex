using Microsoft.AspNetCore.Mvc;

namespace EffectsDatabaseService
{
    [ApiController]
    [Route("[controller]")]
    public class EffectsDatabaseController : ControllerBase
    {

        //abbreviation for EffectsDatabaseServices
        private EffectsDatabaseServices FDBS = new EffectsDatabaseServices();

        //constructor for EffectsDatabaseController
        public EffectsDatabaseController()
        {

        }

        //calls EffectsDatabaseServices.AddEffects() to add a effect to the database
        //returns the true if it was added, false if not
        [HttpPost]
        public IActionResult CreateEffects(Effects effect)
        {
            try
            {
                bool result = FDBS.AddEffect(effect);
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
                //Return 500 if an error occurs
                return StatusCode(500);
            }

        }

        //calls EffectsDatabaseServices.UpdateEffect() to update a effect in the database
        //returns true if the effect was updated, false if not
        [HttpPut]
        public IActionResult UpdateEffects(Effects effect)
        {
            try
            {
                bool result = FDBS.UpdateEffect(effect);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update effect" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Effect updated successfully" });
                };
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //calls EffectsDatabaseServices.DeleteEffect() to delete a effect from the database
        //returns the effect that was deleted
        [HttpDelete("{id}")]
        public IActionResult DeleteEffects(int id)
        {
            try
            {
                bool result = FDBS.DeleteEffect(id);
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
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //calls EffectsDatabaseServices.GetEffectsById() to get a effect by its id
        //returns the effect with the specified id
        [HttpGet("{id}")]
        public IActionResult GetEffectsById(int id)
        {
            try
            {
                Effects result = FDBS.GetEffectById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                //Log errors to Sentry when added.
                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //calls EffectsDatabaseServices.GetAllEffectss() to get all Effectss from the database
        //returns a list of all Effectss
        [HttpGet]
        public IActionResult GetAllEffectss()
        {
            try
            {
                List<Effects> result = FDBS.GetAllEffects();

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
                //Return 500 if an error occurs
                return StatusCode(500);
            }

        }

    }
}
