using UnityEngine;
using System.Collections;
using PTGame.Core;
using System;
using System.IO;

namespace PTGame
{
	public class PTUGame :PTMonoSingleton<PTUGame>
	{
		private const string KEY_HOTUPDATE_APPVERSION = "kptappversion"; //是否是新版本（包括第一次安装和版本覆盖升级）
		private const string KEY_HOTUPDATE_RESUPDATED = "kpthasresupdated"; //资源是否更新过
		private bool m_isNewVersion = false;

	    internal string putaoUniqueDeviceId = "deviceid";

		[SerializeField]
		public string defaultProjectTag="putaogame";

		void Awake(){
			GameObject.DontDestroyOnLoad (gameObject);
			CheckAppVersion ();
			PTGameConfig.Instance.Init(defaultProjectTag);
			ParseConfigs(PTGameConfig.Instance.ConfigContent);
			InitComponents ();
		}
		private void ParseConfigs (string configContent)
		{
		
			IConfigParser[] parsers = this.gameObject.GetComponents<IConfigParser> ();
			int length = parsers.Length;
			for (int i = 0; i < length; i++) {
				parsers [i].LoadConfig (configContent);
			}
		}


		private void InitComponents(){
			IPTComponent[] comps = this.gameObject.GetComponents<IPTComponent> ();
			int length = comps.Length;
			for(int i=0;i<length;i++){
				comps [i].Init ();
			}
		}


		public void GetDeviceId (string keyChain)
		{
			putaoUniqueDeviceId = PTUniInterface.GetPTDeviceId (keyChain);
			if (putaoUniqueDeviceId == "deviceid") {
				PTDebug.Log ("*** get unique device id failed ***");
			} else {
				PTDebug.Log ("*** get device id : " + putaoUniqueDeviceId);
			}
		}


		public bool IsNewVersion {
			get {
				return m_isNewVersion;
			}
		}

		public void CheckAppVersion(){
			string storedVersion = PlayerPrefs.GetString (KEY_HOTUPDATE_APPVERSION, "0.0.0");
			string currentVersion = PTUniInterface.GetAppVersion ();
			m_isNewVersion = storedVersion == currentVersion ? false : true;
			PlayerPrefs.SetString (KEY_HOTUPDATE_APPVERSION, currentVersion);

			if (m_isNewVersion) {
				hasResUpdated = false;
				string sPath =Application.persistentDataPath+"/AssetBundles/" + PTUtils.GetPlatformName()+"/"+defaultProjectTag;
				if (Directory.Exists (sPath)) {
					Directory.Delete (sPath, true);
				}
			}
		}

		public bool hasResUpdated {
			get {
				bool result = PlayerPrefs.GetInt (KEY_HOTUPDATE_RESUPDATED, 0) == 0 ? false : true;
				return result;
			}
			set { 
				PlayerPrefs.SetInt (KEY_HOTUPDATE_RESUPDATED, value ? 1 : 0);
			}
		}
	}
}