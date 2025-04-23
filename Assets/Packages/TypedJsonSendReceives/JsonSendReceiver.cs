using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace TypedJsonSendReceives
{
    public class JsonSendReceiver : MonoBehaviour
    {
        private Type[] types;

        #region Field

        [SerializeField] private TypedJsonEvent onReceive;

        #endregion Field

        #region Method

        public void OnReceive(byte[] data)
        {
            var  json     = Encoding.UTF8.GetString(data);
            Type jsonType = null;

            foreach (var type in types)
            {
                try
                {
                    JsonUtility.FromJson(json, type);
                    jsonType = type;
                }
                catch { continue; }
            }

            onReceive.Invoke(jsonType, json);
        }

        #endregion Method
    }
}