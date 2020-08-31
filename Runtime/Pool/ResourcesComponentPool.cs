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

        private T Load(){
            var go = Resources.Load<GameObject>(_path);
            if(!go){
                return null;
            }
            var ret = Object.Instantiate<GameObject>(go).GetComponent<T>();
            return ret;            
        }

        private T Load(Transform parent,Vector3 position,Quaternion rotation){
            var go = Resources.Load<GameObject>(_path);
            if(!go){
                return null;
            }
            return Object.Instantiate<GameObject>(go,position,rotation,parent).GetComponent<T>();
        }

        public override T Request(){
            if(this.Count > 0){
                return base.Request();
            }else{
                return this.Load();
            }
        }

        public T Request(Transform parent,Vector3 position,Quaternion rotation){
            if(this.Count > 0){
                var ret = base.Request(parent);
                ret.transform.localPosition = position;
                ret.transform.localRotation = rotation;
                return ret;
            }else{
                return this.Load(parent,position,rotation);
            }           
        }

        public override void Release(T item){
            base.Release(item);
        }
    }
}
