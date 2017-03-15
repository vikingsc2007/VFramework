using UnityEngine;
using System.Collections;
using System;

namespace PTGame
{
	public class PTSystemCallback : MonoBehaviour
	{

		private static PTSystemCallback mInstance = null;

		public static PTSystemCallback Instance {
			get {
				if (mInstance == null) {
			
					GameObject ptugame = GameObject.Find ("PTUGame");
					if (ptugame != null) {
						mInstance = ptugame.AddComponent<PTSystemCallback> ();
					}
				}
				return mInstance;
			}
		}

		public Action systemNotSupportCallback;
		public Action<string> saveImageCallback;



		/// <summary>
		/// Raises the system not support confirm event.
		/// </summary>
		public void OnSystemNotSupportConfirm ()
		{
			if (systemNotSupportCallback != null) {
				systemNotSupportCallback ();
			}
			systemNotSupportCallback = null;
		}

		/// <summary>
		/// Saves the image to album callback.
		/// </summary>
		/// <param name="result">Result. "success"  "failed"</param>
		public void SaveImageToAlbumCallback (string result)
		{
	
			if (saveImageCallback != null) {
				saveImageCallback (result);	
			}
			saveImageCallback = null;
		}



		public Action cancleUpdateAppListener;
		public Action selectUpdateAppListener;

		public void CancleUpdateApp ()
		{
			if (cancleUpdateAppListener != null) {
				cancleUpdateAppListener ();
			}
		}

		public void SelectUpdateApp ()
		{
			if (selectUpdateAppListener != null) {
				selectUpdateAppListener ();
			}

		}

		public Action systemAlertConfirmListener;

		public void OnSystemAlertConfirmClick ()
		{
	
			if (systemAlertConfirmListener != null) {
				systemAlertConfirmListener ();
			}
		}

		public Action<string> ptassistGetUserInfo;

		public void OnPTAssistGetUserInfo (string userInfo)
		{
			if (ptassistGetUserInfo != null) {
				ptassistGetUserInfo (userInfo);
			}
		}

	}

}