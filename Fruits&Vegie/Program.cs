using System;
using System.Data.SqlClient;

namespace VegetableFruitApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Data Source=EUGENE1984;Initial Catalog=FV;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection to the database successful!\n");

                   
                    Console.WriteLine("All information from the table:\n");
                    DisplayAllData(connection);

                   
                    Console.WriteLine("\nAll vegetable and fruit names:\n");
                    DisplayAllNames(connection);

                   
                    Console.WriteLine("\nAll colors:\n");
                    DisplayAllColors(connection);

                    
                    Console.WriteLine("\nMaximum calories: " + GetMaxCalories(connection));
                    Console.WriteLine("Minimum calories: " + GetMinCalories(connection));
                    Console.WriteLine("Average calories: " + GetAvgCalories(connection));

                    
                    Console.WriteLine("\nNumber of vegetables: " + GetVegetableCount(connection));
                    Console.WriteLine("Number of fruits: " + GetFruitCount(connection));

                
                    Console.WriteLine("\nNumber of vegetables and fruits of a specified color (Green): " + GetCountByColor(connection, "Green"));

                    
                    Console.WriteLine("\nNumber of vegetables and fruits for each color:\n");
                    DisplayCountByColor(connection);

                    Console.WriteLine("\nVegetables and fruits with calories below 40:\n");
                    DisplayByCaloriesBelow(connection, 40);

                    Console.WriteLine("\nVegetables and fruits with calories above 50:\n");
                    DisplayByCaloriesAbove(connection, 50);

                    Console.WriteLine("\nVegetables and fruits with calories between 30 and 60:\n");
                    DisplayByCaloriesInRange(connection, 30, 60);


                    Console.WriteLine("\nVegetables and fruits with colors yellow or red:\n");
                    DisplayByColors(connection, "Yellow", "Red");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error connecting to the database: {ex.Message}");
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        connection.Close();
                        Console.WriteLine("\nDisconnected from the database");
                    }
                }
            }

            Console.ReadLine(); 
        }


        static void DisplayAllData(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM VegetablesAndFruits", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Color"]}, {reader["Calories"]} kcal");
                }
            }
        }

     
        static void DisplayAllNames(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT Name FROM VegetablesAndFruits", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Name"]);
                }
            }
        }

       
        static void DisplayAllColors(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT DISTINCT Color FROM VegetablesAndFruits WHERE Color IS NOT NULL", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine(reader["Color"]);
                }
            }
        }

        
        static int GetMaxCalories(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT MAX(Calories) FROM VegetablesAndFruits", connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

       
        static int GetMinCalories(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT MIN(Calories) FROM VegetablesAndFruits", connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

      
        static int GetAvgCalories(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT AVG(Calories) FROM VegetablesAndFruits", connection))
            {
                return (int)command.ExecuteScalar();
            }
        }
       
        static int GetVegetableCount(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = 'Vegetable'", connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

   
        static int GetFruitCount(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM VegetablesAndFruits WHERE Type = 'Fruit'", connection))
            {
                return (int)command.ExecuteScalar();
            }
        }

        static int GetCountByColor(SqlConnection connection, string color)
        {
            using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM VegetablesAndFruits WHERE Color = @Color", connection))
            {
                command.Parameters.AddWithValue("@Color", color);
                return (int)command.ExecuteScalar();
            }
        }

        static void DisplayCountByColor(SqlConnection connection)
        {
            using (SqlCommand command = new SqlCommand("SELECT Color, COUNT(*) AS Count FROM VegetablesAndFruits GROUP BY Color", connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Color"]}: {reader["Count"]}");
                }
            }
        }

        
        static void DisplayByCaloriesBelow(SqlConnection connection, int maxCalories)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM VegetablesAndFruits WHERE Calories < @MaxCalories", connection))
            {
                command.Parameters.AddWithValue("@MaxCalories", maxCalories);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Color"]}, {reader["Calories"]} kcal");
                    }
                }
            }
        }

       
        static void DisplayByCaloriesAbove(SqlConnection connection, int minCalories)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM VegetablesAndFruits WHERE Calories > @MinCalories", connection))
            {
                command.Parameters.AddWithValue("@MinCalories", minCalories);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Color"]}, {reader["Calories"]} kcal");
                    }
                }
            }
        }

        static void DisplayByCaloriesInRange(SqlConnection connection, int minCalories, int maxCalories)
        {
            using (SqlCommand command = new SqlCommand("SELECT * FROM VegetablesAndFruits WHERE Calories BETWEEN @MinCalories AND @MaxCalories", connection))
            {
                command.Parameters.AddWithValue("@MinCalories", minCalories);
                command.Parameters.AddWithValue("@MaxCalories", maxCalories);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Color"]}, {reader["Calories"]} kcal");
                    }
                }
            }
        }

      
        static void DisplayByColors(SqlConnection connection, params string[] colors)
        {
            string colorList = string.Join(",", colors.Select(c => $"'{c}'"));
            string query = $"SELECT * FROM VegetablesAndFruits WHERE Color IN ({colorList})";

            using (SqlCommand command = new SqlCommand(query, connection))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["Name"]}, {reader["Type"]}, {reader["Color"]}, {reader["Calories"]} kcal");
                }
            }
        }
    }
}
