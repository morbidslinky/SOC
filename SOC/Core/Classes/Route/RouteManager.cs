using SOC.Classes.GzsTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using static FoxLib.Tpp.RouteSet;

namespace SOC.Core.Classes.Route
{
    public static class RouteManager
    {
        public static string routeNameDictionaryFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets\\ToolAssets\\route_name_dictionary.txt");
        public static string routeAssetsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SOCassets//RouteAssets");

        public static Dictionary<uint, string> RouteNameHashDictionary = new Dictionary<uint, string>();

        public static string GetRouteFileName(string frtName)
        {
            return Path.Combine(routeAssetsPath, frtName) + ".frt";
        }

        public static List<string> GetRouteFileNameList()
        {
            List<string> routeNameList = new List<string>();

            foreach (string filename in Directory.GetFiles(routeAssetsPath, "*.frt"))
            {
                routeNameList.Add(Path.GetFileNameWithoutExtension(filename));
            }

            return routeNameList;
        }

        public static List<string> GetRouteNames(string frtName)
        {
            string frtPath = GetRouteFileName(frtName);
            uint[] frtUintNames = GetUintNames(frtPath);

            if (File.Exists(routeNameDictionaryFile))
                RouteNameHashDictionary = Hashing.MakeHashLookupTableFromFile(routeNameDictionaryFile);
            else
                MessageBox.Show("Route Dictionary Not Found. \n\n" + routeNameDictionaryFile, "Dictionary Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            List<string> routeStringNames = new List<string>();

            foreach (uint routeUintName in frtUintNames)
            {
                string routeStringName = "";
                if (RouteNameHashDictionary.TryGetValue(routeUintName, out routeStringName))
                    routeStringNames.Add(routeStringName);
                else
                    routeStringNames.Add(routeUintName.ToString());
            }

            routeStringNames.Sort();
            return routeStringNames;
        }

        private static uint[] GetUintNames(string frtPath)
        {
            RouteSet frtRoutes;
            uint[] routeNames = new uint[0];

            if (File.Exists(frtPath))
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                using (var reader = new BinaryReader(new FileStream(frtPath, FileMode.Open), getEncoding()))
                {
                    Action<int> skipBytes = numberOfBytes => SkipBytes(reader, numberOfBytes);
                    var readFunctions = new ReadFunctions(reader.ReadSingle, reader.ReadUInt16, reader.ReadUInt32, reader.ReadInt32, reader.ReadBytes, skipBytes);
                    frtRoutes = Read(readFunctions);
                }

                IEnumerable<uint> routes = from route in frtRoutes.Routes
                                           select route.Name;

                routeNames = routes.ToArray();
            }
            return routeNames;
        }

        private static void SkipBytes(BinaryReader reader, int numberOfBytes)
        {
            reader.BaseStream.Position += numberOfBytes;
        }
    }
}
