using System;

// 1. Simulated Data Repository Layer
// This simulates the database interaction where the value is stored as an integer.
public class DataRepository
{
    // The configuration key for the connection timeout
    private const string ConnectionTimeoutKey = "ConnectionTimeoutSeconds";
    
    // Simulate fetching a configuration value from the database
    public int? GetIntConfigurationValue(string key)
    {
        // In a real application, this would involve a database query.
        // We simulate that the 'ConnectionTimeoutSeconds' is stored as the integer '30'.
        if (key == ConnectionTimeoutKey)
        {
            Console.WriteLine($"[Repository] Retrieved integer value for '{key}': 30");
            return 30; // Stored as an integer in the DB
        }

        Console.WriteLine($"[Repository] Configuration key '{key}' not found.");
        return null;
    }
}

// 2. Configuration Service Layer
// This service is responsible for providing the configuration values 
// to the rest of the application, ensuring they are the expected type (string).
public class ConfigurationService
{
    private readonly DataRepository _repository;

    public ConfigurationService(DataRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Reads an integer value from the repository and converts it to a string.
    /// </summary>
    /// <param name="key">The configuration key to look up.</param>
    /// <returns>The configuration value as a string, or a default value if not found.</returns>
    public string GetConnectionTimeoutSetting()
    {
        const string key = "ConnectionTimeoutSeconds";
        const string defaultValue = "60"; // Default to 60 seconds as a string

        try
        {
            // Fetch the value, which returns an int? (nullable integer)
            int? intValue = _repository.GetIntConfigurationValue(key);

            if (intValue.HasValue)
            {
                // Core action: Convert the retrieved integer to a string.
                string stringValue = intValue.Value.ToString();
                Console.WriteLine($"[Service] Successfully converted integer '{intValue.Value}' to string '{stringValue}'.");
                return stringValue;
            }
            else
            {
                Console.WriteLine($"[Service] Value for '{key}' was null. Returning default string value: {defaultValue}.");
                return defaultValue;
            }
        }
        catch (Exception ex)
        {
            // Log the error and fall back to a safe default
            Console.WriteLine($"[Service Error] Failed to process configuration value for '{key}'. Error: {ex.Message}");
            return defaultValue;
        }
    }
}

// 3. Application Entry Point (Demonstration)
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Starting Configuration Lookup Demonstration ---");
        
        // Setup dependencies
        var repository = new DataRepository();
        var configService = new ConfigurationService(repository);

        // Get the configuration value
        string timeoutValue = configService.GetConnectionTimeoutSetting();

        // The consuming application code receives a string, as expected.
        Console.WriteLine($"\n[Application] The final expected connection timeout (as string) is: {timeoutValue}");
        
        // Example of how the application might use the string value:
        // (Perhaps an external library expects configuration input only as strings)
        Console.WriteLine($"[Application] Configuration is ready to be consumed by external component.");
        
        // To show it's a string, we can check its type:
        Console.WriteLine($"[Application] Type of returned value: {timeoutValue.GetType().Name}");
        
        Console.WriteLine("\n--- Demonstration Complete ---");
    }
}
