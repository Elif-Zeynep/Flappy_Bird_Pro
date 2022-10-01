using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject prefab;
    // spawn rate & position variables
    public float spawnRate = 4.5f;  
    public float minY = -2f;
    public float maxY = 2f;

    // sizing variables
    public float minScale = -0.3f;
    public float maxScale = 0.3f;
    private Vector3 scaleChange;
    private float rand;

    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);   // will be called every spawnRate unit of time
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    private void Spawn()
    {
        GameObject clouds = Instantiate(prefab, transform.position, Quaternion.identity);
        clouds.transform.position += Vector3.up * Random.Range(minY, maxY);

        rand = Random.Range(minScale, maxScale);
        scaleChange = new Vector3( rand , rand, 0.0f);
        clouds.transform.localScale += scaleChange;

    }
}
