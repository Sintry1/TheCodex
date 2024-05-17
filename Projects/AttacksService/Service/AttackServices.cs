using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace AttacksService
{
    public class AttackServices
    {
        private readonly HttpClient _client;
        private string uri = "http://localhost:5261/";
        
        //constructor for EffectsServices
        public AttackServices()
        {

        }

        //Method for adding an attack to the database
        //Makes an API call to the AttackDatabaseController to add an attack to the database
        //Returns true if the attack was added successfully, false if not
        //runs asynchronously
        public async Task<bool> AddAttack(Attack attack)
        {
            try
            {
                //Serializes the attack object to JSON
                string jsonData = JsonConvert.SerializeObject(attack);

                //Creates a new StringContent object with the JSON data
                var dataJson = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json"
                    );

                //Makes a POST request to the AttackDatabaseController to add an attack to the database
                var response = await _client.PostAsync(uri + "AttackDatabase", dataJson);

                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Method for updating an attack in the database
        //Makes an API call to the AttackDatabaseController to update an attack in the database
        //takes an attack object as parameter
        //returns true if the attack was updated successfully, false if not
        //runs asynchronously
        public async Task<bool> UpdateAttack(Attack attack)
        {
            try
            {
                //Serializes the attack object to JSON
                string jsonData = JsonConvert.SerializeObject(attack);

                //Creates a new StringContent object with the JSON data
                var dataJson = new StringContent(
                    jsonData,
                    Encoding.UTF8,
                    "application/json"
                    );

                //Makes a PUT request to the AttackDatabaseController to update an attack in the database
                var response = await _client.PutAsync(uri + "AttackDatabase", dataJson);

                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Method for deleting an attack from the database
        //Makes an API call to the AttackDatabaseController to delete an attack from the database
        //takes an int id as parameter
        //returns true if the attack was deleted successfully, false if not
        //runs asynchronously
        public async Task<bool> DeleteAttack(int id)
        {
            try
            {
                //Makes a DELETE request to the AttackDatabaseController to delete an attack from the database
                var response = await _client.DeleteAsync(uri + "AttackDatabase/" + id);

                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //Method for getting an attack by id from the database
        //Makes an API call to the AttackDatabaseController to get an attack by id from the database
        //takes an int id as parameter
        //returns an attack object
        //runs asynchronously
        public async Task<Attack> GetAttackById(int id)
        {
            try
            {
                //Makes a GET request to the AttackDatabaseController to get an attack by id from the database
                var response = await _client.GetAsync(uri + "AttackDatabase/" + id);

                //Returns the attack object if the response is successful, null if not
                if (response.IsSuccessStatusCode)
                {
                    var attack = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Attack>(attack);
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

        //Method for getting all attacks from the database
        //Makes an API call to the AttackDatabaseController to get all attacks from the database
        //returns a list of attack objects
        //runs asynchronously
        public async Task<List<Attack>> GetAllAttacks()
        {
            try
            {
                //Makes a GET request to the AttackDatabaseController to get all attacks from the database
                var response = await _client.GetAsync(uri + "AttackDatabase");

                //Returns the list of attack objects if the response is successful, null if not
                if (response.IsSuccessStatusCode)
                {
                    var attacks = await response.Content.ReadAsStringAsync();
                    List<Attack> attack = JsonConvert.DeserializeObject<List<Attack>>(attacks);
                    return attack;
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