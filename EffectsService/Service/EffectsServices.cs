using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace EffectsService
{
    public class EffectsServices
    {
        private readonly HttpClient _client;
        private string uri = "http://localhost:5231/";
        
        //constructor for EffectsServices
        public EffectsServices()
        {

        }

        //Method for adding an effect to the database
        //Makes an API call to the EffectsDatabaseController to add an effect to the database
        //Returns true if the effect was added successfully, false if not
        //runs asynchronously
        public async Task<bool> AddEffect(Effects effect)
        {
            try
            {
                //Serializes the effect object to JSON
                string jsonData = JsonConvert.SerializeObject(effect);

                //Creates a new StringContent object with the JSON data
                var dataJson = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json"
                    );

                //Makes a POST request to the EffectsDatabaseController to add an effect to the database
                var response = await _client.PostAsync(uri + "EffectsDatabase", dataJson);

                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Method for updating an effect in the database
        //Makes an API call to the EffectsDatabaseController to update an effect in the database
        //takes an effect object as parameter
        //returns true if the effect was updated successfully, false if not
        //runs asynchronously
        public async Task<bool> UpdateEffect(Effects effect)
        {
            try
            {
                //Serializes the effect object to JSON
                string jsonData = JsonConvert.SerializeObject(effect);

                //Creates a new StringContent object with the JSON data
                var dataJson = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json"
                    );

                //Makes a PUT request to the EffectsDatabaseController to update an effect in the database
                var response = await _client.PutAsync(uri + "EffectsDatabase", dataJson);

                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Method for deleting an effect from the database
        //Makes an API call to the EffectsDatabaseController to delete an effect from the database
        //takes an int id as parameter
        //returns true if the effect was deleted successfully, false if not
        //runs asynchronously
        public async Task<bool> DeleteEffect(int id)
        {
            try
            {
                //Makes a DELETE request to the EffectsDatabaseController to delete an effect from the database
                var response = await _client.DeleteAsync(uri + "EffectsDatabase/" + id);

                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Method for getting an effect by id from the database
        //Makes an API call to the EffectsDatabaseController to get an effect by id from the database
        //takes an int id as parameter
        //returns an effect object
        //runs asynchronously
        public async Task<Effects> GetEffectById(int id)
        {
            try
            {
                //Makes a GET request to the EffectsDatabaseController to get an effect by id from the database
                var response = await _client.GetAsync(uri + "EffectsDatabase/" + id);

                //Returns the effect object if the response is successful, null if not
                if (response.IsSuccessStatusCode)
                {
                    var effect = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Effects>(effect);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //Method for getting all effects from the database
        //Makes an API call to the EffectsDatabaseController to get all effects from the database
        //returns a list of effect objects
        //runs asynchronously
        public async Task<List<Effects>> GetAllEffects()
        {
            try
            {
                //Makes a GET request to the EffectsDatabaseController to get all effects from the database
                var response = await _client.GetAsync(uri + "EffectsDatabase");

                //Returns the list of effect objects if the response is successful, null if not
                if (response.IsSuccessStatusCode)
                {
                    var effects = await response.Content.ReadAsStringAsync();
                    List<Effects> effect = JsonConvert.DeserializeObject<List<Effects>>(effects);
                    return effect;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }
}