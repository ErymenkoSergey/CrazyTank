using UnityEngine;

namespace CrazyTank.Interface
{
    public interface IControllable
    {
        bool StartGame();
        void SetPalyer(GameObject player);
        IMoveble GetMoveble();
        IShoot GetShoot();
        void SetPause(bool pause);
        void RestartGame();
        void QuitGame();
    }
}