

using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{

    public class ObjectPool<T>{


        private List<T> _cache = new List<T>();
        private HashSet<T> _cacheSet = new HashSet<T>();

        public ObjectPool(){

        }

        public virtual T Request(){
            T ret = _cache[_cache.Count  - 1];
            _cache.RemoveAt(_cache.Count - 1);
            _cacheSet.Remove(ret);
            return ret;
        }

        public virtual void Release(T item){
            if(_cacheSet.Contains(item)){
                Debug.LogErrorFormat("Duplicate Release item {0}, ignored",item);
                return;
            }
            _cache.Add(item);
            _cacheSet.Add(item);
        }

        public int Count{
            get{
                return _cache.Count;
            }
        }

    } 

}