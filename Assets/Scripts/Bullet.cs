using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float rightDestroyPosition;
    public float leftDestroyPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * 10 * Time.deltaTime);
        if(transform.position.x < leftDestroyPosition || transform.position.x > rightDestroyPosition)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollisionBlock"))
        {
            Destroy(this.gameObject);
        }
    }
}