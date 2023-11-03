using UnityEngine;

public class FasterFireRateDecorator : WeaponDecorator
{
    private float fireRateMultiplier = 0.5f;
    private byte accelerationDuration = 3; // Длительность ускорения в секундах
    private bool isActive = false;
    private float activationTime = 0f;

    public FasterFireRateDecorator(Weapon weaponToDecorate) : base(weaponToDecorate) { }

    public void ActivateAcceleration()
    {
        if (!isActive)
        {
            weapon.SetFireRate(weapon.GetFireRate() * fireRateMultiplier);
            isActive = true;
            activationTime = Time.time;
        }
    }

    public override void Shoot(Transform firePoint, byte bulletCount, float spreadAngle)
    {
        base.Shoot(firePoint, bulletCount, spreadAngle);

        if (isActive && Time.time - activationTime >= accelerationDuration)
        {
            DeactivateAcceleration();
        }
    }

    private void DeactivateAcceleration()
    {
        weapon.SetFireRate(weapon.GetFireRate() / fireRateMultiplier);
        isActive = false;
    }
}
