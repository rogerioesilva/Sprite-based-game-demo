using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int MaxHits = 5;
    private int Hits;

    private void Start()
    {
        Reset();
    }

    public void Reset()
    {
        Hits = 0;
    }

    private void Update()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color color = renderer.color;
        color.a = (float)(MaxHits - Hits) / MaxHits;
        renderer.color = color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && collision.gameObject.GetComponent<Bullet>().type != gameObject.GetComponentInParent<Spaceship>().bulletType)
        {
            Hits++;
            Instantiate(collision.gameObject.GetComponent<Bullet>().Hit, transform.position, transform.rotation);
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.CompareTag("UFO"))
        {
            Hits++;
            Instantiate(collision.gameObject.GetComponent<Spaceship>().Explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);
        }

        if(Hits >= MaxHits)
        {
            gameObject.GetComponentInParent<Spaceship>().ShieldDown();
        }
    }
}
