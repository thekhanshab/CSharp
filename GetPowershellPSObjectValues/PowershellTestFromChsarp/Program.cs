using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Collections.ObjectModel;
using System.IO;
using Newtonsoft.Json;

namespace PowershellTestFromChsarp
{
    class Program
    {
        static void Main(string[] args)
        {
            var executionPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            RunspaceConfiguration runspaceConfiguration = RunspaceConfiguration.Create();
            Runspace runspace = RunspaceFactory.CreateRunspace(runspaceConfiguration);
            runspace.Open();

            RunspaceInvoke scriptInvoker = new RunspaceInvoke(runspace);

            Pipeline pipeline = runspace.CreatePipeline();

            Command myCommand = new Command(Path.Combine(executionPath, "SystemInfoV2.ps1").Substring(6));
            //CommandParameter testParam = new CommandParameter("key", "value");
            //myCommand.Parameters.Add(testParam);

            pipeline.Commands.Add(myCommand);

            PSObject  psObject = pipeline.Invoke()[0];

            //Serialize the PSObject
            var serializedPsObject = JsonConvert.SerializeObject(psObject.Properties.ToDictionary(k => k.Name, v => v.Value));

            //Deserialize the object to the C# class object
            var deseializedSystemInfo = JsonConvert.DeserializeObject<SystemInfo>(serializedPsObject);
            string websites = deseializedSystemInfo.WebSites;
            string appPools = deseializedSystemInfo.AppPools;
            string installedDotNet = deseializedSystemInfo.InstalledDotNet;
            string runtimeDotNet = deseializedSystemInfo.RunTimeDotNet;
            string machineConfig = deseializedSystemInfo.MachineConfig;

            Hardware hardware = deseializedSystemInfo.HardWare;
            string ram = hardware.RAM;
            string processor = hardware.Processor;
            string hardDisk = hardware.HardDisk;

            Console.WriteLine($"WebSites: {websites}");
            Console.WriteLine($"AppPools: {appPools}");
            Console.WriteLine($"InstalledDotNet: {installedDotNet}");
            Console.WriteLine($"RuntimeDotNet: {runtimeDotNet}");
            Console.WriteLine($"MachineConfig: {machineConfig}");
            Console.WriteLine($"RAM: {ram}");
            Console.WriteLine($"Processor: {processor}");
            Console.WriteLine($"HardDisk: {hardDisk}");

            Console.ReadLine();

        }
    }
}
