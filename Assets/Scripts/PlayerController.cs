using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CannonType
{
    FRONT,
    SIDE,
    BOTH
};

public class PlayerController : Spaceship
{
    public float Speed = 1.0f;
    public Vector2 XBoundary = new Vector2(-8.5f, 0f);
    public Vector2 YBoundary = new Vector2(-4.5f, 4.5f);

    public Transform FrontCannonRef;
    public Transform SideCannon1Ref;
    public Transform SideCannon2Ref;

    public float ShootingRate = 0.2f;
    public CannonType cannonType = CannonType.FRONT;

    // Update is called once per frame
    private void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");

        Vector3 position = transform.position;
        position += new Vector3(horizontalAxis * Speed * Time.deltaTime, verticalAxis * Speed * Time.deltaTime, 0f);
        position.x = Mathf.Clamp(position.x, XBoundary.x, XBoundary.y);
        position.y = Mathf.Clamp(position.y, YBoundary.x, YBoundary.y);
        transform.position = position;

        lastShot += Time.deltaTime;
        if(lastShot >= ShootingRate * currentBullet + ShootingRate)
        {
            GameObject bullet = null;

            if (cannonType == CannonType.FRONT || cannonType == CannonType.BOTH)
            {
                bullet = Instantiate(Bullets[currentBullet], FrontCannonRef.position, FrontCannonRef.rotation);
            }

            if (cannonType == CannonType.SIDE || cannonType == CannonType.BOTH)
            {
                bullet = Instantiate(Bullets[currentBullet], SideCannon1Ref.position, SideCannon1Ref.rotation);
                bullet.GetComponent<Bullet>().direction = new Vector3(1.0f, 0.3f, 0f);
                bullet = Instantiate(Bullets[currentBullet], SideCannon2Ref.position, SideCannon2Ref.rotation);
                bullet.GetComponent<Bullet>().direction = new Vector3(1.0f, -0.3f, 0f);
            }

            if(bullet)
                bullet.GetComponent<Bullet>().type = bulletType;

            lastShot = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("UFO"))
        {
            TakeDamage(EnergyLevel);
        }
    }

    override public void Die()
    {
        GameObject.Find("HUD").GetComponent<HUD>().GameOver();
        Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void RestoreEnergy()
    {
        EnergyLevel = GameObject.Find("HUD").GetComponent<HUD>().PlayerEnergyLevel.maxValue;
    }
    public void ChangeBullet()
    {
        switch(cannonType)
        {
            case CannonType.FRONT: cannonType = CannonType.SIDE; break;
            case CannonType.SIDE: cannonType = CannonType.BOTH; break;
            case CannonType.BOTH: cannonType = CannonType.FRONT; break;
        }
        currentBullet = (currentBullet + 1) % Bullets.Count;
    }
}
