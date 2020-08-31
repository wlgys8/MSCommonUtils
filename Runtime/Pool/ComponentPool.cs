using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{


    /// <summary>
    /// Designed for storing Component objects.
    /// All objects released into the pool will be the child of poolNode,which won't be destroyed on scene unload.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComponentPool<T> :ObjectPool<T> where T:Component
    {
        
        private string _name;

        public ComponentPool(string name){
            _name = name;
        }

        private Transform _poolNode;
        public Transform poolNode{
            get{
                if(AppQuittingCheck.quitting){
                    return null;
                }
                if(!_poolNode){
                    _poolNode = new GameObject(_name).transform;
                    _poolNode.gameObject.SetActive(false);
                    Object.DontDestroyOnLoad(_poolNode.gameObject);
                }
                return _poolNode;
            }
        }

        /// <summary>
        /// Remove all items from the pool, and call Object.Destroy on it's gameObject.
        /// </summary>
        public void ClearAndDestroyAll(){
            while(Count > 0){
                var ret = Request();
                Object.Destroy(ret.gameObject);
            }
        }


        public T Request(Transform parent){
            T ret = base.Request();
            ret.transform.SetParent(parent,false);
            return ret;
        }

        public override T Request(){
            return Request(null);
        }

        public override void Release(T item){
            var p = poolNode;
            if(!item){
                return;
            }
            base.Release(item);
            item.transform.SetParent(poolNode,false);
        }
        
    }

    internal static class AppQuittingCheck{
        public static bool quitting{
            get{
                return _quiting;
            }
        }
        private static void OnApplicationQuit(){
            _quiting = true;
        }

        [RuntimeInitializeOnLoadMethod]
        private static void InitializeOnLoad(){
            Application.quitting += OnApplicationQuit;
        }

        private static bool _quiting = false;        
    }

}
