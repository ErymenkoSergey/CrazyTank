using CrazyTank.Data;
using CrazyTank.Interface;
using UnityEngine;

namespace CrazyTank.Characters
{
    public abstract class BaseCharacter : MonoBehaviour, IDamageble
    {
        protected bool gameOn { get; set; }
        protected CharacterType characterType { get; set; }
        protected ISpawning iSpawner { get; private set; }
        protected IFollowing iFollowing { get; set; }
        protected IArmed iArmed { get; set; }
        public float Healts { get; private set; }
        public float Armor { get; private set; }
        public float Speed { get; private set; }
        public float Damage { get; private set; }
        public int Index { get; private set; }

        public void SetData(PlayerConfiguration configuration, int index, ISpawning generated)
        {
            Healts = configuration.Health;
            Armor = configuration.Armor;
            Speed = configuration.MoveSpeed;
            Damage = configuration.Damage;
            Index = index;
            iSpawner = generated;

            SeparationActions();
        }

        private void SeparationActions()
        {
            if (characterType == CharacterType.Player)
            {
                iSpawner.SetPalyer(this.gameObject);
                SetPositionTarget();
            }
            if (characterType == CharacterType.Enemy)
            {
                CheckPositionTarget();
            }
        }

        private void SetPositionTarget()
        {
            iArmed.SetStatusGame(true);
        }

        private void CheckPositionTarget()
        {
            iFollowing.ChangeStatus(true);
        }

        public void TakeDamage(float value)
        {
            Healts = Healts - (value * Armor);

            if (Healts <= 0f)
            {
                if (characterType == CharacterType.Enemy)
                {
                    iSpawner.Respawn(Index);
                    Destroy(this.gameObject);
                }
                if (characterType == CharacterType.Player)
                {
                    iSpawner.GameOver();
                }
            }
        }
    }
}