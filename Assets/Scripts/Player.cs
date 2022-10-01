using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    private int spriteIndex;    // init??

    private float bottomEdge;

    float smooth = 1.0f;
    float harsh = 7.5f;
    public float accel = 1.0f;
    //float tiltAngle = 60.0f;
    Quaternion target;

    [SerializeField] private Vector3 direction;
    public float gravity = -20f;
    public float strength = 10f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bottomEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        InvokeRepeating(nameof(AnimateSprite),0.105f, 0.105f);    // cycle every 1.5 secs
    }

    private void OnEnable()
    {
        Vector3 position = transform.position;
        position.y = 0f;
        transform.position = position;
        direction = Vector3.zero;
    }

    // Update is called once per frame
    // handle input
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            direction = Vector3.up * strength;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                direction = Vector3.up * strength;
            }
        }
        
        else if ( direction.y < 0 )
        {
            direction.y -= 0.028f;          // fall faster
            if ( direction.y < -4.0 ) {
                target = Quaternion.Euler(0, 0, -90.0f);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth * accel);
                accel += 0.023f;
            }

        }
        else
        {
            accel = 1.0f;
            target = Quaternion.Euler(0, 0, 10.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * harsh);
        }
        direction.y += gravity * Time.deltaTime;
        transform.position += direction * Time.deltaTime;

        //if (transform.position.y < bottomEdge)
        if ( (Camera.main.WorldToViewportPoint(transform.position).y < 0) || (Camera.main.WorldToViewportPoint(transform.position).y > 1.0) )
        {
            FindObjectOfType<GameManager>().GameOver();
        }
    }

    private void AnimateSprite()
    {
        spriteIndex++;
        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        spriteRenderer.sprite = sprites[spriteIndex];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( (other.gameObject.tag == "Obstacle"))
        {
            FindObjectOfType<GameManager>().GameOver();       // searches entire scene, expensive function
        }
        else if ( other.gameObject.tag == "Scoring")
        {
            FindObjectOfType<GameManager>().IncreaseScoreByOne();
        }

    }
}
