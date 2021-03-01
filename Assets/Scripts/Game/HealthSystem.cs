
public class HealthSystem
{
    private int health;
    public int Maxhealth;

    public HealthSystem(int maxhealth)
    {
        Maxhealth = maxhealth;
        health = maxhealth;
    }

    public int gethealth()
    {
        return health;
    }

    public float getHealthPercentage()
    {
        return (float) health / Maxhealth;
    }

    public void damage(int damageCount)
    {
        health -= damageCount;
        if (health < 0)
            health = 0;
    }

    public void heal (int healAmount)
    {
        health += healAmount;
        if (health > Maxhealth)
            health = Maxhealth;
    }
}
