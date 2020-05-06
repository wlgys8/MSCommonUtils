using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MS.CommonUtils{

    /// <summary>
    /// 用这个类，实现在Unity的主线程上运行指定的函数。
    /// </summary>
    public class ActionExecutorThreadSafe
    {
        private List<UnityAction> _actions = new List<UnityAction>();

        private ActionExecutorThreadSafe(){

        }

        /// <summary>
        /// 将指定的UnityAction在Unity主线程上执行.
        /// action会被压入执行队列，并在下一个Update周期执行
        /// </summary>
        public void Execute(UnityAction action){
            lock(_actions){
                    _actions.Add(action);
            }
        }

        internal void InvokeAllActions(){
            List<UnityAction> invokeActions = null;
            lock(_actions){
                if(_actions.Count == 0){
                    return;
                }
                invokeActions = ListPool<UnityAction>.Request();
                invokeActions.AddRange(_actions);
                _actions.Clear();
            }
            foreach(var ac in invokeActions){
                try{
                    ac();
                }catch(System.Exception e){
                    Debug.LogException(e);
                }
            }
            ListPool<UnityAction>.Release(invokeActions);
        }

        private static ActionExecutorThreadSafe _default;
        public static ActionExecutorThreadSafe Default{
            get{
                if(_default == null){
                    _default = new ActionExecutorThreadSafe();
                }
                return _default;
            }
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize(){
            var bh = new GameObject("ActionExecutorBehaviour").AddComponent<ActionExecutorBehaviour>();
            Object.DontDestroyOnLoad(bh.gameObject);
        }
    }

    internal class ActionExecutorBehaviour:MonoBehaviour{

        void Update(){
            ActionExecutorThreadSafe.Default.InvokeAllActions();
        }
    }
}
