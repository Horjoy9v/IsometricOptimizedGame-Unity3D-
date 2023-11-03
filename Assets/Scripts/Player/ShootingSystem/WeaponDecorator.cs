using UnityEngine;

public abstract class WeaponDecorator : Weapon
{
    protected Weapon weapon;

    public WeaponDecorator(Weapon weaponToDecorate)
    {
        weapon = weaponToDecorate;
    }

    public override void Shoot(Transform firePoint, byte bulletCount, float spreadAngle)
    {
        weapon.Shoot(firePoint, bulletCount, spreadAngle);
    }
}

