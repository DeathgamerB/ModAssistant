using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using static ModAssistant.Http;

namespace ModAssistant
{
    public class Utils
    {
        public static bool IsAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
        public static string ExePath = Process.GetCurrentProcess().MainModule.FileName;

        public class Constants
        {
            public const string BeatSaberAPPID = "620980";

            public const string BeatModsAPIUrl_beatmods = "https://beatmods.com/api/v1/";
            public const string TeknikAPIUrl_beatmods = "https://api.teknik.io/v1/";
            public const string BeatModsURL_beatmods = "https://beatmods.com";
            public const string BeatModsVersions_beatmods = "https://versions.beatmods.com/versions.json";
            public const string BeatModsAlias_beatmods = "https://alias.beatmods.com/aliases.json";
            public const string WeebCDNAPIURL_beatmods = "https://pat.assistant.moe/api/v1.0/";
            public const string BeatModsTranslation_beatmods = "https://wgzeyu.github.io/BeatSaberModListTranslationRepo/zh-Hans.json";

            public const string BeatModsAPIUrl_wgzeyu = "https://beatmods.wgzeyu.com/api/v1/";
            public const string TeknikAPIUrl_wgzeyu = "https://beatmods.wgzeyu.com/teknik/v1/";
            public const string BeatModsURL_wgzeyu = "https://beatmods.wgzeyu.com";
            public const string BeatModsVersions_wgzeyu = "https://beatmods.wgzeyu.com/bmversions/versions.json";
            public const string BeatModsAlias_wgzeyu = "https://beatmods.wgzeyu.com/alias/aliases.json";
            public const string WeebCDNAPIURL_wgzeyu = "https://beatmods.wgzeyu.com/assistant/api/v1.0/";
            public const string BeatModsTranslation_wgzeyu = "https://beatmods.wgzeyu.com/github/BeatSaberModListTranslationRepo/zh-Hans.json";

            public const string BeatModsAPIUrl_bmtop = "https://api.beatmods.top/api/v1/";
            public const string TeknikAPIUrl_bmtop = "https://teknikapi.beatmods.top/v1/";
            public const string BeatModsURL_bmtop = "https://beatmods.beatmods.top";
            public const string BeatModsVersions_bmtop = "https://versions-beatmods.beatmods.top/versions.json";
            public const string BeatModsAlias_bmtop = "https://alias-beatmods.beatmods.top/aliases.json";
            public const string WeebCDNAPIURL_bmtop = "https://pat-assistant-moe.beatmods.top/api/v1.0/";

            public const string BeatSaverURLPrefix_default = "https://api.beatsaver.com";
            public const string BeatSaverURLPrefix_wgzeyu = "https://beatsaver.wgzeyu.vip/api";
            public const string BeatSaverURLPrefix_beatsaberchina = "https://beatsaver.beatsaberchina.com/api";

            public const string BeatSaverCDNURLPrefix_default = "https://cdn.beatsaver.com";
            public const string BeatSaverCDNURLPrefix_wgzeyu = "https://beatsaver.wgzeyu.vip/sea";
            public const string BeatSaverCDNURLPrefix_beatsaberchina = "https://beatsaver-cdn.beatsaberchina.com";

            public const string ModelSaberURLPrefix_default = "https://modelsaber.com/files/";
            public const string ModelSaberURLPrefix_wgzeyu = "https://modelsaber.wgzeyu.vip/files/";
            public const string ModelSaberURLPrefix_beatsaberchina = "https://modelsaber.beatsaberchina.com/files/";
            
            public static string BeatModsAPIUrl;
            public static string TeknikAPIUrl;
            public static string BeatModsURL;
            public static string BeatModsVersions;
            public static string BeatModsAlias;
            public static string WeebCDNAPIURL;
            public static string BeatModsTranslation;

            public const string BeatModsModsOptions = "mod?status=approved";
            public const string MD5Spacer = "                                 ";
            public static readonly char[] IllegalCharacters = new char[]
            {
                '<', '>', ':', '/', '\\', '|', '?', '*', '"',
                '\u0000', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\u0007',
                '\u0008', '\u0009', '\u000a', '\u000b', '\u000c', '\u000d', '\u000e', '\u000d',
                '\u000f', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016',
                '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d', '\u001f',
            };

            public static void UpdateDownloadNode() {
                if (ModAssistant.Properties.Settings.Default.DownloadServer == Server.WGZeyu)
                {
                    Utils.Constants.BeatModsAPIUrl = Utils.Constants.BeatModsAPIUrl_wgzeyu;
                    Utils.Constants.TeknikAPIUrl = Utils.Constants.TeknikAPIUrl_wgzeyu;
                    Utils.Constants.BeatModsURL = Utils.Constants.BeatModsURL_wgzeyu;
                    Utils.Constants.BeatModsVersions = Utils.Constants.BeatModsVersions_wgzeyu;
                    Utils.Constants.BeatModsAlias = Utils.Constants.BeatModsAlias_wgzeyu;
                    Utils.Constants.WeebCDNAPIURL = Utils.Constants.WeebCDNAPIURL_wgzeyu;
                    Utils.Constants.BeatModsTranslation = Utils.Constants.BeatModsTranslation_wgzeyu;
                }
                else if (ModAssistant.Properties.Settings.Default.DownloadServer == Server.BeatModsTop)
                {
                    Utils.Constants.BeatModsAPIUrl = Utils.Constants.BeatModsAPIUrl_bmtop;
                    Utils.Constants.TeknikAPIUrl = Utils.Constants.TeknikAPIUrl_bmtop;
                    Utils.Constants.BeatModsURL = Utils.Constants.BeatModsURL_bmtop;
                    Utils.Constants.BeatModsVersions = Utils.Constants.BeatModsVersions_bmtop;
                    Utils.Constants.BeatModsAlias = Utils.Constants.BeatModsAlias_bmtop;
                    Utils.Constants.WeebCDNAPIURL = Utils.Constants.WeebCDNAPIURL_bmtop;
                    Utils.Constants.BeatModsTranslation = Utils.Constants.BeatModsTranslation_beatmods;
                }
                else {
                    Utils.Constants.BeatModsAPIUrl = Utils.Constants.BeatModsAPIUrl_beatmods;
                    Utils.Constants.TeknikAPIUrl = Utils.Constants.TeknikAPIUrl_beatmods;
                    Utils.Constants.BeatModsURL = Utils.Constants.BeatModsURL_beatmods;
                    Utils.Constants.BeatModsVersions = Utils.Constants.BeatModsVersions_beatmods;
                    Utils.Constants.BeatModsAlias = Utils.Constants.BeatModsAlias_beatmods;
                    Utils.Constants.WeebCDNAPIURL = Utils.Constants.WeebCDNAPIURL_beatmods;
                    Utils.Constants.BeatModsTranslation = Utils.Constants.BeatModsTranslation_beatmods;
                }
            }
        }

        public class TeknikPasteResponse
        {
            public Result result;
            public class Result
            {
                public string id;
                public string url;
                public string title;
                public string syntax;
                public DateTime? expiration;
                public string password;
            }
        }

        public class WeebCDNRandomResponse
        {
            public int index;
            public string url;
            public string ext;
        }

        public static void SendNotify(string message, string title = null)
        {
            string defaultTitle = (string)Application.Current.FindResource("Utils:NotificationTitle");

            var notification = new System.Windows.Forms.NotifyIcon()
            {
                Visible = true,
                // resource icon from pack
                Icon = System.Drawing.Icon.ExtractAssociatedIcon(ExePath),
                BalloonTipTitle = title ?? defaultTitle,
                BalloonTipText = message
            };

            notification.ShowBalloonTip(5000);

           // notification.Dispose(); This seems to cause Microsoft.Explorer.Notification.{random guid}
        }

        public static void StartAsAdmin(string Arguments, bool Close = false)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = Process.GetCurrentProcess().MainModule.FileName;
                process.StartInfo.Arguments = Arguments;
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.Verb = "runas";

                try
                {
                    process.Start();

                    if (!Close)
                    {
                        process.WaitForExit();
                    }
                }
                catch
                {
                    MessageBox.Show((string)Application.Current.FindResource("Utils:RunAsAdmin"));
                }

                if (Close) Application.Current.Shutdown();
            }
        }

        public static string CalculateMD5(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    var hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        public static string CalculateMD5FromStream(Stream stream)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        public static string GetInstallDir()
        {
            string InstallDir = Properties.Settings.Default.InstallFolder;

            if (!string.IsNullOrEmpty(InstallDir)
                && Directory.Exists(InstallDir)
                && Directory.Exists(Path.Combine(InstallDir, "Beat Saber_Data", "Plugins"))
                && File.Exists(Path.Combine(InstallDir, "Beat Saber.exe")))
            {
                return InstallDir;
            }

            try
            {
                InstallDir = GetSteamDir();
            }
            catch { }
            if (!string.IsNullOrEmpty(InstallDir))
            {
                return InstallDir;
            }

            try
            {
                InstallDir = GetOculusDir();
            }
            catch { }
            if (!string.IsNullOrEmpty(InstallDir))
            {
                return InstallDir;
            }

            MessageBox.Show((string)Application.Current.FindResource("Utils:NoInstallFolder"));

            InstallDir = GetManualDir();
            if (!string.IsNullOrEmpty(InstallDir))
            {
                return InstallDir;
            }

            return null;
        }

        public static string SetDir(string directory, string store)
        {
            App.BeatSaberInstallDirectory = directory;
            App.BeatSaberInstallType = store;
            Pages.Options.Instance.InstallDirectory = directory;
            Pages.Options.Instance.InstallType = store;
            Properties.Settings.Default.InstallFolder = directory;
            Properties.Settings.Default.StoreType = store;
            Properties.Settings.Default.Save();
            Constants.UpdateDownloadNode();
            return directory;
        }

        public static string GetSteamDir()
        {

            string SteamInstall = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)?.OpenSubKey("SOFTWARE")?.OpenSubKey("WOW6432Node")?.OpenSubKey("Valve")?.OpenSubKey("Steam")?.GetValue("InstallPath").ToString();
            if (string.IsNullOrEmpty(SteamInstall))
            {
                SteamInstall = Registry.LocalMachine.OpenSubKey("SOFTWARE")?.OpenSubKey("WOW6432Node")?.OpenSubKey("Valve")?.OpenSubKey("Steam")?.GetValue("InstallPath").ToString();
            }

            if (string.IsNullOrEmpty(SteamInstall)) return null;

            string vdf = Path.Combine(SteamInstall, @"steamapps\libraryfolders.vdf");
            if (!File.Exists(@vdf)) return null;

            Regex regex = new Regex("\\s\"\\d\"\\s+\"(.+)\"");
            Regex regex_new = new Regex("\\s\"(?:\\d|path)\"\\s+\"(.+)\"");
            List<string> SteamPaths = new List<string>
            {
                Path.Combine(SteamInstall, @"steamapps")
            };

            using (StreamReader reader = new StreamReader(@vdf))
            {
	            string line;
	            while ((line = reader.ReadLine()) != null)
	            {
		            Match match_old = regex.Match(line);
		            if (match_old.Success)
		            {
			            SteamPaths.Add(Path.Combine(match_old.Groups[1].Value.Replace(@"\\", @"\"), @"steamapps"));
		            } else
		            {
			            Match match_new = regex_new.Match(line);
			            if (match_new.Success) {
				            SteamPaths.Add(Path.Combine(match_new.Groups[1].Value.Replace(@"\\", @"\"), @"steamapps"));
			            }
		            }  
	            }
            }

            regex = new Regex("\\s\"installdir\"\\s+\"(.+)\"");
            foreach (string path in SteamPaths)
            {
                if (File.Exists(Path.Combine(@path, @"appmanifest_" + Constants.BeatSaberAPPID + ".acf")))
                {
                    using (StreamReader reader = new StreamReader(Path.Combine(@path, @"appmanifest_" + Constants.BeatSaberAPPID + ".acf")))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Match match = regex.Match(line);
                            if (match.Success)
                            {
                                if (File.Exists(Path.Combine(@path, @"common", match.Groups[1].Value, "Beat Saber.exe")))
                                {
                                    return SetDir(Path.Combine(@path, @"common", match.Groups[1].Value), "Steam");
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }

        public static async Task<string> GetVersion()
        {
            string result = string.Empty;

            var versions = await GetAllPossibleVersions();

            string filename = Path.Combine(App.BeatSaberInstallDirectory, "Beat Saber_Data", "globalgamemanagers");
            using (var stream = File.OpenRead(filename))
            using (var reader = new StreamReader(stream, Encoding.UTF8))
            {
                var line = reader.ReadLine();

                while (line != null)
                {
                    foreach (var version in versions)
                    {
                        if (line.Contains(version))
                        {
                            result = version;
                            break;
                        }
                    }
                    if (!string.IsNullOrEmpty(result)) break;
                    line = reader.ReadLine();
                }

                ////There is one version ending in "p1" on BeatMods
                var filteredVersionMatch = Regex.Match(result, @"[\d]+.[\d]+.[\d]+(p1)?");
                return filteredVersionMatch.Success ? filteredVersionMatch.Value : result;
            }
        }

        // TODO: should cache this
        public static async Task<List<string>> GetVersionsList()
        {
            var resp = await HttpClient.GetAsync(Constants.BeatModsVersions);
            var body = await resp.Content.ReadAsStringAsync();
            List<string> versions = JsonSerializer.Deserialize<string[]>(body).ToList();

            return versions;
        }

        // TODO: should cache this
        public static async Task<Dictionary<string, string[]>> GetAliasDictionary()
        {
            var resp = await HttpClient.GetAsync(Constants.BeatModsAlias);
            var body = await resp.Content.ReadAsStringAsync();
            var aliases = JsonSerializer.Deserialize<Dictionary<string, string[]>>(body);

            return aliases;
        }

        public static async Task<List<string>> GetAllPossibleVersions()
        {
            var versions = await GetVersionsList();
            var aliases = await GetAliasDictionary();

            return versions.Concat(aliases.SelectMany(x => x.Value)).ToList();
        }

        public static string GetOculusDir()
        {
            string OculusInstall = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)?.OpenSubKey("SOFTWARE")?.OpenSubKey("Wow6432Node")?.OpenSubKey("Oculus VR, LLC")?.OpenSubKey("Oculus")?.OpenSubKey("Config")?.GetValue("InitialAppLibrary").ToString();
            if (string.IsNullOrEmpty(OculusInstall)) return null;

            if (!string.IsNullOrEmpty(OculusInstall))
            {
                if (File.Exists(Path.Combine(OculusInstall, "Software", "hyperbolic-magnetism-beat-saber", "Beat Saber.exe")))
                {
                    return SetDir(Path.Combine(OculusInstall, "Software", "hyperbolic-magnetism-beat-saber"), "Oculus");
                }
            }

            // Yoinked this code from Umbranox's Mod Manager. Lot's of thanks and love for Umbra <3
            using (RegistryKey librariesKey = Registry.CurrentUser.OpenSubKey("Software")?.OpenSubKey("Oculus VR, LLC")?.OpenSubKey("Oculus")?.OpenSubKey("Libraries"))
            {
                // Oculus libraries uses GUID volume paths like this "\\?\Volume{0fea75bf-8ad6-457c-9c24-cbe2396f1096}\Games\Oculus Apps", we need to transform these to "D:\Game"\Oculus Apps"
                WqlObjectQuery wqlQuery = new WqlObjectQuery("SELECT * FROM Win32_Volume");
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(wqlQuery))
                {
                    Dictionary<string, string> guidLetterVolumes = new Dictionary<string, string>();

                    foreach (ManagementBaseObject disk in searcher.Get())
                    {
                        var diskId = ((string)disk.GetPropertyValue("DeviceID")).Substring(11, 36);
                        var diskLetter = ((string)disk.GetPropertyValue("DriveLetter")) + @"\";

                        if (!string.IsNullOrWhiteSpace(diskLetter))
                        {
                            guidLetterVolumes.Add(diskId, diskLetter);
                        }
                    }

                    // Search among the library folders
                    foreach (string libraryKeyName in librariesKey.GetSubKeyNames())
                    {
                        using (RegistryKey libraryKey = librariesKey.OpenSubKey(libraryKeyName))
                        {
                            string libraryPath = (string)libraryKey.GetValue("Path");
                            // Yoinked this code from Megalon's fix. <3
                            string GUIDLetter = guidLetterVolumes.FirstOrDefault(x => libraryPath.Contains(x.Key)).Value;
                            if (!string.IsNullOrEmpty(GUIDLetter))
                            {
                                string finalPath = Path.Combine(GUIDLetter, libraryPath.Substring(49), @"Software\hyperbolic-magnetism-beat-saber");
                                if (File.Exists(Path.Combine(finalPath, "Beat Saber.exe")))
                                {
                                    return SetDir(finalPath, "Oculus");
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }

        public static string GetManualDir()
        {
            var dialog = new SaveFileDialog()
            {
                Title = (string)Application.Current.FindResource("Utils:InstallDir:DialogTitle"),
                Filter = "Directory|*.this.directory",
                FileName = "select"
            };

            var old_store = Properties.Settings.Default.StoreType;
            if (dialog.ShowDialog() == true)
            {
                string path = dialog.FileName;
                path = path.Replace("\\select.this.directory", "");
                path = path.Replace(".this.directory", "");
                path = path.Replace("\\select.directory", "");
                if (File.Exists(Path.Combine(path, "Beat Saber.exe")))
                {
                    string store;
                    if (File.Exists(Path.Combine(path, "Beat Saber_Data", "Plugins", "steam_api64.dll"))
                       || File.Exists(Path.Combine(path, "Beat Saber_Data", "Plugins", "x86_64", "steam_api64.dll")))
                    {
                        store = "Steam";
                    }
                    else
                    {
                        store = "Oculus";
                    }
                    SetDir(path, store);
                }
            }
            if (!old_store.Equals(Properties.Settings.Default.StoreType)) {
                Process.Start(Utils.ExePath, App.Arguments);
                Application.Current.Dispatcher.Invoke(() => { Application.Current.Shutdown(); });
            }
            return null;
        }

        public static void CheckDirValid(string BeatSaberInstallDirectory) {
            for (int i = 0; i < BeatSaberInstallDirectory.Length; ++i)
            {
                if (((int)BeatSaberInstallDirectory[i]) > 127)
                {
                    MessageBox.Show((string)Application.Current.FindResource("App:InstallDirInvalid"));
                    break;
                }
            }
        }

        public static string GetManualFile(string filter = "", string title = "Open File")
        {
            var dialog = new OpenFileDialog()
            {
                Title = title,
                Filter = filter,
                Multiselect = false,
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }
            return null;
        }
        
        public static byte[] StreamToArray(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static void OpenFolder(string location)
        {
            if (!location.EndsWith(Path.DirectorySeparatorChar.ToString())) location += Path.DirectorySeparatorChar;
            if (Directory.Exists(location))
            {
                try
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = location,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                    return;
                }
                catch { }
            }
            MessageBox.Show($"{string.Format((string)Application.Current.FindResource("Utils:CannotOpenFolder"), location)}.");
        }

        public static void Log(string message, string severity = "LOG")
        {
            string path = Path.GetDirectoryName(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
            string logFile = $"{path}{Path.DirectorySeparatorChar}log.log";
            File.AppendAllText(logFile, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffffff")}][{severity.ToUpperInvariant()}] {message}\n");
        }

        public static async Task<string> Download(string link, string folder, string output, bool preferContentDisposition = false, bool modelsaber = false, bool throughProxy = false)
        {
            var resp = await HttpClient.GetAsync(link);
            var cdFilename = resp.Content.Headers.ContentDisposition?.FileName?.Trim('"');
            // Prevent path traversal
            if (cdFilename?.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
            {
                cdFilename = null;
            }

            var filename = WebUtility.UrlDecode(Path.Combine(
                folder,
                (preferContentDisposition ? cdFilename : null) ?? output
            ));

            using (var stream = await resp.Content.ReadAsStreamAsync())
            using (var fs = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                await stream.CopyToAsync(fs);
                if (modelsaber && throughProxy) {
                    ModAssistant.ZeyuCount.downloadModelSaberSingle();
                }
            }

            return filename;
        }

        private delegate void ShowMessageBoxDelegate(string Message, string Caption);

        private static void ShowMessageBox(string Message, string Caption)
        {
            MessageBox.Show(Message, Caption);
        }

        public static void ShowMessageBoxAsync(string Message, string Caption)
        {
            ShowMessageBoxDelegate caller = new ShowMessageBoxDelegate(ShowMessageBox);
            caller.BeginInvoke(Message, Caption, null, null);
        }

        public static void ShowMessageBoxAsync(string Message)
        {
            ShowMessageBoxDelegate caller = new ShowMessageBoxDelegate(ShowMessageBox);
            caller.BeginInvoke(Message, null, null, null);
        }

        /// <summary>
        /// Attempts to write the specified string to the <see cref="System.Windows.Clipboard"/>.
        /// </summary>
        /// <param name="text">The string to be written</param>
        public static void SetClipboard(string text)
        {
            bool success = false;
            try
            {
                Clipboard.SetText(text);
                success = true;
            }
            catch (Exception)
            {
                // Swallow exceptions relating to writing data to clipboard.
            }

            // This could be placed in the try/catch block but we don't
            // want to suppress exceptions for non-clipboard operations
            if (success)
            {
                Utils.SendNotify($"Copied text to clipboard");
            }
        }

        public static void UpdateCountIndicator()
        {
            string count = (string)Application.Current.FindResource("MainWindow:AssetsServerLimitLabelUnlimited");
            if (ModAssistant.Properties.Settings.Default.AssetsDownloadServer == "国内源@WGzeyu")
            {
                count = ZeyuCount.getCount().ToString();
            }

            if (ModAssistant.MainWindow.Instance != null) {
                ModAssistant.MainWindow.Instance.AssetsServerLimitLabel.Text = $"{string.Format((string)Application.Current.FindResource("MainWindow:AssetsServerLimitLabel"), count)}";
            }
        }
    }
}
