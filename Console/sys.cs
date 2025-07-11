using System;
using Spectre.Console;
using System.Management;
using AW_Lib;
class Program
{
    static void Main(string[] args)
    {
        var table = new Table();
        table.Border(TableBorder.None);

        // Fetching system info
        var osInfo = GetOSInfo();
        var processorInfo = GetProcessorInfo();
        var memoryInfo = GetMemoryInfo();

        // Adding columns and rows
        table.AddColumn(new TableColumn("Category").Centered());
        table.AddColumn(new TableColumn("Info").Centered());

        table.AddRow("[blue]Operating System[/]", osInfo);
        table.AddRow("[blue]Processor[/]", processorInfo);
        table.AddRow("[blue]Memory[/]", memoryInfo);

        AnsiConsole.Write(table);
    }

    static string GetOSInfo()
    {
        var osName = Environment.OSVersion.Platform.ToString();
        var osVersion = Environment.OSVersion.Version.ToString();
        return $"{osName} {osVersion}";
    }

    static string GetProcessorInfo()
    {
        var cpuInfo = "";
        var query = "SELECT Name, NumberOfCores, NumberOfLogicalProcessors FROM Win32_Processor";
        var searcher = new ManagementObjectSearcher(query);
        foreach (ManagementObject item in searcher.Get())
        {
            cpuInfo = $"{item["Name"]} ({item["NumberOfCores"]} Cores, {item["NumberOfLogicalProcessors"]} Threads)";
        }
        return cpuInfo;
    }

    static string GetMemoryInfo()
    {
        var ramInfo = "";
        var query = "SELECT Capacity FROM Win32_PhysicalMemory";
        var searcher = new ManagementObjectSearcher(query);
        ulong totalCapacity = 0;
        foreach (ManagementObject item in searcher.Get())
        {
            totalCapacity += (ulong)item["Capacity"];
        }
        ramInfo = $"{FormatBytes(totalCapacity)}";
        return ramInfo;
    }

    static string FormatBytes(ulong bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double formatted = bytes;
        int order = 0;

        while (formatted >= 1024 && order < sizes.Length - 1)
        {
            order++;
            formatted /= 1024;
        }

        return $"{formatted:0.##} {sizes[order]}";
    }
}
