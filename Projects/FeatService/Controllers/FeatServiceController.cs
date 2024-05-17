using System;
using Microsoft.AspNetCore.Mvc;

namespace FeatService
{
    //Feat service API endpoint
    [ApiController]
    [Route("[controller]")]
    public class FeatServiceController : ControllerBase
    {

        private readonly IHub _sentryHub;
        private FeatServices FS = new FeatServices();

        //Constructor for FeatServiceController
        public FeatServiceController(IHub sentryHub)
        {
            _sentryHub = sentryHub;
        }

        //Calls FeatServices.AddFeat() to add a feat to the database
        //Returns Success with true if the feat was added, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpPost]
        public async Task<IActionResult> CreateFeat(Feat feat)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("CreateFeat");
            try
            {
                bool result = await FS.AddFeat(feat);
                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to add feat" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Feat added successfully" });
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

        //Calls FeatServices.UpdateFeat() to update a feat in the database
        //Returns Success with true if the feat was updated, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpPut]
        public async Task<IActionResult> UpdateFeat(Feat feat)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("UpdateFeat");

            try
            {
                bool result = await FS.UpdateFeat(feat);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to update feat" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Feat updated successfully" });
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

        //Calls FeatServices.DeleteFeat() to delete a feat from the database
        //Returns Success with true if the feat was deleted, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpDelete]
        public async Task<IActionResult> DeleteFeat(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("DeleteFeat");

            try
            {
                bool result = await FS.DeleteFeat(id);

                if (!result)
                {
                    return BadRequest(new { Success = false, Message = "Failed to delete feat" });
                }
                else
                {
                    return Ok(new { Success = true, Message = "Feat deleted successfully" });
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

        //Calls FeatServices.GetFeatById() to get a feat from the database by id
        //return an Ok status code with the feat if successful, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatById(int id)
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetFeatById");

            try
            {
                Feat result = await FS.GetFeatById(id);

                if (result == null)
                {
                    return BadRequest(new { Success = false, Message = "Failed to get feat" });
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

        //Calls FeatServices.GetFeats() to get all feats from the database
        //return an Ok status code with the list of feats if successful, BadRequest if not
        //returns 500 if an error occurs
        //Runs asynchronously
        [HttpGet]
        public async Task<IActionResult> GetAllFeats()
        {
            var childSpan = _sentryHub.GetSpan()?.StartChild("GetAllFeats");

            try
            {
                List<Feat> result = await FS.GetAllFeats();

                if (result == null || !result.Any())
                {
                    return BadRequest(new { Success = false, Message = "Failed to get feats" });
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
