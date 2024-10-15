using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Serialization
{
    /// <summary>
    /// RectOffset UnityException: set_left is not allowed to be called during serialization, call it from OnEnable instead.
    /// </summary>
    [Serializable]
    public class SerializableRectOffset
    {
        [SerializeField]
        public int left;
        [SerializeField]
        public int right;
        [SerializeField]
        public int top;
        [SerializeField]
        public int bottom;

        public SerializableRectOffset()
        {
        }
        public SerializableRectOffset(int left, int right, int top, int bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

        public RectOffset ToRectOffset()
        {
            return new RectOffset(left, right, top, bottom);
        }
        public override string ToString()
        {
            return left + "," + right + "," + top + "," + bottom;
        }
    }
}
