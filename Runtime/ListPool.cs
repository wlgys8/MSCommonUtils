using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{
    public class ListPool<T>
    {
    
        private static ObjectPool<List<T>> _cache = new ObjectPool<List<T>>();


        public static int Count{
            get{
                return _cache.Count;
            }
        }
        public static List<T> Request(){
            if(Count == 0){
                return new List<T>();
            }else{
                return _cache.Request();
            }
        }

        /// <summary>
        /// List will be cleared and put back into the pool
        /// </summary>
        public static void Release(List<T> list){
            list.Clear();
            _cache.Release(list);
        }
    }
}
