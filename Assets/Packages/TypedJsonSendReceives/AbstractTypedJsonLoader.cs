using System;
using UnityEngine;

namespace TypedJsonSendReceives
{
    public abstract class AbstractTypedJsonLoader<T> : MonoBehaviour
    {
        public void OnReceive(Type type, string json)
        {
            if (typeof(T) != type)
            {
                return;
            }

            LoadJson(json);
        }

        protected abstract void LoadJson(string json);
    }
}