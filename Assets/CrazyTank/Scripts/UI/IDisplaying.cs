using UnityEngine;

namespace CrazyTank.Interface
{
    public interface IDisplaying
    {
        void SetControllable(IControllable controllable);
        void ChangeUIGun(ref Texture2D image, ref string name);
        void GameOver();
    }
}