using UnityEngine;

namespace Assets.Common.ObjectPooling
{
    public interface IPoolableObject
    {
        void ResetPoolObject(Vector3 position, Quaternion rotation);
    }
}
