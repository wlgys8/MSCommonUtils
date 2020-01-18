using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MS.CommonUtils{

    //T is event type
    public class Eventbus<T>
    {
        private Dictionary<T, EventCallbackList> _callbacks = new Dictionary<T, EventCallbackList>();

        public void On(T eventType,UnityAction callback){
            if(!_callbacks.ContainsKey(eventType)){
                _callbacks.Add(eventType,new EventCallbackList());
            }
            _callbacks[eventType].Add(callback,false);
        }

        public void Off(T eventType,UnityAction callback){
            if(!_callbacks.ContainsKey(eventType)){
                return;
            }
            _callbacks[eventType].Remove(callback);
        }

        public void Once(T eventType,UnityAction callback){
            if(!_callbacks.ContainsKey(eventType)){
                _callbacks.Add(eventType,new EventCallbackList());
            }
            _callbacks[eventType].Add(callback,false);
        }

        public void Emit(T eventType){
            if(_callbacks.ContainsKey(eventType)){
                var callbackList = _callbacks[eventType];
                callbackList.Invoke();
            }
        }
    }

    //T is event type
    public class Eventbus<T,V>
    {
        private Dictionary<T, EventCallbackList<V>> _callbacks = new Dictionary<T, EventCallbackList<V>>();

        public void On(T eventType,UnityAction<V> callback){
            if(!_callbacks.ContainsKey(eventType)){
                _callbacks.Add(eventType,new EventCallbackList<V>());
            }
            _callbacks[eventType].Add(callback,false);
        }

        public void Off(T eventType,UnityAction<V> callback){
            if(!_callbacks.ContainsKey(eventType)){
                return;
            }
            _callbacks[eventType].Remove(callback);
        }

        public void Once(T eventType,UnityAction<V> callback){
            if(!_callbacks.ContainsKey(eventType)){
                _callbacks.Add(eventType,new EventCallbackList<V>());
            }
            _callbacks[eventType].Add(callback,false);
        }

        public void Emit(T eventType,V param){
            // Debug.LogWarningFormat("Emit {0}",eventType);
            if(_callbacks.ContainsKey(eventType)){
                var callbackList = _callbacks[eventType];
                callbackList.Invoke(param);
            }
        }
    }

    internal class EventCallback{
        public UnityAction action;
        public bool once;
    }

    internal class EventCallback<T>{
        public UnityAction<T> action;

        public bool once;
    }

    internal class EventCallbackList{
        private List<EventCallback> _callbacks = new List<EventCallback>();
        public void Add(UnityAction action,bool once){
            _callbacks.Add(new EventCallback(){
                action = action,
                once = once,
            });
        }

        public void Remove(UnityAction action){
            var idx = 0;
            foreach(var cb in _callbacks){
                if(cb.action == action){
                    _callbacks.RemoveAt(idx);
                }
                idx ++;
            }
        }

        public void Invoke(){
            //copy to invoke list
            var invokeList = ListPool<EventCallback>.Request();
            invokeList.AddRange(_callbacks);
            //remove once callbacks
            for(var i = _callbacks.Count - 1;i >= 0;i --){
                if(_callbacks[i].once){
                    _callbacks.RemoveAt(i);
                }
            }
            //begin invoke callbacks
            foreach(var cb in invokeList){
                try{
                    cb.action();
                }catch(System.Exception e){
                    Debug.LogException(e);
                }
            }
            invokeList.Clear();
            ListPool<EventCallback>.Release(invokeList);
        }
    }


    internal class EventCallbackList<T>{
        private List<EventCallback<T>> _callbacks = new List<EventCallback<T>>();

        public void Add(UnityAction<T> action,bool once){
            _callbacks.Add(new EventCallback<T>(){
                action = action,
                once = once,
            });
        }

        public void Remove(UnityAction<T> action){
            var idx = 0;
            foreach(var cb in _callbacks){
                if(cb.action == action){
                    _callbacks.RemoveAt(idx);
                }
                idx ++;
            }
        }

        public void Invoke(T param){
            //copy to invoke list
            var invokeList = ListPool<EventCallback<T>>.Request();
            invokeList.AddRange(_callbacks);
            //remove once callbacks
            for(var i = _callbacks.Count - 1;i >= 0;i --){
                if(_callbacks[i].once){
                    _callbacks.RemoveAt(i);
                }
            }
            //begin invoke callbacks
            foreach(var cb in invokeList){
                try{
                    cb.action(param);
                }catch(System.Exception e){
                    Debug.LogException(e);
                }
            }
            ListPool<EventCallback<T>>.Release(invokeList);
        }
    }

}
