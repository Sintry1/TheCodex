﻿using DotNetEnv;
using MySql.Data.MySqlClient;
using AttacksDatabaseService;

namespace AttacksDatabaseService
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

        //method for adding a new attack to the database
        //This method takes an attack object as a parameter
        //It returns a boolean value, true if the attack was added successfully and false if it was not
        //also return false if an exception is thrown for the sake of fault isolation
        //This method uses prepared statements to prevent SQL injection
        public bool InsertAttack(Attack attack)
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
                            $"INSERT INTO attacks (name, damage, weapon_requirement) " +
                            $"VALUES (@name, @damage, @weapon_requirement)";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter nameParam = new MySqlParameter("name", attack.Name);
                        MySqlParameter damageParam = new MySqlParameter("damage", attack.Damage);
                        MySqlParameter weaponRequirementParam = new MySqlParameter("weapon_requirement", attack.WeaponRequirement);

                        // Adds the parameters to the command
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(damageParam);
                        command.Parameters.Add(weaponRequirementParam);

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

        //method for updating an attack in the database
        //This method takes an attack object as a parameter
        //It returns a boolean value, true if the attack was updated successfully and false if it was not
        //also return false if an exception is thrown for the sake of fault isolation
        //This method uses prepared statements to prevent SQL injection
        public bool UpdateAttack(Attack attack)
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
                            $"UPDATE attacks SET name = @name, damage = @damage, weapon_requirement = @weapon_requirement " +
                            $"WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("id", attack.Id);
                        MySqlParameter nameParam = new MySqlParameter("name", attack.Name);
                        MySqlParameter damageParam = new MySqlParameter("damage", attack.Damage);
                        MySqlParameter weaponRequirementParam = new MySqlParameter("weapon_requirement", attack.WeaponRequirement);

                        // Adds the parameters to the command
                        command.Parameters.Add(idParam);
                        command.Parameters.Add(nameParam);
                        command.Parameters.Add(damageParam);
                        command.Parameters.Add(weaponRequirementParam);

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

        //method for deleting an attack from the database
        //This method takes an attack object as a parameter
        //It returns a boolean value, true if the attack was deleted successfully and false if it was not   
        //also return false if an exception is thrown for the sake of fault isolation
        //This method uses prepared statements to prevent SQL injection
        public bool DeleteAttack(int id)
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
                            $"DELETE FROM attacks WHERE id = @id";

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

        //method for getting all attacks from the database
        //This method returns a list of attack objects
        //also return null if an exception is thrown for the sake of fault isolation
        //This method uses prepared statements to prevent SQL injection
        public List<Attack> GetAllAttacks()
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
                            $"SELECT * FROM attacks";

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Create a list of attacks
                        List<Attack> attacks = new List<Attack>();

                        // Read the data and add it to the list
                        while (reader.Read())
                        {
                            Attack attack = new Attack
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Damage = reader.GetInt32("damage"),
                                WeaponRequirement = reader.GetString("weapon_requirement")
                            };

                            attacks.Add(attack);
                        }

                        // Return the list of attacks
                        return attacks;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return null
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

        //method for getting an attack by id from the database
        //This method takes an id as a parameter
        //It returns an attack object
        //also return null if an exception is thrown for the sake of fault isolation
        //This method uses prepared statements to prevent SQL injection
        public Attack GetAttackById(int id)
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
                            $"SELECT * FROM attacks WHERE id = @id";

                        // Sets mySQL parameters for the prepared statement
                        MySqlParameter idParam = new MySqlParameter("id", id);

                        // Adds the parameters to the command
                        command.Parameters.Add(idParam);

                        // Call Prepare after setting the Commandtext and Parameters.
                        command.Prepare();

                        // Execute the query
                        MySqlDataReader reader = command.ExecuteReader();

                        // Read the data and return the attack
                        if (reader.Read())
                        {
                            Attack attack = new Attack
                            {
                                Id = reader.GetInt32("id"),
                                Name = reader.GetString("name"),
                                Damage = reader.GetInt32("damage"),
                                WeaponRequirement = reader.GetString("weapon_requirement")
                            };

                            return attack;
                        }

                        // Return null if no attack is found
                        return null;
                    }
                    catch (MySqlException ex)
                    {
                        // Handle the exception (e.g., log it) and return null
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