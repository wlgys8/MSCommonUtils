using UnityEngine;

namespace MS.CommonUtils{


    /// <summary>
    /// 基于ComponentPool,扩展了自动加载功能。
    /// 进行Request请求时，如果Pool中没有了，那么自动根据提供的Path，从Resources中加载
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResourcesComponentPool<T> :ComponentPool<T> where T:Component{

        private string _path;
        public ResourcesComponentPool(string poolName,string path):base(poolName){
            _path = path;
        }

        public override T Request(){
            if(this.Count > 0){
                return base.Request();
            }else{
                var go = Resources.Load<GameObject>(_path);
                if(!go){
                    return null;
                }
                return Object.Instantiate<GameObject>(go).GetComponent<T>();
            }
        }

        public override void Release(T item){
            base.Release(item);
        }
    }
}
