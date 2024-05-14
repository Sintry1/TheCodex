﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class WeaponServices
{
    private readonly HttpClient _client();

    // Constructor
    public WeaponServices()
    {
        _client = new HttpClient();
    }

    // This method calls the WeaponDatabaseService API to get all weapons from the database
    // It returns a list of all weapons gotten from the WeaponDatabaseService API
    // If an error occurs, it returns an empty list
    public async Task<List<Weapon>> GetWeaponsAsync()
    {
        try
        {
            _client.BaseAddress = new Uri(); // Add the base address of the WeaponDatabaseService API later
            HttpResponseMessage response = await _client.GetAsync("WeaponDatabaseService");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Weapon>>();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }

        return new List<Weapon>();
    }

    // This method calls the WeaponDatabaseService API to get a weapon by its ID
    // It returns the weapon gotten from the WeaponDatabaseService API
    // If an error occurs, it returns null
    public async Task<Weapon> GetWeaponByIdAsync(int id)
    {
        try
        {
            _client.BaseAddress = new Uri(); // Add the base address of the WeaponDatabaseService API later
            HttpResponseMessage response = await _client.GetAsync($"WeaponDatabaseService/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<Weapon>();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }

        return null;
    }

    // This method calls the WeaponDatabaseService API to add a new weapon to the database
    // It returns true if the weapon was added successfully and false if it failed
    // If an error occurs, it returns false
    public async Task<bool> AddWeaponAsync(Weapon weapon)
    {
        try
        {
            _client.BaseAddress = new Uri(); // Add the base address of the WeaponDatabaseService API later
            HttpResponseMessage response = await _client.PostAsJsonAsync("WeaponDatabaseService", weapon);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }

        return false;
    }

    // This method calls the WeaponDatabaseService API to update an existing weapon in the database
    // It returns true if the weapon was updated successfully and false if it failed
    // If an error occurs, it returns false
    public async Task<bool> UpdateWeaponAsync(Weapon weapon)
    {
        try
        {
            _client.BaseAddress = new Uri(); // Add the base address of the WeaponDatabaseService API later
            HttpResponseMessage response = await _client.PutAsJsonAsync($"WeaponDatabaseService/{weapon.Id}", weapon);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }

        return false;
    }

    // This method calls the WeaponDatabaseService API to delete a weapon from the database
    // It returns true if the weapon was deleted successfully and false if it failed
    // If an error occurs, it returns false 
    public async Task<bool> DeleteWeaponAsync(int id)
    {
        try
        {
            _client.BaseAddress = new Uri(); // Add the base address of the WeaponDatabaseService API later
            HttpResponseMessage response = await _client.DeleteAsync($"WeaponDatabaseService/{id}");
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Request error: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Unexpected error: {e.Message}");
        }

        return false;
    }
}
