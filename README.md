# MSCommonUtils

 一些通用的工具类


# 简单索引


## Math

* `RandomBag` 

    随机对象背包，模拟背包随机取物.
* `RandomPie` 

    饼状随机实现

## Pool

* `ObjectPool<T>` 

    最基础的对象池, 通过`Request`获取，`Release`释放
* `ListPool<T>` 

    List的对象池. 其实就是`ObjectPool<List<T>>`的封装
* `ComponentPool<T>` 

    在·ObjectPool·的基础上，限定T类型为Unity中的场景对象组件，存取的时候，会自动对gameObject进行可见性管理
* `ResourcesComponentPool` 

    继承自ComponentPool，取的时候如果池中已无对象，自动从Resources中加载并实例化

## Events

* `EventDispatcher` 

    以Monobehaviour实现接口的形式，来针对gameObject进行事件分发管理.

## Editor

* `LayerFieldAttribute`

    加在Int字段上，可以在Inspector上显示为Layer选择组件。