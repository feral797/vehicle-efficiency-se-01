
namespace VehicleEfficiencyCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            string recommendation = ""; // Car | Bus | Bike

            double distance = GetFullInput("Enter travel distance (in KM):");
            double fuelEfficiency = GetFullInput("Enter car fuel efficiency (KM/Liter):");
            double fuelPrice = GetFullInput("Enter current fuel price ($/Liter):");
            double busFare = GetFullInput("Enter bus fare for one trip ($):");

            double userPriority = GetFullInput("Enter your priority (1 = speed, 2 = eco-friendly, 3 = balanced):");

            // sorting calculations
            // car calcs
            double fuelUsed = distance / fuelEfficiency;
            double carCost = fuelUsed * fuelPrice;
            double carCO2 = fuelUsed * 2.31;
            double carTime = (distance / 40) * 60;
            double carTimeWithParking = carTime + 5;

            // bus calcs
            double busCost = busFare;
            double busCO2 = distance * 0.08;
            double busTime = (distance / 25) * 60 + 10;

            // bike calcs
            double bicycleCost = 0;
            double bicycleCO2 = 0;
            double bicycleTime = (distance / 15) * 60;

            // displaying data
            Console.WriteLine("=============================================");
            Console.WriteLine("Mode:   | Time (Min) | Cost ($) | CO2 (kg)");
            Console.WriteLine("Car :   | " + carTimeWithParking + " | $" + carCost + " | " + carCO2 + "kg"); // car data
            Console.WriteLine("Bus :   | " + busTime + " | $" + busCost + " | " + busCO2 + "kg"); // bus data
            Console.WriteLine("Bike:   | " + bicycleTime + " | $" + bicycleCost + " | " + bicycleCO2 + "kg"); // bike data
            Console.WriteLine("=============================================");

            // all the maths stuff 😋
            if (userPriority == 1) // speed
            {
                if (carTimeWithParking <= busTime && carTimeWithParking <= bicycleTime)
                {
                    recommendation = "Car";
                }
                else if (busTime <= carTimeWithParking && busTime <= bicycleTime)
                {
                    recommendation = "Bus";
                }
                else
                {
                    recommendation = "Bicycle";
                }
            }
            else if (userPriority == 2) // eco friendly
            {
                if (bicycleCO2 <= busCO2 && bicycleCO2 <= carCO2)
                {
                    recommendation = "Bicycle";
                }
                else if (busCO2 <= bicycleCO2 && busCO2 <= carCO2)
                {
                    recommendation = "Bus";
                }
                else
                {
                    recommendation = "Car";
                }
            }
            else if (userPriority == 3) // balanced
            {
                double speedScore = (carTimeWithParking / 60) + (busTime / 60) + (bicycleTime / 60);
                double ecoScore = carCO2 + busCO2 + bicycleCO2;
                double carBalanced = (carTimeWithParking / 60) + speedScore + (carCO2 / ecoScore);
                double busBalanced = (busTime / 60) + speedScore + (busCO2 + ecoScore);
                double bicycleBalanced = (bicycleTime / 60) + speedScore + (bicycleCO2 + ecoScore);

                if (carBalanced <= busBalanced && carBalanced <= bicycleBalanced)
                {
                    recommendation = "Car";
                }
                else if (busBalanced <= carBalanced && busBalanced <= bicycleBalanced)
                {
                    recommendation = "Bus";
                }
                else
                {
                    recommendation = "Bicycle";
                }
            }

            Console.WriteLine("Recommendation: " + recommendation);
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