using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

    public class MonoBehaviourPool<T> where T : Component
    {
        public ReadOnlyCollection<T> UsedItems { get; private set; }

        private readonly List<T> _notUsedItems = new();
        private readonly List<T> _usedItems = new();

        private readonly T _prefab;
        private readonly Transform _parent;

        public MonoBehaviourPool(T prefab, Transform parent, int defaultCount)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < defaultCount; i++)
            {
                AddNewItemInPool();
            }

            UsedItems = new ReadOnlyCollection<T>(_usedItems);
        }

        public T Take()
        {
            var itemFromPool = TakeWithoutSetActive();
            
            itemFromPool.gameObject.SetActive(true);

            return itemFromPool;
        }

        public T TakeWithoutSetActive()
        {
            if (_notUsedItems.Count == 0)
            {
                AddNewItemInPool();
            }

            var lastIndex = _notUsedItems.Count - 1;
            var itemFromPool = _notUsedItems[lastIndex];
            _notUsedItems.RemoveAt(lastIndex);
            _usedItems.Add(itemFromPool);

            return itemFromPool;
        }

        public void Release(T item)
        {
            item.gameObject.SetActive(false);
            item.transform.SetParent(_parent);
            
            _usedItems.Remove(item);
            _notUsedItems.Add(item);
        }

        private void AddNewItemInPool()
        {
            var newItem = Object.Instantiate(_prefab, _parent, false);
            newItem.gameObject.SetActive(false);
            _notUsedItems.Add(newItem);
        }
    }