using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public GameObject[] bulletPrefab;
    public byte poolSize = 10;
    private Queue<GameObject> bullets;
    private List<TrailRenderer> bulletTrailRenderers;
    public static BulletPool instance;

    private async void Awake()
    {
        instance = this;
        await InitializePoolAsync();
    }

    private async Task InitializePoolAsync()
    {
        bullets = new Queue<GameObject>();
        bulletTrailRenderers = new List<TrailRenderer>();

        for (byte i = 0; i < poolSize; i++)
        {
            GameObject bullet = await CreateBulletAsync();
            bullets.Enqueue(bullet);
        }
    }
    private async Task<GameObject> CreateBulletAsync()
    {
        GameObject bullet = Instantiate(bulletPrefab[SetPlayerIndex.CharacterIndex - 1]);
        bullet.SetActive(false);

        TrailRenderer trailRenderer = bullet.GetComponentInChildren<TrailRenderer>();
        bulletTrailRenderers.Add(trailRenderer);

        await Task.Yield();
        return bullet;
    }
    public async Task<GameObject> GetBulletAsync()
    {
        if (bullets.Count > 0)
        {
            GameObject bullet = bullets.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            GameObject bullet = await CreateBulletAsync();
            bullet.SetActive(true);
            return bullet;
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);

        foreach (TrailRenderer trailRenderer in bulletTrailRenderers)
        {
            trailRenderer.Clear();
        }

        bullets.Enqueue(bullet);
    }
}
