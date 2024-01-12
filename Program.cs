using scoop_manifest_generator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using System.Diagnostics;

string ver = "v1.0-devel";
Console.WriteLine($"Scoop manifest generator {ver}");
Console.WriteLine("Created by Filip Komárek");
Console.WriteLine("https://github.com/filip2cz/scoop-manifest-generator");

Config configLoader = new Config();

JObject config = configLoader.LoadConfig();

PlatformID platform = Environment.OSVersion.Platform;

try
{
    string manifestName = (string)config["manifestName"];
}
catch (Exception)
{
    Console.WriteLine("Failed to load manifest name, output will be output.json");
    string manifestName = "output.json";
}

if (config.ContainsKey("version"))
{
    string version = (string)config["version"];
}
else if (config.ContainsKey("versionCommand"))
{
	if (platform == PlatformID.Win32NT || platform == PlatformID.Win32S || platform == PlatformID.Win32Windows || platform == PlatformID.WinCE)
	{
        try
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                Arguments = $"-Command {(string)config["versionCommand"]}"
            };
            using (Process process = new Process { StartInfo = processStartInfo })
            {
                process.Start();

                // get output from powershell
                string version = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                // Wait until process is done
                process.WaitForExit();

                // Write output
                Console.WriteLine("Detected version:");
                Console.WriteLine(version);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Error from powershell:");
                    Console.WriteLine(error);
                }
            }
        }
        catch (Exception ex1)
        {/*
            try
            {
                Console.WriteLine("Powershell not found, trying cmd.exe");
                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    Arguments = $"/c {(string)config["versionCommand"]}"
                };
            }
            catch (Exception ex2)
            {*/
                //Console.WriteLine("Failed to run both powershell.exe and cmd.exe");
                Console.WriteLine("Failed to run powershell.exe");
                Console.WriteLine(ex1);
                //Console.WriteLine(ex2);
                throw;
            //}
        }
    }
}
else
{
    Console.WriteLine("Error: unable to get version");
}