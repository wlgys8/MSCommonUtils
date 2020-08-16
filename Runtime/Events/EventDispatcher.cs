using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils{
    public class EventDispatcher
    {
        public static void GetHandlers<T>(GameObject target,List<T> handlers){
            var components = ListPool<MonoBehaviour>.Request();
            handlers.Clear();
            target.GetComponents<MonoBehaviour>(components);
            foreach(var mono in components){
                if(mono is T){
                    T res = (T)(object)mono;
                    handlers.Add(res);
                }
            }
            ListPool<MonoBehaviour>.Release(components);
        }

        public static bool Execute<T>(GameObject target,object param,ExecuteFunction<T> executeFunc){
            var handlers = ListPool<T>.Request();
            GetHandlers<T>(target,handlers);
            if(handlers.Count == 0){
                return false;
            }
            foreach(var handler in handlers){
                executeFunc(handler,param);
            }
            ListPool<T>.Release(handlers);
            return true;
        }
        public static bool Execute<T,S>(GameObject target,S state,ExecuteFunction<T,S> executeFunc){
            var handlers = ListPool<T>.Request();
            GetHandlers<T>(target,handlers);
            if(handlers.Count == 0){
                return false;
            }
            foreach(var handler in handlers){
                executeFunc(handler,state);
            }
            ListPool<T>.Release(handlers);
            return true;
        }

        public static bool ExecuteHierarchy<T>(GameObject go,object param,ExecuteFunction<T> executeFunction){
            while(true){
                try{
                    if(!go){
                        return false;
                    }
                    var res = Execute<T>(go,param,executeFunction);
                    if(res){
                        return true;
                    }
                    var parent = go.transform.parent;
                    if(!parent){
                        return false;
                    }
                    go = parent.gameObject;
                }catch(System.Exception e){
                    Debug.LogException(e);
                    return false;
                }
            }
        }

        public static bool ExecuteHierarchy<T,S>(GameObject go,S state,ExecuteFunction<T,S> executeFunction){
            while(true){
                try{
                    if(!go){
                        return false;
                    }
                    var res = Execute<T,S>(go,state,executeFunction);
                    if(res){
                        return true;
                    }
                    var parent = go.transform.parent;
                    if(!parent){
                        return false;
                    }
                    go = parent.gameObject;
                }catch(System.Exception e){
                    Debug.LogException(e);
                    return false;
                }
            }
        }

        public delegate void ExecuteFunction<T>(T handler,object param);

        public delegate void ExecuteFunction<T,S>(T handler,S state);

    }
}
