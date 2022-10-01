using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 1.5f;
    private float leftEdge;

    // for clouds, temporary fix, convert to polymorphism
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    public Color myColor;
    private float newTransparency;


    private void Awake()
    {
            spriteRenderer = GetComponent<SpriteRenderer>();
            pickColor();
            pickSpeed();
    }

    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    private void pickColor()
    {
        newTransparency = Random.Range(0.2f, 0.9f);

        myColor = new Color(1.0f, 1.0f, 1.0f, newTransparency);
        spriteRenderer.color = myColor;

    }

    private void pickSpeed()
    {
        speed = Random.Range(1.0f, 2.0f);
    }
}
