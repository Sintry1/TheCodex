﻿using DotNetEnv;
using MySql.Data.MySqlClient;

namespace EffectsDatabaseService
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

        //method for creating a new effect in the database
        //This method takes a effect object as a parameter
        //It returns a boolean value, true if the effect was created successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool InsertEffect(Effects effect)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("EFFECTDB_USERNAME"),
                    Env.GetString("EFFECTDB_PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"INSERT INTO effects (name, description) " +
                            $"VALUES (@name, @description)";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter nameParam = new MySqlParameter("name", effect.Name);
                        MySqlParameter descriptionParam = new MySqlParameter("description", effect.Description);

                        // Adds the parameters to the command
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(descriptionParam);

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

        //method for updating a effect in the database
        //This method takes a effect object as a parameter
        //It returns a boolean value, true if the effect was updated successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool UpdateEffect(Effects effect)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("EFFECTDB_USERNAME"),
                    Env.GetString("EFFECTDB_PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"UPDATE effects " +
                            $"SET name = @name, description = @description " +
                            $"WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter nameParam = new MySqlParameter("name", effect.Name);
                        MySqlParameter descriptionParam = new MySqlParameter("description", effect.Description);
                        MySqlParameter idParam = new MySqlParameter("id", effect.Id);

                        // Adds the parameters to the command
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(descriptionParam);
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

        //method for deleting a effect in the database
        //This method takes a effect object as a parameter
        //It returns a boolean value, true if the effect was deleted successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool DeleteEffect(int id)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("EFFECTDB_USERNAME"),
                    Env.GetString("EFFECTDB_PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"DELETE FROM effects " +
                            $"WHERE id = @id";

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

        //method for getting a effect by id
        //This method takes an integer id as a parameter
        //It returns a effect object
        //This method uses prepared statements to prevent SQL injection
        public Effects GetEffectById(int id)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("EFFECTDB_USERNAME"),
                    Env.GetString("EFFECTDB_PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM effects WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("id", id);

                        // Adds the parameters to the command
                        command.Parameters.Add(idParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a effect object
                        Effects effect = new Effects();

                        // Read the data and add it to the effect object
                        while (reader.Read())
                        {
                            effect.Id = reader.GetInt32("id");
                            effect.Name = reader.GetString("name");
                            effect.Description = reader.GetString("description");
                        }

                        // Return the effect object
                        return effect;
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

        //method for getting all effects
        //This method takes no parameters
        //It returns a list of effect objects
        //This method uses prepared statements to prevent SQL injection
        public List<Effects> GetAllEffects()
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("EFFECTDB_USERNAME"),
                    Env.GetString("EFFECTDB_PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM Effects";

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a list of effect objects
                        List<Effects> effects = new List<Effects>();

                        // Read the data and add it to the list of effect objects
                        while (reader.Read())
                        {
                            Effects effect = new Effects();
                            effect.Id = reader.GetInt32("id");
                            effect.Name = reader.GetString("name");
                            effect.Description = reader.GetString("description");
                            effects.Add(effect);
                        }

                        // Return the list of effect objects
                        return effects;
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


    }
}