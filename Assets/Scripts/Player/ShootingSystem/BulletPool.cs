using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject bulletPrefab;
    public byte poolSize = 10;
    private Queue<GameObject> bullets;
    public static BulletPool instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        bullets = new Queue<GameObject>();
        for (byte i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bullets.Enqueue(bullet);
        }
    }

    public GameObject GetBullet()
    {
        if (bullets.Count > 0)
        {
            GameObject bullet = bullets.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(true);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullets.Enqueue(bullet);
    }
}


