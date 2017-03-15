using UnityEngine;
using System.Collections;
using PTGame.Core;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using System;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;

namespace PTGame
{
	public class PTGameConfig:PTSingleton<PTGameConfig>
    {
		private string keyChain;
        public PTGameConfig()
        {
			PTDebug.Log ("ptgameconfig construct **********");
		
        }
		public void Init(string projectTag){
			PTDebug.Log ("ptgameconfig init **********");

			PTUGame.Instance.CheckAppVersion ();
			LoadConfigFile (projectTag);
			LoadConfig (ConfigContent);
			PTUGame.Instance.GetDeviceId (keyChain);
		}

		private string m_configContent;
		public string  ConfigContent{
			get{ return m_configContent;}
			
		}

		/// <summary>
		/// 加载配置表
		/// </summary>
		private void LoadConfigFile (string projectTag)
		{

			string sPath = "/AssetBundles/" + PTUtils.GetPlatformName () + "/" + projectTag + "/ptconfig.pt";

			string finalPath = String.Format ("{0}" + sPath, Application.persistentDataPath);
			if (!File.Exists (finalPath)) {
				if (Application.platform == RuntimePlatform.Android) {
					PTGameConfig.ReadConfigFile (projectTag, finalPath);
				} else {
					finalPath = String.Format ("{0}" + "/ptconfig.pt", Application.streamingAssetsPath);
				}
			}
			PTDebug.Log ("van finalPath:" + finalPath);

			m_configContent = File.ReadAllText (finalPath);
			if (string.IsNullOrEmpty (m_configContent)) {
				PTDebug.Log ("****  PTGame read config file faied *****");
				return;
			}
			m_configContent = PTGameConfig.Decrypt (m_configContent);
		
		}


		public void LoadConfig(string content)
        {
			PTDebug.Log(" ---- LOAD PTGame CONFIG START  -----"+content);

            XmlDocument xmlDoc = new XmlDocument();
	
            xmlDoc.LoadXml(content);
		
			XmlNode  deviceConfig = xmlDoc.SelectSingleNode("config/deviceId");
	
			string def = deviceConfig.Attributes["default"].Value;
	
			keyChain =deviceConfig.SelectSingleNode(def).Attributes["keychain"].Value;

			PTDebug.Log(" ---- LOAD PTGame CONFIG FINISH  -----");
            xmlDoc = null;
        }

    
        public static string Decrypt(string toDecrypt)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            // AES-256 key
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            // better lang support
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

		public static void ReadConfigFile(string defaultProjectTag,string destPath)
		{

			Debug.Log ("ReadConfigFile************ van *****");
			string streamAssetRoot = Application.dataPath;

			ZipFile zipFile = new ZipFile(streamAssetRoot);
			string entryName = "assets/ptconfig.pt";
			ZipEntry entry = zipFile.GetEntry(entryName);

			PTDebug.Log("Read ptconfig file:" + entryName + "" + (entry == null ? "failed" : "success"));
			if (null != entry)
			{
				int nReadCount = 0;
				int nReadByte = 0;
				byte[] readBuff = new byte[4096];
				Stream inputStream = zipFile.GetInputStream(entry);

				if(!Directory.Exists(Application.persistentDataPath+"/AssetBundles/" + PTUtils.GetPlatformName()+"/"+defaultProjectTag)){
					Directory.CreateDirectory (Application.persistentDataPath+"/AssetBundles/" + PTUtils.GetPlatformName()+"/"+defaultProjectTag);
				}

				FileStream outputStream = File.OpenWrite(destPath);

				while (true)
				{
					nReadCount += nReadByte;

					nReadByte = inputStream.Read(readBuff, 0, readBuff.Length);
					if (nReadByte > 0)
					{
						outputStream.Write(readBuff, 0, nReadByte);
					}
					else
					{
						break;
					}
				}

				inputStream.Close();
				outputStream.Close();
			}

			zipFile.Close();

	        PTDebug.Log ("ReadConfigFile  end ************ van *****");

		}

    }
}
