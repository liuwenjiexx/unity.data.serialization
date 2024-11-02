using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Serialization
{
    //JsonUtility.ToJson(struct<T>)  Serialize fail
    /// <summary>
    /// 对象序列化
    /// 新的 <see cref="MonoBehaviour"/> 和 <see cref="ScriptableObject"/> 对象成员序列化使用 <see cref="SerializeReference"/>
    /// 简单对象序列化 json 使用该类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class SerializableObject<T> : ISerializationCallbackReceiver
    {
        [NonSerialized]
        private T target;
        [SerializeField]
        private string typeName;
        [SerializeField]
        private string data;
        [NonSerialized]
        private Exception deserializeError;
        [SerializeField]
        private bool delay;
        [NonSerialized]
        private bool deserialized;

        /// <summary>
        /// System.Type
        /// </summary>
        const string TYPE_NAME_TYPE = ":T";
        const string TYPE_NAME_TYPE2 = "Type";

        public SerializableObject()
        {
        }

        public SerializableObject(T target)
        {
            this.target = target;
            data = null;
            typeName = null;
            deserializeError = null;
            deserialized = true;
        }


        public T Target
        {
            get
            {
                if (!deserialized)
                {
                    DeserializeTarget();
                    if (deserializeError != null)
                        return default;
                }
                return target;
            }

            set
            {
                target = value;
                deserializeError = null;
                deserialized = true;
            }
        }

        public bool IsDelayDeserialize
        {
            get => delay;
            set => delay = value;
        }

        public Exception DeserializeError
        {
            get => deserializeError;
        }

        public void OnBeforeSerialize()
        {
            if (deserializeError != null)
                return;
            typeName = null;
            data = null;

            if (target != null)
            {
                if (typeof(T) == typeof(Type))
                {
                    typeName = TYPE_NAME_TYPE2;
                    data = ((Type)(object)target).AssemblyQualifiedName;
                }
                else
                {
                    typeName = target.GetType().AssemblyQualifiedName;
                    data = JsonUtility.ToJson(target);
                }
            }
        }

        public void OnAfterDeserialize()
        {
            if (!delay)
            {
                DeserializeTarget();
            }
        }

        private void DeserializeTarget()
        {
            try
            {
                deserialized = true;
                deserializeError = null;
                target = default;

                if (!string.IsNullOrEmpty(typeName))
                {
                    Type type;

                    if (typeName == TYPE_NAME_TYPE || typeName == TYPE_NAME_TYPE2)
                    {
                        type = typeof(Type);
                    }
                    else
                    {
                        type = Type.GetType(typeName);
                    }

                    if (type != null)
                    {
                        if (type == typeof(Type))
                        {
                            if (!string.IsNullOrEmpty(data))
                            {
                                target = (T)(object)Type.GetType(data);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(data))
                            {
                                target = (T)JsonUtility.FromJson(data, type);
                            }
                            else
                            {
                                target = default;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                deserializeError = ex;
            }
        }


        public static implicit operator T(SerializableObject<T> a)
        {
            return a.Target;
        }
        public static implicit operator SerializableObject<T>(T a)
        {
            return new SerializableObject<T>(a);
        }
    }
}
