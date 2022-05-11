using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    PLAYER,
    UFO
};

public class Bullet : MonoBehaviour
{
    public float Speed = 5f;
    public float Power = 2.5f;
    public Vector3 direction = new Vector3(1f, 0f, 0f);
    public BulletType type = BulletType.PLAYER;

    public GameObject Hit;

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position += direction.normalized * Speed * Time.deltaTime;
        transform.position = position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("UFO") && type == BulletType.PLAYER) ||
            (collision.gameObject.CompareTag("Player") && type == BulletType.UFO))
        {
            collision.gameObject.GetComponent<Spaceship>().TakeDamage(Power);
            Instantiate(Hit, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Bullet") && type != collision.gameObject.GetComponent<Bullet>().type)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
