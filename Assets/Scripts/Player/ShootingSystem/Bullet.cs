using UnityEngine;

public class Bullet : MonoBehaviour
{
    public byte speed = 15;
    private float timer = 0f;
    public float maxLifeTime;

    private void Update()
    {
        timer += Time.deltaTime;
        transform.position += transform.forward * speed * Time.deltaTime;

        if (timer >= maxLifeTime)
        {
            BulletPool.instance.ReturnBullet(gameObject);
            timer = 0f;
        }
    }
}
