using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{
    public class AppStates : MonoBehaviour
    {
        
        public static bool quiting{
            get;private set;
        }
        private void OnApplicationQuit() {
            quiting = true;
        }


        [RuntimeInitializeOnLoadMethod]
        private static void Initialize(){
            var states = new GameObject("AppStates").AddComponent<AppStates>();
            Object.DontDestroyOnLoad(states.gameObject);
        }
    }
}
