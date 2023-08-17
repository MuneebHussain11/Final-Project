using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("spawnBullet", 2.0f, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void spawnBullet()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
