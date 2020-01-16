using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MS.CommonUtils
{
    
    public static class GameObjectExts
    {
        
        public static void SetLayerRecursively(this GameObject go,int layer){
            go.layer = layer;
            for(var i = 0; i < go.transform.childCount;i++){
                go.transform.GetChild(i).gameObject.SetLayerRecursively(layer);
            }
        }
    }
}
