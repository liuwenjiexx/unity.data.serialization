using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Serialization
{

    enum SerializableTypeCode : byte
    {
        Null = 0,

        #region Base Types

        Object = 1,
        DBNull = 2,
        Boolean = 3,
        Char = 4,
        SByte = 5,
        Byte = 6,
        Int16 = 7,
        UInt16 = 8,
        Int32 = 9,
        UInt32 = 10,
        Int64 = 11,
        UInt64 = 12,
        Single = 13,
        Double = 14,
        Decimal = 15,
        DateTime = 16,
        String = 18,
        BaseTypeMax = String,
        #endregion

        Type = 20,

        #region Unity Types


        UnityObject = 32,
        Vector2 = 33,
        Vector2Int = 34,
        Vector3 = 35,
        Vector3Int = 36,
        Vector4 = 37,
        Color = 38,
        Color32 = 39,
        Rect = 40,
        RectInt = 41,
        Bounds = 42,
        BoundsInt = 43,
        AnimationCurve = 44,
        RectOffset = 45,
        RectOffsetSerializable = 46,

        #endregion

        #region Array

        Array = 128,
        List = 128,
        ArrayOrList = Array | List,

        #endregion


    }


}
