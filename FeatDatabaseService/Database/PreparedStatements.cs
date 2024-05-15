﻿using DotNetEnv;
using MySql.Data.MySqlClient;
using FeatDatabaseService;

namespace FeatDatabaseService
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

        //method for creating a new feat in the database
        //This method takes a feat object as a parameter
        //It returns a boolean value, true if the feat was created successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool InsertFeat(Feat feat)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("NAME"),
                    Env.GetString("PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"INSERT INTO Feats (name, description) " +
                            $"VALUES (@name, @description)";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter nameParam = new MySqlParameter("name", feat.Name);
                        MySqlParameter descriptionParam = new MySqlParameter("description", feat.Description);

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

        //method for updating a feat in the database
        //This method takes a feat object as a parameter
        //It returns a boolean value, true if the feat was updated successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool UpdateFeat(Feat feat)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("NAME"),
                    Env.GetString("PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"UPDATE Feats " +
                            $"SET name = @name, description = @description " +
                            $"WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter nameParam = new MySqlParameter("name", feat.Name);
                        MySqlParameter descriptionParam = new MySqlParameter("description", feat.Description);
                        MySqlParameter idParam = new MySqlParameter("id", feat.Id);

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
       

        //method for deleting a feat in the database
        //This method takes a feat object as a parameter
        //It returns a boolean value, true if the feat was deleted successfully and false if it was not
        //This method uses prepared statements to prevent SQL injection
        public bool DeleteFeat(int id)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("NAME"),
                    Env.GetString("PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText =
                            $"DELETE FROM Feats " +
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

        //method for getting a feat by id
        //This method takes an integer id as a parameter
        //It returns a feat object
        //This method uses prepared statements to prevent SQL injection
        public Feat GetFeatById(int id)
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("NAME"),
                    Env.GetString("PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM Feats WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("id", id);

                        // Adds the parameters to the command
                        command.Parameters.Add(idParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a feat object
                        Feat feat = new Feat();

                        // Read the data and add it to the feat object
                        while (reader.Read())
                        {
                            feat.Id = reader.GetInt32("id");
                            feat.Name = reader.GetString("name");
                            feat.Description = reader.GetString("description");
                        }

                        // Return the feat object
                        return feat;
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

        //method for getting all feats
        //This method takes no parameters
        //It returns a list of feat objects
        //This method uses prepared statements to prevent SQL injection
        public List<Feat> GetAllFeats()
        {
            DotNetEnv.Env.Load();

            try
            {
                // Set credentials for the user needed
                dbConnection.SetConnectionCredentials(
                    Env.GetString("NAME"),
                    Env.GetString("PASSWORD"));

                // Use mySqlConnection to open the connection and throw an exception if it fails
                using (MySqlConnection connection = dbConnection.OpenConnection())
                {
                    try
                    {
                        // Create an instance of MySqlCommand
                        MySqlCommand command = new MySqlCommand(null, connection);

                        // Create and prepare an SQL statement.
                        command.CommandText = "SELECT * FROM Feats";

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a list of feat objects
                        List<Feat> feats = new List<Feat>();

                        // Read the data and add it to the list of feat objects
                        while (reader.Read())
                        {
                            Feat feat = new Feat();
                            feat.Id = reader.GetInt32("id");
                            feat.Name = reader.GetString("name");
                            feat.Description = reader.GetString("description");
                            feats.Add(feat);
                        }

                        // Return the list of feat objects
                        return feats;
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