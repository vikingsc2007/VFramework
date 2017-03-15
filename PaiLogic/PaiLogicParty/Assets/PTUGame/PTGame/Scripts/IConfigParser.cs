using UnityEngine;
using System.Collections;

namespace PTGame{
	public interface IConfigParser
	{
		void LoadConfig (string content);
	}
}