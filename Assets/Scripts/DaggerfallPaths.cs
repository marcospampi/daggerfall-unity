using System;
using System.IO;
using System.Linq;
using UnityEngine;
using ICSharpCode.SharpZipLib.Zip;
namespace DaggerfallWorkshop
{
    public sealed class Paths
    {
        private string streamingAssetsPath;
        private string dataPath;
        private string persistentDataPath;
        private string storagePath;

        private Paths()
        {
            Console.WriteLine(Application.systemLanguage);
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    this.initAndroid();
                    break;
                default:
                    this.init();
                    break;
            }
        }
        private void init()
        {
            storagePath = "/";
            streamingAssetsPath = Application.streamingAssetsPath;
            dataPath = Application.dataPath;
            persistentDataPath = Application.persistentDataPath;

        }
        /**
        * Application.dataPath refers to where the APK is located
        * Application.streamingAssets refers to assets inside Application.dataPath
        * Application.persistentDataPath refers to some private storage, so must be overriden
        */
        private void initAndroid()
        {


            var apkPath = Application.dataPath;

            // breaks on /storage/emulated/0/(Android)
            string basePath = "/" + Application.persistentDataPath.Split('/')
                .TakeWhile(s => s.ToLower() != "android")
                .Aggregate((a, b) => String.Concat(a, "/", b));
            
            storagePath = basePath;
            dataPath = Path.Combine(basePath, "DaggerfallUnity");
            persistentDataPath = dataPath;
            streamingAssetsPath = Path.Combine(dataPath, "assets");

            if (!Directory.Exists(dataPath)) {
                Directory.CreateDirectory(dataPath);
                // Extract apk
                {
                    FastZip fastZip = new FastZip();
                    fastZip.ExtractZip(apkPath, dataPath, @".*;-^(?!assets);-^\/assets\/bin" );
                }
            }
            
            


        }

        private static readonly Lazy<Paths> lazy = new Lazy<Paths>(() => new Paths());

        public static Paths Instance => lazy.Value;
        public static string StreamingAssetsPath => lazy.Value.streamingAssetsPath;
        public static string DataPath => lazy.Value.dataPath;
        public static string PersistentDataPath => lazy.Value.persistentDataPath;
        public static string StoragePath => lazy.Value.storagePath;


    }
}