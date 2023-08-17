using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuistionBlock : MonoBehaviour
{
    public GameObject coinPrefab;
    public bool disable = false;
    public GameObject emptyblock;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 spawnPosition = transform.position + new Vector3 (0f, 2f, 0);
        if(collision.gameObject.CompareTag("Player") && disable == false)
        {
            Vector3 normal = collision.contacts[0].normal;
            if (normal.y > 0.7f) // Check if collision is from above
            {
                

                Instantiate(coinPrefab, spawnPosition, coinPrefab.transform.rotation);
                Instantiate(emptyblock, transform.position, emptyblock.transform.rotation);
                disable = true;
                Destroy(this.gameObject);

            }   
            
        }
        
    }
}
