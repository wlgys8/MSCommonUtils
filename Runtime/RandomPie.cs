using System.Collections.Generic;

namespace MS.CommonUtils{
    public class RandomPie<T>
    {
        private Dictionary<T,float> _elements = new Dictionary<T, float>();

        private System.Random _random;

        private float _totalWeight = 0;

        public RandomPie(){
            _random = new System.Random();
        }

        public RandomPie(int seed){
            _random = new System.Random(seed);
        }

        public void Add(T element,float weight){
            if(weight <= 0){
                throw new System.Exception("weight mush be > 0");
            }
            _elements.Add(element,weight);
            _totalWeight += weight;
        }

        public void Clear(){
            _elements.Clear();
        }

        public int ElementCount{
            get{
                return _elements.Count;
            }
        }

        public T Random(){
            if(ElementCount == 0){
                throw new System.Exception("element count is zero");
            }
            float value = _random.Next() * 1f / int.MaxValue;
            float weight = 0f;
            foreach(var pair in _elements){
                weight += pair.Value;
                if(value <= weight / _totalWeight){
                    return pair.Key;
                }
            }
            throw new System.Exception("get random value failed");
        }
    }
}
