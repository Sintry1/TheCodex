using Microsoft.AspNetCore.Mvc;

namespace FeatDatabaseService
{
    [ApiController]
    [Route("[controller]")]
    public class FeatDatabaseController : ControllerBase
    {

        //abbreviation for FeatDatabaseServices
        private FeatDatabaseServices FDBS = new FeatDatabaseServices();
        private readonly IHub _sentryHub;

        //constructor for FeatDatabaseController
        public FeatDatabaseController(IHub sentryHub)
        {
            _sentryHub = sentryHub;
        }

        //calls FeatDatabaseServices.AddFeat() to add a Feat to the database
        //returns the true if it was added, false if not
        [HttpPost]
        public IActionResult CreateFeat(Feat Feat)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateFeat");
            try
            {
                bool result = FDBS.CreateFeat(Feat);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to add Feat" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Feat added successfully" });
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

        //calls FeatDatabaseServices.UpdateFeat() to update a Feat in the database
        //returns true if the Feat was updated, false if not
        [HttpPut]
        public IActionResult UpdateFeat(Feat Feat)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateFeat");

            try
            {
                bool result = FDBS.UpdateFeat(Feat);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update Feat" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Feat updated successfully" });
                };
            } catch (Exception e)
            {
                //Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //calls FeatDatabaseServices.DeleteFeat() to delete a Feat from the database
        //returns the Feat that was deleted
        [HttpDelete("{id}")]
        public IActionResult DeleteFeat(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteFeat");

            try
            {
                bool result = FDBS.DeleteFeat(id);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to delete Feat" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Feat deleted successfully" });
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

        //calls FeatDatabaseServices.GetFeatById() to get a Feat by its id
        //returns the Feat with the specified id
        [HttpGet("{id}")]
        public IActionResult GetFeatById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetFeatById");

            try
            {
                Feat result = FDBS.GetFeatById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            } catch (Exception e)
            {
                //Log errors to Sentry when added.
                _sentryHub.CaptureException(e);
                childSpan?.Finish(e); // Return 500 Internal Server Error if an exception occurs

                //Return 500 if an error occurs
                return StatusCode(500);
            }
        }

        //calls FeatDatabaseServices.GetAllFeats() to get all Feats from the database
        //returns a list of all Feats
        [HttpGet]
        public IActionResult GetAllFeats()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllFeats");
            try
            {
                List<Feat> result = FDBS.GetAllFeats();

                if (result == null || !result.Any())
                {
                    //Return 404 if no feats are found
                    return NotFound();

                }
                else
                {
                    return Ok(result);
                }
            } catch(Exception e)
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
