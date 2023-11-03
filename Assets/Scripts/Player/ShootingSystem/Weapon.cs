using UnityEngine;

public abstract class Weapon
{
    public float fireRate = 1f;

    public abstract void Shoot(Transform firePoint, byte bulletCount, float spreadAngle);

    public float GetFireRate()
    {
        return fireRate;
    }

    public void SetFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }
}

