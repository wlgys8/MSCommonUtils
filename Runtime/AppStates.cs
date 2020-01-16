using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MS.CommonUtils{
    public class AppStates : MonoBehaviour
    {
        
        public static event UnityAction<bool> onPause;
        public static bool quiting{
            get;private set;
        }

        private void OnApplicationQuit() {
            quiting = true;
        }

        void OnApplicationPause(bool paused){
            if(onPause != null){
                onPause(paused);
            }
        }


        [RuntimeInitializeOnLoadMethod]
        private static void Initialize(){
            var states = new GameObject("AppStates").AddComponent<AppStates>();
            Object.DontDestroyOnLoad(states.gameObject);
        }
    }
}
