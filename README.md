# MSCommonUtils

 一些通用的工具类

# Dependencies

[UniPool - 简单的对象池工具](https://github.com/wlgys8/UniPool)

# Install

使用`Packages/Manifest.json`安装

```json
"com.ms.litask":"https://github.com/wlgys8/UniPool.git",
"com.ms.commonutils":"https://github.com/wlgys8/MSCommonUtils.git",
```

# 简单索引


## Pool

[参考文档](https://github.com/wlgys8/UniPool)

## Math

* `RandomBag` 

    随机对象背包，模拟背包随机取物.
* `RandomPie` 

    饼状随机实现

## Events

* `EventDispatcher` 

    以Monobehaviour实现接口的形式，来针对gameObject进行事件分发管理.

## Editor

* `LayerFieldAttribute`

    加在Int字段上，可以在Inspector上显示为Layer选择组件。