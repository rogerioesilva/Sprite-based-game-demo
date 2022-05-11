using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScrollingDirection
{
    HORIZONTAL,
    VERTICAL
};

public class RotatingBackground : MonoBehaviour
{
    public ScrollingDirection direction = ScrollingDirection.HORIZONTAL;
    [SerializeField]
    private float speed;
    private Renderer myRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(direction)
        {
            case ScrollingDirection.HORIZONTAL:
                myRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
                break;
            case ScrollingDirection.VERTICAL:
                myRenderer.material.mainTextureOffset += new Vector2(0f, speed * Time.deltaTime);
                break;
        }
    }
}
