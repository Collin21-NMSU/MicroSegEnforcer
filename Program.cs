using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MicroSegEnforcer
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rules.json");

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: {filePath} not found.");
                return;

            }

            // Read and Deserialize JSON policy
            string jsonString = File.ReadAllText(filePath);
            FirewallPolicy policy = JsonSerializer.Deserialize<FirewallPolicy>(jsonString);

            Console.WriteLine($"Loaded Policy: {policy.PolicyName} ({policy.Rules.Count} rules loaded)\n");

            Console.WriteLine("\n**** Interactive Packet Testing Mode ****");
            Console.WriteLine("Type 'exit' for Source IP to quit. \n");

            while (true)
            {
                Console.Write("Source IP: ");
                string srcIP = Console.ReadLine()?.Trim();
                if (string.Equals(srcIP, "exit", StringComparison.OrdinalIgnoreCase)) break;

                Console.Write("Destination IP: ");
                string dstIP = Console.ReadLine()?.Trim();

                Console.Write("Port: ");
                if (int.TryParse(Console.ReadLine(), out int port))
                {
                    Console.WriteLine();
                    FirewallEngine.EvaluateTraffic(policy, srcIP, dstIP, port);
                }
                else
                {
                    Console.WriteLine("Invalid Port Number. Only Integers Accepted");
                }
            }

            Console.WriteLine(new string('*', 40));

        }

        
    }
}