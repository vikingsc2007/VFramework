using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace PTGame.Core{

    [System.Reflection.Obfuscation(ApplyToMembers = true, Exclude = true, Feature = "renaming")]
    public class PTSingleton<T>
    {

        public static T Instance
        {
            get
            {
                return m_instance;
            }
        }


        protected static T m_instance = (T)Activator.CreateInstance(typeof(T), true);
    }
}