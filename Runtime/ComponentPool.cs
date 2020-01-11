using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MS.CommonUtils{
    public class ComponentPool<T> :ObjectPool<T> where T:Component
    {
        
        private string _name;
        public ComponentPool(string name){
            _name = name;
        }

        private Transform _poolNode;
        public Transform poolNode{
            get{
                if(AppStates.quiting){
                    return null;
                }
                var scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
                if(!_poolNode){
                    _poolNode = new GameObject(_name).transform;
                    _poolNode.gameObject.SetActive(false);
                    Object.DontDestroyOnLoad(_poolNode.gameObject);
                }
                return _poolNode;
            }
        }


        public override T Request(){
            T ret = base.Request();
            return ret;
        }

        public override void Release(T item){
            base.Release(item);
            item.transform.SetParent(poolNode,false);
        }


        
    }
}
