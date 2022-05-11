using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerupType
{
    ENERGY,
    SHIELD,
    BULLET
};

public class Powerup : MonoBehaviour
{
    private PowerupType type = PowerupType.ENERGY;

    public List<Sprite> sprites;

    public float LinearSpeed = 3f;
    public float Amplitude = 0.5f;
    public float Frequency = 20f;

    private void Start()
    {
        float R = Random.Range(0, 101);
        if (R < 30)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            type = PowerupType.ENERGY;
        }
        else if(R < 50)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            type = PowerupType.SHIELD;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
            type = PowerupType.BULLET;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.x -= LinearSpeed * Time.deltaTime;
        position.y = Amplitude * Mathf.Sin(Frequency * Time.time);
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<Bullet>().type == BulletType.PLAYER) ||
            collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Bullet"))
                Destroy(collision.gameObject);
            switch(type)
            {
                case PowerupType.ENERGY:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().RestoreEnergy();
                    break;
                case PowerupType.SHIELD:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ShieldUp();
                    break;
                case PowerupType.BULLET:
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().ChangeBullet();
                    break;
            }
            Destroy(gameObject);
        }
    }
}
