using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{

    /// <summary>
    /// List类型的Pool实现。 用以重复利用List类型对象，减少GC开销。
    /// </summary>
    /// <typeparam name="T">泛型类型</typeparam>
    public class ListPool<T>
    {
    
        private static ObjectPool<List<T>> _cache = new ObjectPool<List<T>>();

        /// <value>当前池中的List对象数量</value>
        public static int Count{
            get{
                return _cache.Count;
            }
        }

        /// <summary>
        /// 从池中获取一个List对象
        /// </summary>
        /// <returns></returns>
        public static List<T> Request(){
            if(Count == 0){
                return new List<T>();
            }else{
                return _cache.Request();
            }
        }

        /// <summary>
        /// List will be cleared and put back into the pool
        /// 将一个List对象放回池中。List.Clear会被调用。
        /// </summary>
        public static void Release(List<T> list){
            list.Clear();
            _cache.Release(list);
        }
    }
}
