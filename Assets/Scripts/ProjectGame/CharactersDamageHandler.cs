using ProjectCore;

namespace ProjectGame
{
    [AllowCreateInstance]
    public class CharactersDamageHandler: SceneSingleton<CharactersDamageHandler>
    {
        public void DealDamage(int damage, Character target)
        {
            target.Health.Set(target.Health.CurrentValue - damage);
        }
    }
}