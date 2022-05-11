using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : Spaceship
{
    public Vector2 MaxSpeed = new Vector2(0.5f, 1.0f);
    public Vector2 YBoundary = new Vector2(-4.5f, 4.5f);
    public float UpdateRate = 0.75f;
    float lastUpdate;

    public Vector2 ShootingRange = new Vector2(1f, 5f);
    public Transform cannon;

    public int Points = 10;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        lastUpdate = UpdateRate;
        lastShot = Random.Range(ShootingRange.x, ShootingRange.y);
    }

    // Update is called once per frame
    private void Update()
    {
        lastUpdate -= Time.deltaTime;
        if (lastUpdate <= 0f)
        {
            Vector3 position = transform.position;
            position += new Vector3(Random.Range(0f, -MaxSpeed.x), Random.Range(-MaxSpeed.y, MaxSpeed.y), 0f);
            position.y = Mathf.Clamp(position.y, YBoundary.x, YBoundary.y);
            transform.position = position;

            lastUpdate = UpdateRate;
        }

        lastShot -= Time.deltaTime;
        if (lastShot <= 0f)
        {
            Fire();
            lastShot = Random.Range(ShootingRange.x, ShootingRange.y);
        }
    }

    void Fire()
    {
        GameObject bullet = null;
        bullet = Instantiate(Bullets[0], cannon.position, cannon.rotation);
        bullet.GetComponent<Bullet>().direction = new Vector3(-1.0f, 0f, 0f);
        bullet.GetComponent<Bullet>().type = bulletType;
    }

    override public void Die()
    {
        GameObject.Find("HUD").GetComponent<HUD>().AddPoints(Points);
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
