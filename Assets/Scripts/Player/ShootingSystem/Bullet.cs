using UnityEngine;

public class Bullet : MonoBehaviour
{
    public byte speed = 15;
    //public byte maxDistance = 12;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        transform.position += transform.forward * speed * Time.deltaTime;

        if (timer >= 1f) // Перевірка, чи куля летить принаймні 1 секунду
        {
            BulletPool.instance.ReturnBullet(gameObject);
            timer = 0f; // Скидання таймера
        }
    }
}
