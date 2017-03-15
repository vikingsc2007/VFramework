using UnityEngine;
using System.Collections;



	public static class PTExtensions
	{


		public static T AddMissComponent<T> (this GameObject gameObject) where T:Component
		{
			T com = gameObject.GetComponent<T> ();
			if (com == null) {
				com = gameObject.AddComponent<T> ();
			}
			return com;
		}

	}

