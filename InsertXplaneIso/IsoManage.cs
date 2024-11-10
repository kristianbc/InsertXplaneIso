using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using System.IO;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.Runtime.InteropServices;

namespace InsertXplaneIso
{
    public class IsoManage
    {
        public static void GetIsoPath()
        {
            ShowConsole();
            Console.WriteLine("Example: C:\\path\\to\\your\\file.iso");
            Console.Write("Specify Iso Path \n:");

            string isoPath = Console.ReadLine();

            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "isoPath.txt";
            string filePath = Path.Combine(appFolder, fileName);

            File.WriteAllText(filePath, isoPath);
            Console.WriteLine($"File created at: {filePath}");

            if (File.Exists(filePath))
            {
                MountIso();
            }
            else
            {
                throw new Exception("isoPath.txt does not exist!");
            }
        }

        public static bool IsIsoMounted(string isoPath)
        {
            string script = $"Get-DiskImage -ImagePath '{isoPath}' | Select-Object -ExpandProperty Attached";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "powershell",
                Arguments = $"-Command \"{script}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                process.WaitForExit();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                    return false;
                }
                else
                {
                    return output.Contains("True");
                }
            }
        }

        public static void MountIso()
        {
            HideConsole();
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "isoPath.txt";
            string filePath = Path.Combine(appFolder, fileName);

            try
            {
                string isoPath = File.ReadAllText(filePath);
                if (IsIsoMounted(isoPath))
                {
                    Environment.Exit(0);
                }

                try
                {
                    string command = $"Mount-DiskImage -ImagePath '{isoPath}'";

                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = $"-Command \"{command}\"",
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };

                    using (Process process = Process.Start(psi))
                    {
                        process.WaitForExit();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (string.IsNullOrEmpty(error))
                            Console.WriteLine("ISO mounted successfully.");
                        else
                            ShowConsole();
                            Console.WriteLine($"Error: {error}");
                    }
                }
                catch (Exception ex)
                {
                    ShowConsole();
                    Console.WriteLine($"Exception: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                ShowConsole();
                Console.WriteLine($"{ex.Message}");
            }

        }
        public static void Decide()
        {
            string appFolder = AppDomain.CurrentDomain.BaseDirectory;
            string fileName = "isoPath.txt";
            string filePath = Path.Combine(appFolder, fileName);

            if (File.Exists(filePath))
            {
                MountIso();
            }
            else
            {
                GetIsoPath();
            }
        }
        public static void AddToRegistryStartup()
        {
            string appName = Assembly.GetExecutingAssembly().GetName().Name;
            string exePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, appName + ".exe");

            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

                registryKey.SetValue(appName, exePath);
                registryKey.Close();
            }
            catch (Exception ex)
            {
                ShowConsole();
                Console.WriteLine($"Error adding to registry startup: {ex.Message}");
            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public static void HideConsole()
        {
            IntPtr handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
            {
                ShowWindow(handle, SW_HIDE);
            }
        }
        public static void ShowConsole()
        {
            IntPtr handle = GetConsoleWindow();
            if (handle != IntPtr.Zero)
            {
                ShowWindow(handle, SW_SHOW);
            }
        }
    }
}
