using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{
    public static class SetPool<T>
    {
        private static ObjectPool<ISet<T>> _cache = new ObjectPool<ISet<T>>();

        /// <value>当前池中的Set对象数量</value>
        public static int Count{
            get{
                return _cache.Count;
            }
        }

        /// <summary>
        /// 从池中获取一个Set对象
        /// </summary>
        /// <returns></returns>
        public static ISet<T> Request(){
            if(Count == 0){
                return new HashSet<T>();
            }else{
                return _cache.Request();
            }
        }

        /// <summary>
        /// Set will be cleared and put back into the pool
        /// </summary>
        public static void Release(ISet<T> set){
            set.Clear();
            _cache.Release(set);
        }
    }
}
