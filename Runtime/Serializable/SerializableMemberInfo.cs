using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Serialization
{

    class SerializableMemberInfo
    {
        public string name;
        public SerializableTypeCode typeCode;
        public Type valueType;
        public FieldInfo field;
        public PropertyInfo property;

        static Dictionary<Type, Dictionary<string, SerializableMemberInfo>> cachedMembers;

        public static Dictionary<string, SerializableMemberInfo> GetMembers(Type type)
        {
            if (cachedMembers == null) cachedMembers = new();

            Dictionary<string, SerializableMemberInfo> members;

            if (cachedMembers.TryGetValue(type, out members))
                return members;

            members = new();

            Type parent = type.BaseType;
            if (parent != null)
            {
                var tmp = GetMembers(parent);
                if (tmp != null)
                {
                    foreach (var item in tmp)
                    {
                        if (members.ContainsKey(item.Key))
                            continue;
                        members[item.Key] = item.Value;
                    }

                }
            }


            SerializableMemberInfo member;
            foreach (var mInfo in type.GetMembers(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetField | BindingFlags.SetProperty))
            {
                if (!(mInfo.MemberType == MemberTypes.Field || mInfo.MemberType == MemberTypes.Property))
                    continue;
                if (mInfo.DeclaringType != type)
                    continue;
                member = null;

                var field = mInfo as FieldInfo;

                if (field != null)
                {
                    if (field.IsInitOnly) continue;
                    if (field.IsDefined(typeof(NonSerializedAttribute), true)) continue;
                    if (!field.IsPublic && !field.IsDefined(typeof(SerializeField), true)) continue;

                    member = new SerializableMemberInfo() { field = field, valueType = field.FieldType };
                }
                else
                {
                    var property = mInfo as PropertyInfo;
                    if (property != null)
                    {
                        if (!property.CanWrite && property.PropertyType.IsValueType)
                        {
                            continue;
                        }
                        if (property.IsIndexer()) continue;
                        //if (property.SetMethod == null || !property.SetMethod.IsPublic) continue;
                        member = new SerializableMemberInfo() { property = property, valueType = property.PropertyType };
                    }
                }

                SerializableTypeCode typeCode = SerializableUtility.TypeToSerializableTypeCode(member.valueType, out var itemType);
                if (typeCode == SerializableTypeCode.Object)
                {
                    if (!member.valueType.IsDefined(typeof(SerializableAttribute), false))
                        continue;
                }
                else if ((typeCode & SerializableTypeCode.Array) != 0)
                {
                    SerializableTypeCode itemTypeCode = typeCode & ~SerializableTypeCode.Array;
                    if (itemTypeCode == SerializableTypeCode.Object)
                    {
                        if (!itemType.IsDefined(typeof(SerializableAttribute), false))
                            continue;
                    }
                }


                if (member != null)
                {
                    member.name = mInfo.Name;
                    member.typeCode = typeCode;
                    members[mInfo.Name] = member;
                }
            }
            cachedMembers[type] = members;
            return members;
        }
    }
}
