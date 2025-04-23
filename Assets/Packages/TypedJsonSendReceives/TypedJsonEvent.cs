using System;
using UnityEngine.Events;

namespace TypedJsonSendReceives
{
    [Serializable] public class TypedJsonEvent : UnityEvent<Type, string> { }
}