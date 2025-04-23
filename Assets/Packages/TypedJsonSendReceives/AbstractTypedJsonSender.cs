using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

namespace TypedJsonSendReceives
{
    public abstract class AbstractTypedJsonSender : MonoBehaviour
    {
        #region Field

        [SerializeField] private TypedJsonEvent onSend;

        #endregion Field

        #region Method

        public void Send<T>(T instance, IEnumerable<IPEndPoint> targets)
        {
            var json = GetJsonString(instance);
            var data = Encoding.UTF8.GetBytes(typeof(T).ToString() + ',' + json);

            foreach (var target in targets)
            {
                Send(target, data);
            }

            onSend.Invoke(typeof(T), json);
        }

        protected abstract string GetJsonString<T>(T instance);
        protected abstract void   Send(IPEndPoint iPEndPoint, byte[] data);

        #endregion Method
    }
}