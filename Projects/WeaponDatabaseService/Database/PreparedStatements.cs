﻿﻿using DotNetEnv;
using MySql.Data.MySqlClient;
using WeaponModel;


namespace WeaponDatabaseService
{
    public class PreparedStatements
    {
        /* Makes a private dbConnection of type DBConnection, that can only be written to in initialization or in the constructor
        * this ensures that the connection can only be READ by other classes and not modified
        */
        private readonly DBConnection dbConnection;

        //Constructor for PreparedStatements
        private PreparedStatements()
        {
            DotNetEnv.Env.Load();
            dbConnection = DBConnection.CreateInstance();
        }

        //Method for creating an instance of PreparedStatements
        //This is needed as the constructor above is private for security.
        public static PreparedStatements CreateInstance()
        {
            return new PreparedStatements();
        }

        //Method for creating a new weapon in the database
        //This method takes a weapon object as a parameter
        //It returns a boolean value, true if the weapon was created successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool InsertWeapon(Weapon weapon)
        {
            Console.WriteLine("You are now in the weapon database Services");
            DotNetEnv.Env.Load();

            try
            {
                Console.WriteLine("fetching ENV");
                Console.WriteLine(Env.GetString("DB_USER"));
                Console.WriteLine(Env.GetString("DB_PASS"));
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("DB_USER"),
                    Env.GetString("DB_PASS"));

                

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        Console.WriteLine("Connection opened.");
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        Console.WriteLine("Command created.");
                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"INSERT INTO weapons (name, slot, type, effect, minDamage, maxDamage) " +
                            $"VALUES (@name, @slot, @type, @effect, @minDamage, @maxDamage)";

                        Console.WriteLine("Command text set.");
                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter nameParam = new MySqlParameter("name", weapon.Name);
                        MySqlParameter slotParam = new MySqlParameter("slot", weapon.Slot);
                        MySqlParameter typeParam = new MySqlParameter("type", weapon.Type);
                        MySqlParameter effectParam = new MySqlParameter("effect", weapon.EffectId ?? (object)DBNull.Value);
                        MySqlParameter minDamageParam = new MySqlParameter("minDamage", weapon.MinDamage);
                        MySqlParameter maxDamageParam = new MySqlParameter("maxDamage", weapon.MaxDamage);

                        Console.WriteLine("Parameters set.");
                        // Adds the parameters to the command
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(slotParam);
                        command.Parameters.Add(typeParam);
                        command.Parameters.Add(effectParam);
                        command.Parameters.Add(minDamageParam);
                        command.Parameters.Add(maxDamageParam);

                        Console.WriteLine("Parameters added.");

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();
                        Console.WriteLine("Command prepared.");

                        // Execute the query
                        object result = command.ExecuteScalar();
                        Console.WriteLine("Query executed.");
                        // Return true if no exceptions are thrown
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return false
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        return false;
                    }
                    finally
                    {
                        // Close the connection at the end
                        dbConnection.CloseConnection();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        //Method for updating a weapon in the database
        //This method takes a weapon object as a parameter
        //It returns a boolean value, true if the weapon was updated successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool UpdateWeapon(Weapon weapon)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("DB_USER"),
                    Env.GetString("DB_PASS"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        string commandText = "UPDATE weapons SET ";
                        if (weapon.Name != null)
                        {
                            commandText += "name = @name, ";
                            command.Parameters.AddWithValue("@name", weapon.Name);
                        }
                        if (weapon.Slot != null)
                        {
                            commandText += "slot = @slot, ";
                            command.Parameters.AddWithValue("@slot", weapon.Slot);
                        }
                        if (weapon.Type != null)
                        {
                            commandText += "type = @type, ";
                            command.Parameters.AddWithValue("@type", weapon.Type);
                        }
                        if (weapon.EffectId != null)
                        {
                            commandText += "effect = @effect, ";
                            command.Parameters.AddWithValue("@effect", weapon.EffectId);
                        }
                        if (weapon.MinDamage != null)
                        {
                            commandText += "minDamage = @minDamage, ";
                            command.Parameters.AddWithValue("@minDamage", weapon.MinDamage);
                        }
                        if (weapon.MaxDamage != null)
                        {
                            commandText += "maxDamage = @maxDamage, ";
                            command.Parameters.AddWithValue("@maxDamage", weapon.MaxDamage);
                        }
                        commandText = commandText.TrimEnd(',', ' ') + " WHERE id = @id";
                        command.CommandText = commandText;

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("@id", weapon.Id);
                        command.Parameters.Add(idParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        object result = command.ExecuteScalar();

                        // Return true if no exceptions are thrown
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return false
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        return false;
                    }
                    finally
                    {
                        // Close the connection at the end
                        dbConnection.CloseConnection();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Method for deleting a weapon from the database
        //This method takes an integer id as a parameter
        //It returns a boolean value, true if the weapon was deleted successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool DeleteWeapon(int id)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("DB_USER"),
                    Env.GetString("DB_PASS"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = $"DELETE FROM weapons WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("id", id);

                        // Adds the parameters to the command
                        command.Parameters.Add(idParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        object result = command.ExecuteScalar();

                        // Return true if no exceptions are thrown
                        return true;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return false
                        // We may want to implement secure logging to store the error message
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        return false;
                    }
                    finally
                    {
                        // Close the connection at the end
                        dbConnection.CloseConnection();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Method for getting all weapons from the database
        //This method returns a list of weapons
        //This method uses prepared statements to prevent SQL injection
        public List<Weapon> GetAllWeapons()
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("DB_USER"),
                    Env.GetString("DB_PASS"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM weapons";

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a list of weapons
                        List<Weapon> weapons = new List<Weapon>();

                        // Read the data and add it to the list
                        while (reader.Read())
                        {
                            Weapon weapon = new Weapon
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Slot = reader.GetString("slot"),
                                Type = reader.GetString("type"),
                                EffectId = reader.IsDBNull(reader.GetOrdinal("effect")) ? (int?)null : reader.GetInt32("effect"),
                                MinDamage = reader.GetInt32("minDamage"),
                                MaxDamage = reader.GetInt32("maxDamage")
                            };

                            Console.WriteLine("got a weapon");
                            weapons.Add(weapon);
                        }
                        Console.WriteLine("got a list weapon");
                        // Return the list of weapons
                        return weapons;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return an empty list
                        // We may want to implement secure logging to store the error message
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        return null;
                    }
                    finally
                    {
                        // Close the connection at the end
                        dbConnection.CloseConnection();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Method for getting a weapon by its id from the database
        //This method takes an integer id as a parameter
        //It returns a weapon object
        //This method uses prepared statements to prevent SQL injection
        public Weapon GetWeaponById(int id)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("DB_USER"),
                    Env.GetString("DB_PASS"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM weapons WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("id", id);

                        // Adds the parameters to the command
                        command.Parameters.Add(idParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a weapon object
                        Weapon weapon = new Weapon();

                        // Read the data and add it to the weapon object
                        while (reader.Read())
                        {
                            weapon.Id = reader.GetInt32("id");
                            weapon.Name = reader.GetString("name");
                            weapon.Slot = reader.GetString("slot");
                            weapon.Type = reader.GetString("type");
                            weapon.EffectId = reader.IsDBNull(reader.GetOrdinal("effect")) ? (int?)null : reader.GetInt32("effect");
                            weapon.MinDamage = reader.GetInt32("minDamage");
                            weapon.MaxDamage = reader.GetInt32("maxDamage");
                        }

                        // Return the weapon object
                        return weapon;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return null
                        // We may want to implement secure logging to store the error message
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        return null;
                    }
                    finally
                    {
                        // Close the connection at the end
                        dbConnection.CloseConnection();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Method for getting a list of weapon by their type
        //This method takes a string type as a parameter
        //It returns a list of weapon objects
        //This method uses prepared statements to prevent SQL injection
        public List<Weapon> GetWeaponsByType(string type)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("DB_USER"),
                    Env.GetString("DB_PASS"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM weapons WHERE type = @type";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter typeParam = new MySqlParameter("type", type);

                        // Adds the parameters to the command
                        command.Parameters.Add(typeParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a list of weapons
                        List<Weapon> weapons = new List<Weapon>();

                        // Read the data and add it to the list
                        while (reader.Read())
                        {
                            Weapon weapon = new Weapon
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Slot = reader.GetString("slot"),
                                Type = reader.GetString("type"),
                                EffectId = reader.IsDBNull(reader.GetOrdinal("effect")) ? (int?)null : reader.GetInt32("effect"),
                                MinDamage = reader.GetInt32("minDamage"),
                                MaxDamage = reader.GetInt32("maxDamage")
                            };


                            weapons.Add(weapon);

                        }

                        // Return the list of weapons
                        return weapons;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return an empty list
                        // We may want to implement secure logging to store the error message
                        Console.WriteLine($"Error executing query: {ex.Message}");
                        return null;
                    }
                    finally
                    {
                        // Close the connection at the end
                        dbConnection.CloseConnection();
                        Console.WriteLine("Connection closed.");
                    }
                }
            }
            catch (Exception)
            {
                   return null; 
            }
        }

    } 
}