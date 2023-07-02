using CrazyTank.Data;

public interface IDamageble
{
    void TakeDamage(float value);
    CharacterType GetCharacterType();
}
