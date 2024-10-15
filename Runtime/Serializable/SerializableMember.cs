using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unity.Serialization
{

    [Serializable]
    struct SerializableMember
    {
        /// <summary>
        /// name
        /// </summary>
        public string n;
        /// <summary>
        /// index
        /// </summary>
        public int _;
        /// <summary>
        /// value
        /// </summary>
        public SerializableValue v;

        public SerializableMember(string name, SerializableValue value)
        {
            n = name;
            v = value;
            _ = -1;
        }

        public SerializableMember(string name, SerializableTypeCode typeCode)
        {
            n = name;
            v = new SerializableValue(typeCode);
            _ = -1;
        }
        public SerializableMember(string name, int index)
        {
            n = name;
            _ = index;
            v = SerializableValue.Null;
        }
        public SerializableMember(int index)
        {
            n = null;
            this._ = index;
            v = SerializableValue.Null;
        }

    }

    //[Serializable]
    //struct ReferenceMember
    //{

    //}

}
