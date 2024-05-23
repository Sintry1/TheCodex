using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using System.Text;
using Polly;
using Resilience;
using EffectsModel;
using WeaponModel;
using System.Linq.Expressions;

namespace WeaponService { 
    public class WeaponServices
    {
        private readonly HttpClient _client;
        private readonly HttpClient effectClient;
        private string weaponUri = "http://localhost:5008/";
        private string effectUri = "http://localhost:5059/";
        private readonly AsyncPolicy<HttpResponseMessage> _policy = PollyPolicies.GetRetryAndCircuitBreakerPolicy();

        // Constructor
        public WeaponServices()
        {
            _client = new HttpClient();
            effectClient = new HttpClient();
        }

        // This method calls the WeaponDatabaseService API to add a new weapon to the database
        // It returns true if the weapon was added successfully and false if it failed
        // If an error occurs, it returns false
        // runs asynchronously
        public async Task<bool> AddWeaponAsync(Weapon weapon)
        {
            try
            {
                _client.BaseAddress = new Uri(weaponUri); // Add the base address of the WeaponDatabaseService API later
                HttpResponseMessage response = await _policy.ExecuteAsync(() => _client.PostAsJsonAsync("WeaponDatabase/", weapon));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        // This method calls the WeaponDatabaseService API to update an existing weapon in the database
        // It returns true if the weapon was updated successfully and false if it failed
        // If an error occurs, it returns false
        public async Task<bool> UpdateWeaponAsync(Weapon weapon)
        {
            try
            {
                _client.BaseAddress = new Uri(weaponUri); // Add the base address of the WeaponDatabaseService API later
                HttpResponseMessage response = await _policy.ExecuteAsync(() => _client.PutAsJsonAsync($"WeaponDatabase/", weapon));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        // This method calls the WeaponDatabaseService API to delete a weapon from the database
        // It returns true if the weapon was deleted successfully and false if it failed
        // If an error occurs, it returns false 
        public async Task<bool> DeleteWeaponAsync(int id)
        {
            try
            {
                _client.BaseAddress = new Uri(weaponUri); // Add the base address of the WeaponDatabaseService API later
                HttpResponseMessage response = await _policy.ExecuteAsync(() => _client.DeleteAsync($"WeaponDatabase/{id}"));
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return false;
            }
        }

        // This method calls the WeaponDatabaseService API to get all weapons from the database
        // It returns a list of all weapons gotten from the WeaponDatabaseService API
        // If an error occurs, it returns an empty list
        // runs asynchronously
        public async Task<List<Weapon>> GetWeaponsAsync()
        {
            try
            {
                _client.BaseAddress = new Uri(weaponUri); // Add the base address of the WeaponDatabaseService API later
                HttpResponseMessage response = await _policy.ExecuteAsync(() => _client.GetAsync("WeaponDatabase/"));


                //if the response is successful, Call EffectsService API to get the effect name
                //and adding the effect name to the weapon object
                //if the response is not successful, return an empty list
                if (response.IsSuccessStatusCode)
                {
                    List<Weapon> weapons = await response.Content.ReadFromJsonAsync<List<Weapon>>();

                    Console.WriteLine("Get weapons");

                    effectClient.BaseAddress = new Uri(effectUri);
                    
                    foreach (Weapon weapon in weapons)
                    {

                        if (weapon.EffectId == null)
                        {
                            weapon.Effect = null;
                        }else 
                        { 
                            // Call EffectsService API to get the effect name
                            // and adding the effect name to the weapon object
                            // if the response is successful, add the effect name to the weapon object
                            // if the response is not successful, add an empty string to the weapon Éffect property
                            HttpResponseMessage effectResponse = await _policy.ExecuteAsync(() => effectClient.GetAsync($"EffectsService/{weapon.EffectId}"));

                            if (effectResponse.IsSuccessStatusCode)
                            {
                                Effects effect = await effectResponse.Content.ReadFromJsonAsync<Effects>();

                                weapon.Effect = effect.Name;
                            }
                            else
                            {
                                Console.WriteLine($"Error: {effectResponse.StatusCode} - {effectResponse.ReasonPhrase}");
                                weapon.Effect = "";
                            }
                        }
                    }
                    return weapons;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<Weapon>();
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new List<Weapon>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return new List<Weapon>();
            }
        }

        // This method calls the WeaponDatabaseService API to get a weapon by its ID
        // It returns the weapon gotten from the WeaponDatabaseService API
        // If an error occurs, it returns null
        // runs asynchronously
        public async Task<Weapon> GetWeaponByIdAsync(int id)
        {
            try
            {
                _client.BaseAddress = new Uri(weaponUri); // Add the base address of the WeaponDatabaseService API later
                HttpResponseMessage response = await _policy.ExecuteAsync(() => _client.GetAsync($"WeaponDatabase/id/{id}"));

                //if the response is successful, Call EffectsService API to get the effect name
                //and adding the effect name to the weapon object
                //if the response is not successful, return an empty list
                if (response.IsSuccessStatusCode)
                {
                    Weapon weapon = await response.Content.ReadFromJsonAsync<Weapon>();

                    if(weapon.EffectId==null)
                    {
                           weapon.Effect = null;
                    } else 
                    {
                        effectClient.BaseAddress = new Uri(effectUri);

                        // Call EffectsService API to get the effect name
                        // and adding the effect name to the weapon object
                        // if the response is successful, add the effect name to the weapon object
                        // if the response is not successful, add an empty string to the weapon Éffect property
                        HttpResponseMessage effectResponse = await _policy.ExecuteAsync(() => effectClient.GetAsync($"EffectsService/{weapon.EffectId}"));

                        if (effectResponse.IsSuccessStatusCode)
                        {

                            Effects effect = await effectResponse.Content.ReadFromJsonAsync<Effects>();
                            weapon.Effect = effect.Name;
                        }
                        else
                        {
                            Console.WriteLine($"Error: {effectResponse.StatusCode} - {effectResponse.ReasonPhrase}");
                            weapon.Effect = "";
                        }
                    }
                    return weapon;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return null;
            }
        }

        // This method calls the WeaponDatabaseService API to get all weapons by type
        // It returns a list of all weapons gotten from the WeaponDatabaseService API
        // If an error occurs, it returns an empty list
        // runs asynchronously
        public async Task<List<Weapon>> GetWeaponsByTypeAsync(string type)
        {
            try
            {

                _client.BaseAddress = new Uri(weaponUri); // Add the base address of the WeaponDatabaseService API later
                HttpResponseMessage response = await _policy.ExecuteAsync(() => _client.GetAsync($"WeaponDatabase/type/{type}"));

                //if the response is successful, Call EffectsService API to get the effect name
                //and adding the effect name to the weapon object
                //if the response is not successful, return an empty list
                if (response.IsSuccessStatusCode)
                {
                    List<Weapon> weapons = await response.Content.ReadFromJsonAsync<List<Weapon>>();

                    effectClient.BaseAddress= new Uri(effectUri);
                    foreach (Weapon weapon in weapons)
                    {
                        if (weapon.EffectId == null)
                        {
                            weapon.Effect = null;
                        }
                        else
                        {
                            // Call EffectsService API to get the effect name
                            // and adding the effect name to the weapon object
                            // if the response is successful, add the effect name to the weapon object
                            // if the response is not successful, add an empty string to the weapon Éffect property
                            HttpResponseMessage effectResponse = await _policy.ExecuteAsync(() => effectClient.GetAsync($"EffectsService/{weapon.EffectId}"));
                            if (effectResponse.IsSuccessStatusCode)
                            {
                                Effects effect = await effectResponse.Content.ReadFromJsonAsync<Effects>();
                                weapon.Effect = effect.Name;
                            }
                            else
                            {
                                Console.WriteLine($"Error: {effectResponse.StatusCode} - {effectResponse.ReasonPhrase}");
                                weapon.Effect = null;
                            }
                        }
                    }
                    return weapons;
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                    return new List<Weapon>();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
                return new List<Weapon>();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
                return new List<Weapon>();
            }
        }
    }
}