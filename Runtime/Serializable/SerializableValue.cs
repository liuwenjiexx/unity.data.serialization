using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Unity.Serialization
{
    [Serializable]
    struct SerializableValue
    {
        /// <summary>
        /// typeCode
        /// </summary>
        public SerializableTypeCode c;
        /// <summary>
        /// string value
        /// </summary>
        public string s;
        /// <summary>
        /// UnityEngine.Object value
        /// </summary>
        public UnityEngine.Object o;

        /// <summary>
        /// int value
        /// </summary>
        public long i;

        /// <summary>
        /// float value
        /// </summary>
        public double f;

        public SerializableValue(SerializableTypeCode code, string value)
        {
            c = code;
            s = value;
            o = null;
            i = 0L;
            f = 0f;
        }

        public SerializableValue(SerializableTypeCode code)
        {
            c = code;
            s = null;
            o = null;
            i = 0L;
            f = 0f;
        }
        public SerializableValue(Object @object)
        {
            c = SerializableTypeCode.UnityObject;
            s = null;
            o = @object;
            i = 0L;
            f = 0f;
        }

        public void SetNull()
        {
            c = SerializableTypeCode.Null;
            s = null;
            o = null;
            i = 0L;
            f = 0f;
        }


        public static readonly SerializableValue Null = new SerializableValue(SerializableTypeCode.Null);

        public static implicit operator SerializableValue(SerializableTypeCode code)
        {
            return new SerializableValue(code);
        }

        public static implicit operator SerializableTypeCode(SerializableValue value)
        {
            return value.c;
        }

        public static implicit operator SerializableValue(UnityEngine.Object value)
        {
            return new SerializableValue(value);
        }
    }

}
