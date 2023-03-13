using System;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using Spectre.Console;
using static System.Net.Mime.MediaTypeNames;

namespace PhantomTerminal
{
    class Program
    {
        static Dictionary<string, Action<string[]>> commands = new Dictionary<string, Action<string[]>>();


        static void Main()
        {
            Console.Title = "Phantom Terminal";


            AnsiConsole.Write(new FigletText("Phantom Terminal").Centered().Color(24));


            var options = new[] { "Get Pc Info", "Phantom Command Line", "Code Injector", "Exit" };
            var prompt = new SelectionPrompt<string>()
                .Title("Select an option")
                .AddChoices(options);

            var selectedOption = AnsiConsole.Prompt(prompt);
            Console.WriteLine($"You selected: {selectedOption}");

            // Perform action based on selected option
            switch (selectedOption)
            {
                case "Get Pc Info":
                    GetPcInfo();
                    break;
                case "Phantom Command Line":
                    PhantomCMD();
                    break;
                case "Code Injector":
                    Console.WriteLine("Performing action for Option 3");
                    break;
                case "Exit":
                    UnregisterAllCommands();
                    Environment.Exit(1);
                    break;
                default:
                    Console.WriteLine("Invalid option selected");
                    break;
            }







        }

        static void GetPcInfo()
        {
            // Get computer name
            string computerName = Environment.MachineName;
            Console.WriteLine($"Computer Name: {computerName}");

            // Get current user name
            string userName = Environment.UserName;
            Console.WriteLine($"User Name: {userName}");

            // Get IP address
            ProcessStartInfo processStartInfo = new ProcessStartInfo("ipconfig.exe");
            processStartInfo.Arguments = "/all";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            Process process = Process.Start(processStartInfo);
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            int index = output.IndexOf("IPv4 Address");
            if (index != -1)
            {
                index = output.IndexOf(":", index) + 2;
                int endIndex = output.IndexOf(Environment.NewLine, index);
                string ipAddress = output.Substring(index, endIndex - index);
                Console.WriteLine($"IP Address: {ipAddress}");
            }
            else
            {
                Console.WriteLine("IP Address: Not Found");
            }

            // Get WiFi name
            processStartInfo = new ProcessStartInfo("netsh.exe");
            processStartInfo.Arguments = "wlan show interfaces";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            process = Process.Start(processStartInfo);
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            index = output.IndexOf("SSID");
            if (index != -1)
            {
                index = output.IndexOf(":", index) + 2;
                int endIndex = output.IndexOf(Environment.NewLine, index);
                string wifiName = output.Substring(index, endIndex - index);
                Console.WriteLine($"WiFi Name: {wifiName}");
            }
            else
            {
                Console.WriteLine("WiFi Name: Not Found");
            }

            // Get hardware info
            processStartInfo = new ProcessStartInfo("wmic.exe");
            processStartInfo.Arguments = "csproduct get name";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            process = Process.Start(processStartInfo);
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            index = output.IndexOf("Name");
            if (index != -1)
            {
                index = output.IndexOf(Environment.NewLine, index) + 2;
                int endIndex = output.Length - 2;
                string hardwareInfo = output.Substring(index, endIndex - index);
                Console.WriteLine($"Hardware Info: {hardwareInfo}");
            }
            else
            {
                Console.WriteLine("Hardware Info: Not Found");
            }

            // Get WiFi password
            processStartInfo = new ProcessStartInfo("netsh.exe");
            processStartInfo.Arguments = "wlan show profile name=* key=clear";
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardOutput = true;
            process = Process.Start(processStartInfo);
            output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine($"WiFi Password: {output}");

            Console.WriteLine("");
            Console.WriteLine("Press Enter To Continue...");

            Console.ReadLine();
            Console.Clear();
            Main();
        }      
        

        static void PhantomCMD()
        {
            RegisterCommands();

            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine().Trim();

                if (string.IsNullOrEmpty(input))
                    continue;

                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (commands.ContainsKey(parts[0].ToLower()))
                {
                    commands[parts[0].ToLower()](parts[1..]);
                }
                else
                {
                    Console.WriteLine($"Unknown command '{parts[0]}'.");
                }
            }
        }

        static void RegisterCommands()
        {
            commands.Add("echo", (args) =>
            {
                Console.WriteLine(string.Join(' ', args));
            });

            commands.Add("add", (args) =>
            {
                if (args.Length == 2 && int.TryParse(args[0], out int num1) && int.TryParse(args[1], out int num2))
                {
                    Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
                }
                else
                {
                    Console.WriteLine("Invalid arguments for 'add' command.");
                }
            });

            commands.Add("exit", (args) =>
            {
                UnregisterAllCommands();
                back();
            });

            commands.Add("cls", (args) =>
            {
                Console.Clear();
                AnsiConsole.Write(new FigletText("Phantom Terminal").Centered().Color(24));
                Console.WriteLine($"You selected: Phantom Command Line");
            });

            commands.Add("help", (args) =>
            {
                Console.WriteLine("");
                Console.WriteLine("echo     Allows you to print text");
                Console.WriteLine("add      Allows you to add numbers together. Example 1+1 = 2");
                Console.WriteLine("cls      Clears the Terminal");
                Console.WriteLine("exit     Goes back to the main menu");
                Console.WriteLine("");
            });

        }

        static void UnregisterAllCommands()
        {
            commands.Clear();
        }


        static void back()
        {
            Console.Clear();
            Main();
        }
    }
}