using UnityEngine;
using System.Collections;

namespace PTGame.Core{
	public static class PTUniExtension  {

		public static T AddUniqueComponent<T>(this GameObject gameObject,T component) where T:Component
		{
			T t = gameObject.GetComponent<T> ();

			if( t== null)
			{
				t = gameObject.AddComponent<T> ();
			}
			return t;
		}
	}
}
