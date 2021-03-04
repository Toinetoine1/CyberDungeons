
public class HealthSystem
{
    private float health;
    public float Maxhealth;

    public HealthSystem(float maxhealth)
    {
        Maxhealth = maxhealth;
        health = maxhealth;
    }

    public float gethealth()
    {
        return health;
    }

    public float getHealthPercentage()
    {
        return health / Maxhealth;
    }

    public void damage(float damageCount)
    {
        health -= damageCount;
        if (health < 0)
            health = 0;
    }

    public void heal (float healAmount)
    {
        health += healAmount;
        if (health > Maxhealth)
            health = Maxhealth;
    }
}
