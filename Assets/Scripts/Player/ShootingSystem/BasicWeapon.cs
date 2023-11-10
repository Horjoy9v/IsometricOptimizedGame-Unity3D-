using UnityEngine;
using System.Threading.Tasks;

public class BasicWeapon : Weapon
{
    private float timeSinceLastShot = 0f;
    public override async void Shoot(Transform firePoint, byte bulletCount, float spreadAngle)
    {
        timeSinceLastShot += Time.deltaTime;

        if (timeSinceLastShot >= fireRate)
        {
            for (byte i = 0; i < bulletCount; i++)
            {
                float angle = i * spreadAngle - (spreadAngle * (bulletCount - 1) / 2);
                Quaternion rotation = firePoint.rotation * Quaternion.Euler(angle, 0, 0);

                // Отримуємо кулю з пулу асинхронно
                GameObject bullet = await BulletPool.instance.GetBulletAsync();

                // Встановлюємо позицію кулі
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = rotation;
            }

            timeSinceLastShot = 0f;
        }
    }
}
