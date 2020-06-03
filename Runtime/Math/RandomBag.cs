using System.Collections.Generic;

namespace MS.CommonUtils{

    /// <summary>
    /// 背包随机元素取出，取一个少一个。
    /// </summary>
    public class RandomBag<T>
    {

        private Dictionary<T,int> _itemCounts = new Dictionary<T, int>();

        private System.Random _random;

        private int _totalCount = 0;

        public RandomBag(int seed){
            _random = new System.Random(seed);
        }

        

        /// <summary>
        /// 往背包中增加{count}数量的{item}
        /// </summary>
        public void Put(T item,int count){
            if(count <= 0){
                throw new System.Exception("count must be > 0");
            }
            if(_itemCounts.ContainsKey(item)){
                _itemCounts[item] += count;
            }else{
                _itemCounts.Add(item,count);
            }
            _totalCount += count;
        }

        /// <summary>
        /// 随机取出一个元素
        /// </summary>
        public T Get(){
            if(_totalCount == 0){
                throw new System.Exception("no more items");
            }
            var index = _random.Next(0,_totalCount);
            var sum = 0;
            T result = default(T);
            foreach(var kv in _itemCounts){
                sum += kv.Value;
                if(index < sum){
                    result = kv.Key;
                    break;
                }
            }
            _itemCounts[result] --;
            _totalCount --;
            return result;
        }

        public int Count{
            get{
                return _totalCount;
            }
        }

        public void Clear(){
            _itemCounts.Clear();
            _totalCount = 0;
        }
    }
}
