using System;
using System.Text;
using UnityEngine;

namespace TypedJsonSendReceives
{
    public class TypedJsonReceiver : MonoBehaviour
    {
        #region Field

        [SerializeField] private TypedJsonEvent onReceive;

        #endregion Field

        #region Method

        public void OnReceive(byte[] data)
        {
            var typedJson  = Encoding.UTF8.GetString(data);
            var commaIndex = typedJson.IndexOf(',');

            if (commaIndex == -1)
            {
                return;
            }

            var type = Type.GetType(typedJson[..commaIndex]);
            var json = typedJson[(commaIndex + 1)..];

            onReceive.Invoke(type, json);
        }

        #endregion Method
    }
}

// NOTE:
// There is a way to register the types to be sent and received in advance.
// This method determines the type by checking whether deserialization succeeds.
// The downside of this method is that it cannot detect unexpected deserialization when it occurs.
// 
// public void OnReceive(byte[] data)
// {
//     var  json     = Encoding.UTF8.GetString(data);
//     Type jsonType = null;
//
//     foreach (var type in types)
//     {
//         try
//         {
//             JsonUtility.FromJson(json, type);
//             jsonType = type;
//         }
//         catch { continue; }
//     }
//
//     onReceive.Invoke(jsonType, json);
// }