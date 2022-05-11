using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spaceship : MonoBehaviour
{
    protected float lastShot = 0f;

    public BulletType bulletType;
    public List<GameObject> Bullets = new List<GameObject>();
    protected int currentBullet = 0;

    public float EnergyLevel = 10f;
    public GameObject Explosion;

    public GameObject ShieldRef;

    virtual protected void Start()
    {
        ShieldDown();
    }

    abstract public void Die();

    public void TakeDamage(float amount)
    {
        EnergyLevel -= amount;
        if (EnergyLevel <= 0f)
        {
            Die();
        }
    }
    public void ShieldUp()
    {
        ShieldRef.GetComponent<Shield>().Reset();
        ShieldRef.SetActive(true);
    }
    public void ShieldDown()
    {
        ShieldRef.SetActive(false);
    }
}
