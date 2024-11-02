using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Serialization
{
    [Serializable]
    public class SerializableHashSet<T> : IEnumerable<T>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<T> items;

        private HashSet<T> set = new();

        public int Count => set.Count;


        public bool Add(T item) => set.Add(item);

        public void Clear() => set.Clear();

        public bool Contains(T item) => set.Contains(item);



        public void ExceptWith(IEnumerable<T> other) => set.ExceptWith(other);

        public IEnumerator<T> GetEnumerator() => set.GetEnumerator();

        public void IntersectWith(IEnumerable<T> other) => set.IntersectWith(other);


        public void OnAfterDeserialize()
        {
            set.Clear();
            if (items != null)
            {
                foreach (var item in items)
                {
                    set.Add(item);
                }
            }
        }

        public void OnBeforeSerialize()
        {
            if (items == null) items = new();
            items.Clear();
            foreach (var item in set)
            {
                items.Add(item);
            }
        }

        public bool Overlaps(IEnumerable<T> other) => set.Overlaps(other);

        public bool Remove(T item) => set.Remove(item);


        public void UnionWith(IEnumerable<T> other) => set.UnionWith(other);

        IEnumerator IEnumerable.GetEnumerator() => set.GetEnumerator();
              
    }
}
