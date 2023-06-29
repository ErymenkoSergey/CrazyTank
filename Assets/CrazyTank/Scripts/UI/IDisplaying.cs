using UnityEngine;

namespace CrazyTank.Interface
{
    public interface IDisplaying
    {
        void SetControllable(IControllable controllable);
        void ChangeUIGun(Texture2D image, string name);
        void GameOver();
    }
}