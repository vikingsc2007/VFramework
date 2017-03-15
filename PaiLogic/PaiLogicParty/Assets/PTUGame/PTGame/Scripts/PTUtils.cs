using UnityEngine;
using System.Collections;
using System;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PTGame.Core
{
    public class PTUtils
    {

        public static double GetCurrentMilli()
        {
            DateTime Jan1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan javaSpan = DateTime.UtcNow - Jan1970;
            return javaSpan.TotalMilliseconds;
        }

        public static string Md5Sum(string strToEncrypt)
        {
            System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
            byte[] bytes = ue.GetBytes(strToEncrypt);

            // encrypt bytes
            System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] hashBytes = md5.ComputeHash(bytes);

            // Convert the encrypted bytes back to a string (base 16)
            string hashString = "";

            for (int i = 0; i < hashBytes.Length; i++)
            {
                hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
            }

            return hashString.PadLeft(32, '0');
        }

        public static bool IsNetworkReachable()
        {

            if (Application.internetReachability == NetworkReachability.NotReachable)
            {

                return false;
            }
            return true;

        }
        public static int CompareVersion(string version1, string version2)
        {
            if (version1.Equals(version2))
            {
                return 0;
            }

            string[] version1Array = version1.Split('.');
            string[] version2Array = version2.Split('.');

            int index = 0;
            int minLen = Math.Min(version1Array.Length, version2Array.Length);
            int diff = 0;

            while (index < minLen && (diff = int.Parse(version1Array[index]) - int.Parse(version2Array[index])) == 0)
            {
                index++;
            }

            if (diff == 0)
            {
                for (int i = index; i < version1Array.Length; i++)
                {
                    if (int.Parse(version1Array[i]) > 0)
                    {
                        return 1;
                    }
                }

                for (int i = index; i < version2Array.Length; i++)
                {
                    if (int.Parse(version2Array[i]) > 0)
                    {
                        return -1;
                    }
                }

                return 0;
            }
            else
            {
                return diff > 0 ? 1 : -1;
            }
        }


		public static bool IsWWWFormat(string url){
			if (url.Contains ("://")) {
				return true;
			}
			return false;
		}

		public static string GetPlatformName ()
		{
			#if UNITY_EDITOR
			return GetPlatformForAssetBundles (EditorUserBuildSettings.activeBuildTarget);
			#else
			return GetPlatformForAssetBundles(Application.platform);
			#endif
		}
		#if UNITY_EDITOR
		private static string GetPlatformForAssetBundles (BuildTarget target)
		{
			switch (target) {
			case BuildTarget.Android:
				return "Android";
			case BuildTarget.iOS:
				return "iOS";
			case BuildTarget.WebGL:
				return "WebGL";
			case BuildTarget.WebPlayer:
				return "WebPlayer";
			case BuildTarget.StandaloneWindows:
			case BuildTarget.StandaloneWindows64:
				return "Windows";
			case BuildTarget.StandaloneOSXIntel:
			case BuildTarget.StandaloneOSXIntel64:
			case BuildTarget.StandaloneOSXUniversal:
				return "OSX";
				// Add more build targets for your own.
				// If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
			default:
				return null;
			}
		}
		#endif
		private static string GetPlatformForAssetBundles (RuntimePlatform platform)
		{
			switch (platform) {
			case RuntimePlatform.Android:
				return "Android";
			case RuntimePlatform.IPhonePlayer:
				return "iOS";
			case RuntimePlatform.WebGLPlayer:
				return "WebGL";
			case RuntimePlatform.OSXWebPlayer:
			case RuntimePlatform.WindowsWebPlayer:
				return "WebPlayer";
			case RuntimePlatform.WindowsPlayer:
				return "Windows";
			case RuntimePlatform.OSXPlayer:
				return "OSX";
				// Add more build targets for your own.
				// If you add more targets, don't forget to add the same platforms to GetPlatformForAssetBundles(RuntimePlatform) function.
			default:
				return null;
			}
		}

		public static IEnumerator GetTextWWW (string from, float timeout, Action<string> callback)
		{
			WWW loader = new WWW (from);
			float duration = 0.0f;
			while (!loader.isDone && duration < timeout) {
				duration += Time.deltaTime;
				yield return 0;
			}
			if (loader.error != null || duration >= timeout) {  
				Debug.Log (loader.error);
				if (callback != null) {
					callback (null);
				}
			} else {
				if (loader.text != null) {  
					if (callback != null) {
						callback (loader.text);
					}
				}
			}
			if (loader != null) {
				loader.Dispose ();
				loader = null;
			}
		}

		public static void SaveImgToAndroid(string fileName, byte[] imagebytes)
		{
			string destination = GetAndroidAlbumPath(false);

			destination = destination + "/" + fileName;
			try
			{
				File.WriteAllBytes(destination, imagebytes);
			}
			catch(Exception ex)
			{
				Debug.LogError("SaveImgToAndroid : " + ex.Message);
			}

			AndroidJavaObject ajo = PTAndroidInterface.Instance.GetPTCustomJavaObj ();

			if (SystemInfo.deviceModel.ToLower().Contains("meizu") || SystemInfo.deviceModel.ToLower().Contains("vivo") || SystemInfo.deviceModel.ToLower().Contains("gionee"))
				ajo.CallStatic("ScanSDCardFile", destination, PTAndroidInterface.Instance.GetAndroidJavaObj(),true);        
			else
				ajo.CallStatic("ScanSDCardFile", destination, PTAndroidInterface.Instance.GetAndroidJavaObj(), false);
		}

		//Android 获取多媒体相关路径
		public static string GetAndroidAlbumPath(bool bIsVideo)
		{
			string destination = string.Empty;
			destination = "/mnt/sdcard/DCIM/Camera";
			if (PTAndroidInterface.Instance.GetPTDeviceType() == PTAndroidDeviceType.UNKNOW)
			{
				if (bIsVideo == true)
				{
					if (!Directory.Exists(destination))
					{
						destination = "/mnt/sdcard/DCIM/Video";
						if (!Directory.Exists(destination))
						{
							Directory.CreateDirectory(destination);
						}
					}
					else
					{
				
						if (SystemInfo.deviceModel.ToLower().Contains("meizu"))
						{
						destination = "/mnt/sdcard/DCIM/Video";
						Directory.CreateDirectory(destination);
						}
						else if (SystemInfo.deviceModel.ToLower().Contains("vivo"))
						{
						destination = "/mnt/sdcard/相机";
						Directory.CreateDirectory(destination);
						}
					
					}
				}
				else
				{
					if (!Directory.Exists(destination))
					{
						if (!Directory.Exists("/mnt/sdcard/DCIM"))
						{
							destination = "/storage/emulated/0/Camera";
						}
						else
						{
							destination = "/mnt/sdcard/DCIM/OceanPets";
							Directory.CreateDirectory(destination);
						}
					}
				}
			}
			else //paibot
			{
				if (!Directory.Exists(destination))
					Directory.CreateDirectory(destination);
			}
			return destination;
		}




    }
}