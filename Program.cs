
namespace VehicleEfficiencyCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double distance = GetFullInput("Enter travel distance (in KM):");
            double fuelEfficiency = GetFullInput("Enter car fuel efficiency (KM/Liter):");
            double fuelPrice = GetFullInput("Enter current fuel price ($/Liter):");
            double busFare = GetFullInput("Enter bus fare for one trip ($):");

            double userPriority = GetFullInput("Enter your priority (1 = speed, 2 = eco-friendly, 3 = balanced):");
        }

        static double GetFullInput(string? prompt) // making it quicker to do amounts of data collection
        {
            string inputStr = GetValidInput(prompt);
            return ValidateInteger(inputStr);
        }

        static string GetValidInput(string? prompt) // getting the user input in a string
        {
            Console.WriteLine(prompt); // printing the prompt before asking input
            string? userInput = Console.ReadLine(); // getting input
            if (string.IsNullOrEmpty(userInput) || userInput.ToLower() == "exit") // if the user wants to exit or no input
                return "User Exited."; // closing app
            else
                return userInput; // returning valid output
        }

        static double ValidateInteger(string input) // making sure the string can be turned into an int above 0
        {
            if (double.TryParse(input, out double output)) // trying to parse the string to an int
            {
                if (output > 0)
                    return output;
                else
                    throw new ArgumentException("Value must be larger than 0."); // if the int is 0 or below
            }
            throw new ArgumentException("Please input a valid integer."); // if some other error happens
        }
    }
}