using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Polly;
using Resilience;

namespace CreatureService
{
    public class CreatureServices
    {

        private readonly HttpClient _client;
        private readonly string uri = "http://localhost:5030";
        private readonly AsyncPolicy<HttpResponseMessage> _policy = PollyPolicies.GetRetryAndCircuitBreakerPolicy();

        public CreatureServices()
        {
        }

        // Create a new creature entry
        // Takes a creature object
        // Calls the CreatureDatabaseService to create a new creature entry
        // Returns true if successful, false if not
        // Returns false if an error occurs
        // Runs asynchronously

        public async Task<bool> CreateCreature(Creature creature)
        {
            try
            {
                // Serialize the creature object to JSON
                string jsonData = JsonConvert.SerializeObject(creature);

                // Create a new StringContent object with the JSON data
                var dataJson = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json");

                // Make a POST request to the CreatureDatabaseService to create a new creature entry
                var response = await _policy.ExecuteAsync(() => _client.PostAsync(uri + "/CreatureDatabase", dataJson));

                // Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // Update an existing creature entry
        // Takes a creature object
        // Calls the CreatureDatabaseService to update an existing creature entry
        // Returns true if successful, false if not
        // Returns false if an error occurs
        // Runs asynchronously
        public async Task<bool> UpdateCreature(Creature creature)
        {
            try
            {
                // Serialize the creature object to JSON
                string jsonData = JsonConvert.SerializeObject(creature);

                // Create a new StringContent object with the JSON data
                var dataJson = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json");

                // Make a PUT request to the CreatureDatabaseService to update an existing creature entry
                var response = await _policy.ExecuteAsync(() => _client.PutAsync(uri + "/CreatureDatabase", dataJson));

                // Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // Delete a creature entry
        // Takes a name of the creature to delete
        // Calls the CreatureDatabaseService to delete a creature entry
        // Returns true if successful, false if not
        // Returns false if an error occurs
        // Runs asynchronously
        public async Task<bool> DeleteCreature(string creatureName)
        {
            try
            {
                // Make a DELETE request to the CreatureDatabaseService to delete a creature entry
                var response = await _policy.ExecuteAsync(() => _client.DeleteAsync(uri + "/CreatureDatabase?creatureName=" + creatureName));

                // Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                Console.WriteLine(e.Message);
                return false;
            }
        }

        // Get a creature entry
        // Takes a name of the creature to get
        // Calls the CreatureDatabaseService to get a creature entry
        // Returns a creature object if successful, null if not
        // Runs asynchronously
        public async Task<Creature> GetCreature(string creatureName)
        {
            try
            {
                // Make a GET request to the CreatureDatabaseService to get a creature entry
                var response = await _policy.ExecuteAsync(() => _client.GetAsync(uri + "/CreatureDatabase?creatureName=" + creatureName));

                // Returns the creature object if the response is successful, null if not
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Creature>(content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                Console.WriteLine(e.Message);
                return null;
            }
        }

        // Get all creatures
        // Calls the CreatureDatabaseService to get all creatures
        // Returns a list of all creatures, and null if no success code is returned
        // Returns null if an error occurs
        // Runs asynchronously
        public async Task<List<Creature>> GetAllCreatures()
        {
            try
            {
                // Make a GET request to the CreatureDatabaseService to get all creatures
                var response = await _policy.ExecuteAsync(() => _client.GetAsync(uri + "/CreatureDatabase"));

                // Returns the list of creatures if the response is successful, null if not
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Creature>>(content);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                // Log errors to Sentry when added.
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }

}