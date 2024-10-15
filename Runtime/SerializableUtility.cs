using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Serialization
{
    internal static class SerializableUtility
    {

        internal static bool IsBaseType(SerializableTypeCode typeCode)
        {
            if (typeCode == SerializableTypeCode.Object)
            {
                return false;
            }
            else if ((typeCode & SerializableTypeCode.ArrayOrList) != 0)
            {
                return false;
            }
            return true;
        }

        internal static SerializableTypeCode TypeToSerializableTypeCode(Type type)
        {
            return TypeToSerializableTypeCode(type, out var itemType);
        }

        internal static SerializableTypeCode TypeToSerializableTypeCode(Type type, out Type itemType)
        {
            itemType = null;
            if (type == null)
                return SerializableTypeCode.Null;
            if (type == null) 
                Debug.Log("null");
            var typeCode = (SerializableTypeCode)Type.GetTypeCode(type);
            SerializableTypeCode elemTypeCode;


            if (type.IsArray)
            {
                itemType = type.GetElementType();
                elemTypeCode = TypeToSerializableTypeCode(itemType);
                if (elemTypeCode != SerializableTypeCode.Object)
                    typeCode = SerializableTypeCode.Array | elemTypeCode;
            }
            var listType = type.FindGenericTypeDefinition(typeof(IList<>));
            if (listType != null)
            {
                itemType = listType.GetGenericArguments()[0];
                elemTypeCode = TypeToSerializableTypeCode(itemType);
                typeCode = SerializableTypeCode.List | elemTypeCode;
            }

            if (typeCode == SerializableTypeCode.Object)
            {
                if (typeof(UnityEngine.Object).IsAssignableFrom(type))
                {
                    typeCode = SerializableTypeCode.UnityObject;
                }
                else
                {
                    if (type == typeof(Vector2))
                        typeCode = SerializableTypeCode.Vector2;
                    else if (type == typeof(Vector2Int))
                        typeCode = SerializableTypeCode.Vector2Int;
                    else if (type == typeof(Vector3))
                        typeCode = SerializableTypeCode.Vector3;
                    else if (type == typeof(Vector3Int))
                        typeCode = SerializableTypeCode.Vector3Int;
                    else if (type == typeof(Vector4))
                        typeCode = SerializableTypeCode.Vector4;
                    else if (type == typeof(Color))
                        typeCode = SerializableTypeCode.Color;
                    else if (type == typeof(Color32))
                        typeCode = SerializableTypeCode.Color32;
                    else if (type == typeof(Rect))
                        typeCode = SerializableTypeCode.Rect;
                    else if (type == typeof(RectInt))
                        typeCode = SerializableTypeCode.RectInt;
                    else if (type == typeof(RectOffset))
                        typeCode = SerializableTypeCode.RectOffset;
                    else if (type == typeof(SerializableRectOffset))
                        typeCode = SerializableTypeCode.RectOffsetSerializable;
                    else if (type == typeof(AnimationCurve))
                        typeCode = SerializableTypeCode.AnimationCurve;
                }
            }
            return typeCode;
        }

        internal static Type SerializableTypeCodeToType(SerializableTypeCode typeCode)
        {

            if ((typeCode & SerializableTypeCode.Array) == SerializableTypeCode.Array)
            {
                return SerializableTypeCodeToType(typeCode).MakeArrayType();
            }
            if ((typeCode & SerializableTypeCode.List) == SerializableTypeCode.List)
            {
                return typeof(List<>).MakeGenericType(SerializableTypeCodeToType(typeCode));
            }

            switch (typeCode)
            {
                case SerializableTypeCode.String:
                    return typeof(string);
                case SerializableTypeCode.Int32:
                    return typeof(int);
                case SerializableTypeCode.Single:
                    return typeof(float);
                case SerializableTypeCode.Boolean:
                    return typeof(bool);
                case SerializableTypeCode.DBNull:
                    return typeof(DBNull);
                case SerializableTypeCode.Char:
                    return typeof(char);
                case SerializableTypeCode.SByte:
                    return typeof(sbyte);
                case SerializableTypeCode.Byte:
                    return typeof(byte);
                case SerializableTypeCode.Int16:
                    return typeof(short);
                case SerializableTypeCode.Int64:
                    return typeof(long);
                case SerializableTypeCode.UInt16:
                    return typeof(ushort);
                case SerializableTypeCode.UInt32:
                    return typeof(uint);
                case SerializableTypeCode.UInt64:
                    return typeof(ulong);
                case SerializableTypeCode.Double:
                    return typeof(double);
                case SerializableTypeCode.Decimal:
                    return typeof(decimal);
                case SerializableTypeCode.DateTime:
                    return typeof(DateTime);
                case SerializableTypeCode.UnityObject:
                    return typeof(UnityEngine.Object);
                case SerializableTypeCode.Vector2:
                    return typeof(Vector2);
                case SerializableTypeCode.Vector2Int:
                    return typeof(Vector2Int);
                case SerializableTypeCode.Vector3:
                    return typeof(Vector3);
                case SerializableTypeCode.Vector3Int:
                    return typeof(Vector3Int);
                case SerializableTypeCode.Vector4:
                    return typeof(Vector4);
                case SerializableTypeCode.Color:
                    return typeof(Color);
                case SerializableTypeCode.Color32:
                    return typeof(Color32);
                case SerializableTypeCode.Rect:
                    return typeof(Rect);
                case SerializableTypeCode.RectInt:
                    return typeof(RectInt);
                case SerializableTypeCode.RectOffset:
                    return typeof(RectOffset);
                case SerializableTypeCode.RectOffsetSerializable:
                    return typeof(SerializableRectOffset);
                case SerializableTypeCode.AnimationCurve:
                    return typeof(AnimationCurve);
            }
            return typeof(object);
        }
    }
}
