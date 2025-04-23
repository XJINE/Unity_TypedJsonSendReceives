using System.Net;
using System.Text;
using UnityEngine;

namespace TypedJsonSendReceives
{
    public abstract class AbstractTypedJsonSender : MonoBehaviour
    {
        #region Class

        [System.Serializable] public class Target
        {
            public bool           enabled;
            public IPEndPointInfo iPEndPoint;
        }        

        #endregion Class

        #region Field

        [SerializeField] private Target[]       targets;
        [SerializeField] private TypedJsonEvent onSend;

        #endregion Field

        #region Property

        public Target[] Targets => targets;

        #endregion Property

        #region Method

        public void Send<T>(T instance)
        {
            var json       = GetJsonString(instance);
            var typeString = typeof(T).ToString();
            var jsonBytes  = Encoding.UTF8.GetBytes(typeString + ',' + json);

            foreach (var target in targets)
            {
                if (target.enabled)
                {
                    Send(target.iPEndPoint.IPEndPoint, jsonBytes);
                }
            }

            onSend.Invoke(typeof(T), json);
        }

        public void EnableAllTargets () { foreach (var target in targets) { target.enabled = true;            } }
        public void DisableAllTargets() { foreach (var target in targets) { target.enabled = false;           } }
        public void ToggleAllTargets () { foreach (var target in targets) { target.enabled = !target.enabled; } }

        protected abstract string GetJsonString<T>(T instance);
        protected abstract void   Send(IPEndPoint iPEndPoint, byte[] data);

        #endregion Method
    }
}