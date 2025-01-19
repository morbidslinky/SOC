using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Classes.Common;
using SOC.QuestObjects.Common;
using System.IO;
using SOC.Classes.Assets;
using SOC.Core.Classes.Route;

namespace SOC.Classes.QuestBuild.Assets
{
    public class CommonAssetsBuilder
    {
        private List<string> fpkAssetPaths = new List<string>();
        private List<string> fpkdAssetPaths = new List<string>();

        public void AddFPKAssetPath(string fpkAssetPath)
        {
            if (!fpkAssetPaths.Contains(fpkAssetPath))
                fpkAssetPaths.Add(fpkAssetPath);
        }

        public void AddFPKDAssetPath(string fpkdAssetPath)
        {
            if (!fpkdAssetPaths.Contains(fpkdAssetPath))
                fpkdAssetPaths.Add(fpkdAssetPath);
        }

        public void Build(string buildDir, SetupDetails setupDetails, ObjectsDetails objectsDetails)
        {
            setupDetails.addToAssets(this);

            foreach(ObjectsDetail objectsDetail in objectsDetails.details)
            {
                objectsDetail.AddToAssets(this);
            }

            CopyAssets(buildDir, setupDetails.FpkName);
        }

        public void CopyAssets(string buildDir, string name)
        {
            var questFPKPath = Path.Combine(buildDir, name + "_fpk");
            copyPathsToDir(fpkAssetPaths, questFPKPath);

            var questFPKDPath = Path.Combine(buildDir, name + "_fpkd");
            copyPathsToDir(fpkdAssetPaths, questFPKDPath);
        }

        private void copyPathsToDir(List<string> sourcePaths, string destDir)
        {
            if (!Directory.Exists(destDir))
                Directory.CreateDirectory(destDir);

            foreach (string path in sourcePaths)
            {
                if (isDir(path))
                    CopyDirectory(path, destDir);
                else if (isFile(path))
                    CopyFile(path, Path.Combine(destDir, findAssetPath(path)));
            }
        }

        private bool isDir(string path)
        {
            return Directory.Exists(path);
        }

        private bool isFile(string path)
        {
            return File.Exists(path);
        }

        public static void CopyDirectory(string source, string dest)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(source);

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            foreach (FileInfo fileInfo in dirInfo.GetFiles())
                fileInfo.CopyTo(Path.Combine(dest, fileInfo.Name), true);

            foreach (DirectoryInfo subDirInfo in dirInfo.GetDirectories())
                CopyDirectory(subDirInfo.FullName, Path.Combine(dest, subDirInfo.Name));
        }

        public static void CopyFile(string source, string dest)
        {
            string destinationDir = Path.GetDirectoryName(dest);

            if (!Directory.Exists(destinationDir))
                Directory.CreateDirectory(destinationDir);

            File.Copy(source, dest, true);
        }

        private string findAssetPath(string source)
        {
            FileInfo fileInfo = new FileInfo(source);
            DirectoryInfo dirInfo = fileInfo.Directory;
            string assetPathBuilder = dirInfo.Name;

            while (dirInfo.Name != "Assets")
            {
                assetPathBuilder = Path.Combine(dirInfo.Name, assetPathBuilder);
                dirInfo = dirInfo.Parent;
                if (dirInfo == null || dirInfo.Name == "SOCassets")
                    return Path.Combine("Assets", fileInfo.Name);
            }
            return Path.Combine("Assets", assetPathBuilder, fileInfo.Name);
        }
    }
}
