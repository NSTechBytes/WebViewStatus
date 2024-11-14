using System;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Rainmeter;

namespace WebViewCheckPlugin
{
    public class Measure
    {
        // Check if WebView2 Runtime is installed by looking at the registry.
        internal bool IsWebView2Installed()
        {
            return IsWebView2RuntimeInRegistry();
        }

        private bool IsWebView2RuntimeInRegistry()
        {
            // Registry paths for WebView2 Runtime detection
            string[] registryPaths = new string[]
            {
                @"SOFTWARE\WOW6432Node\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}",
                @"SOFTWARE\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}",
                @"HKEY_CURRENT_USER\Software\Microsoft\EdgeUpdate\Clients\{F3017226-FE2A-4295-8BDF-00C3A9A7E4C5}"
            };

            foreach (var registryPath in registryPaths)
            {
                try
                {
                    // Open registry for 64-bit or 32-bit paths based on the location
                    using (var key = Registry.LocalMachine.OpenSubKey(registryPath))
                    {
                        if (key != null)
                        {
                            string version = key.GetValue("pv") as string; // Check the 'pv' value for the version
                            if (!string.IsNullOrEmpty(version) && version != "0.0.0.0")
                            {
                                Console.WriteLine($"WebView2 Runtime found in registry at: {registryPath}, Version: {version}");
                                return true; // WebView2 Runtime is installed
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading registry at {registryPath}: {ex.Message}");
                }

                try
                {
                    using (var key = Registry.CurrentUser.OpenSubKey(registryPath))
                    {
                        if (key != null)
                        {
                            string version = key.GetValue("pv") as string; // Check the 'pv' value for the version
                            if (!string.IsNullOrEmpty(version) && version != "0.0.0.0")
                            {
                                Console.WriteLine($"WebView2 Runtime found in registry at: {registryPath}, Version: {version}");
                                return true; // WebView2 Runtime is installed
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reading registry at {registryPath}: {ex.Message}");
                }
            }

            return false; // No valid WebView2 Runtime entry in the registry
        }

        internal double Update()
        {
            // Return 1 if WebView2 Runtime is installed, 0 if not.
            return IsWebView2Installed() ? 1.0 : 0.0;
        }

        internal string GetString()
        {
            // Return a string indicating whether WebView2 Runtime is installed.
            return IsWebView2Installed() ? "1" : "0";
        }
    }

    public static class Plugin
    {
        static IntPtr StringBuffer = IntPtr.Zero;

        [DllExport]
        public static void Initialize(ref IntPtr data, IntPtr rm)
        {
            data = GCHandle.ToIntPtr(GCHandle.Alloc(new Measure()));
        }

        [DllExport]
        public static void Finalize(IntPtr data)
        {
            GCHandle.FromIntPtr(data).Free();

            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }
        }

        [DllExport]
        public static void Reload(IntPtr data, IntPtr rm, ref double maxValue)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            // Reload any necessary data here.
        }

        [DllExport]
        public static double Update(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            return measure.Update();
        }

        [DllExport]
        public static IntPtr GetString(IntPtr data)
        {
            Measure measure = (Measure)GCHandle.FromIntPtr(data).Target;
            if (StringBuffer != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(StringBuffer);
                StringBuffer = IntPtr.Zero;
            }

            string stringValue = measure.GetString();
            if (stringValue != null)
            {
                StringBuffer = Marshal.StringToHGlobalUni(stringValue);
            }

            return StringBuffer;
        }
    }
}
