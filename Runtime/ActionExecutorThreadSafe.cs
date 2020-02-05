using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MS.CommonUtils{
    public class ActionExecutorThreadSafe
    {
       private List<UnityAction> _actions = new List<UnityAction>();

        private ActionExecutorThreadSafe(){

        }
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
