using System;
using System.Net.Http;
using Newtonsoft.Json;

namespace FeatService { 
	public class FeatServices
	{

		private readonly HttpClient _client;
		private string uri = "http://localhost:5008/";

		//Constructor for FeatServices
		public FeatServices()
		{
		}

        //Method for adding a feat to the database
        //Makes an API call to the FeatDatabaseController to add a feat to the database
        //Returns true if the feat was added successfully, false if not
        //runs asynchronously
        public async Task<bool> AddFeat(Feat feat)
		{
			try
			{
				//Makes a POST request to the FeatDatabaseController to add a feat to the database
				var response = _client.PostAsJsonAsync(uri + "FeatDatabase", feat).Result;
				//Returns true if the response is successful, false if not
				return response.IsSuccessStatusCode;
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				return false;
			}
		}

		//Method for updating a feat in the database
		//Makes an API call to the FeatDatabaseController to update a feat in the database
		//takes a feat object as parameter
		//returns true if the feat was updated successfully, false if not
		//runs asynchronously
		public async Task<bool> UpdateFeat(Feat feat)
		{
            try
			{
                //Makes a PUT request to the FeatDatabaseController to update a feat in the database
                var response = await _client.PutAsJsonAsync(uri + "FeatDatabase", feat);
                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//Method for deleting a feat from the database
		//Makes an API call to the FeatDatabaseController to delete a feat from the database
		//takes an int id as parameter
		//returns true if the feat was deleted successfully, false if not
		//runs asynchronously
		public async Task<bool> DeleteFeat(int id)
		{
            try
			{
                //Makes a DELETE request to the FeatDatabaseController to delete a feat from the database
                var response = await _client.DeleteAsync(uri + "FeatDatabase/" + id);
                //Returns true if the response is successful, false if not
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return false;
            }
        }

		//Method for getting a feat by id
		//Makes an API call to the FeatDatabaseController to get a feat by id
		//takes an int id as parameter
		//returns a feat object
		//runs asynchronously
		public async Task<Feat> GetFeatById(int id)
		{
            try
			{
                //Makes a GET request to the FeatDatabaseController to get a feat by id
                var response = await _client.GetAsync(uri + "FeatDatabase/" + id);
                //Returns the feat object if the response is successful, null if not
                if (response.IsSuccessStatusCode)
				{
					var json = await response.Content.ReadAsStringAsync();
                    
					var Feat = JsonConvert.DeserializeObject<Feat>(json);
                    return Feat;
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

		//Method for getting all feats
		//Makes an API call to the FeatDatabaseController to get all feats
		//returns a list of feat objects
		//runs asynchronously
		public async Task<List<Feat>> GetAllFeats()
		{
            try
			{
                //Makes a GET request to the FeatDatabaseController to get all feats
                var response = await _client.GetAsync(uri + "FeatDatabase");

                //Returns a list of feat objects if the response is successful, null if not
                if (response.IsSuccessStatusCode)
				{
                    var json = await response.Content.ReadAsStringAsync();
                    
                    var Feats = JsonConvert.DeserializeObject<List<Feat>>(json);
                    return Feats;
                }
                else
				{
                    return null;
                }
            }//catches exceptions and returns null
            catch (Exception e)
			{
                Console.WriteLine(e.Message);
                return null;
            }
        }


	}
}