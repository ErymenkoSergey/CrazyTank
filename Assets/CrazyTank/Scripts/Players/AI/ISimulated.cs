using UnityEngine;

namespace CrazyTank.Interface
{
    public interface ISimulated
    {
        void SetSpeed(float speed);
        void SetTarget(Vector3 target);
        void SetStatusAtack(bool isOn);
    }
}