using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MS.CommonUtils{
    public enum KeyboardEventType{
        Down,
        Up,
        Pressing,
    }  


    /// <summary>
    /// 注册监听输入事件
    /// </summary>
    public class AppInputRegister
    {
        private static List<KeyboardEvent> _keyboardEvents = new List<KeyboardEvent>();

        internal static void CopyKeyboardEventsTo(List<KeyboardEvent> list){
            list.AddRange(_keyboardEvents);
        }

        /// <summary>
        /// 注册监听键盘输入事件
        /// </summary>
        public static void RegisterKeyEvent(KeyCode keyCode,KeyboardEventType eventType,UnityAction action){
            InitializeIfNot();
            var evt = new KeyboardEvent(){
                code = keyCode,
                eventType = eventType,
                action = action
            };
            _keyboardEvents.Add(evt);
        }

        /// <summary>
        /// 取消注册键盘监听事件
        /// </summary>
        public static bool UnregisterKeyEvent(KeyCode keyCode,KeyboardEventType eventType,UnityAction action){
            var idx = 0;
            foreach(var item in _keyboardEvents){
                if(item.code == keyCode && item.eventType == eventType && item.action == action){
                    _keyboardEvents.RemoveAt(idx);
                    return true;
                }
                idx ++;
            }
            return false;
        }

        internal class KeyboardEvent{
            public KeyCode code;
            public KeyboardEventType eventType;
            public UnityAction action;
        }


        private static bool _initialized = false;
        private static void InitializeIfNot(){
            if(_initialized){
                return;
            }
            _initialized = true;
            var mono = new GameObject("AppInputRegister").AddComponent<AppInputRegisterMono>();
            GameObject.DontDestroyOnLoad(mono.gameObject);
        }
    }

    internal class AppInputRegisterMono:MonoBehaviour{
        private List<AppInputRegister.KeyboardEvent> _keyboardEventsRunning = new List<AppInputRegister.KeyboardEvent>();

        void Update()
        {
            AppInputRegister.CopyKeyboardEventsTo(_keyboardEventsRunning);
            foreach(var evt in _keyboardEventsRunning){
                try{
                    if(evt.eventType == KeyboardEventType.Down){
                        if(Input.GetKeyDown(evt.code)){
                            evt.action();
                        }
                    }else if(evt.eventType == KeyboardEventType.Up){
                        if(Input.GetKeyUp(evt.code)){
                            evt.action();
                        }
                    }else if(evt.eventType == KeyboardEventType.Pressing){
                        if(Input.GetKey(evt.code)){
                            evt.action();
                        }
                    }
                }catch(System.Exception e){
                    Debug.LogException(e);
                }
            }
            _keyboardEventsRunning.Clear();
        }
    }
}
