using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : MonoBehaviour
{
    //variables
    public float speed; 
    public float seen; 
    private Transform player;

    //shooting variables
    public float fireRate = 1;
    private float nextFireTime;
    public float shootingRange;
    public GameObject bullet; 
    public GameObject bulletPos; 

    Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);
        if( distance < seen && distance > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime );
        }
        else if (distance <= shootingRange && nextFireTime < Time.time)
        {
            Instantiate(bullet, bulletPos.transform.position, Quaternion.identity);
            nextFireTime = fireRate + Time.time;
        }
        else if (distance > seen){
            transform.position = Vector2.MoveTowards(this.transform.position, startPosition, speed * Time.deltaTime);
    }
}
    
    private void onDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, seen);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }

    void OnTrigger(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}