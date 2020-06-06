using UnityEngine;

namespace MS.CommonUtils
{

    /// <summary>
    /// 加载int字段上，标记为layer。 在Insepctor即可以显示为layer控件
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Field)]
    public class LayerFieldAttribute : PropertyAttribute{}
}
