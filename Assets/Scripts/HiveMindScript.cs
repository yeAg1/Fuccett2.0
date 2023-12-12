using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMindScript : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnpoint;
    public float spawnCD = 5f;
    private float currentCD;
    public int maxSpawn = 5;
    private int currentSpawn = 0;   

    // Start is called before the first frame update
    void Start()
    {
        currentCD = spawnCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawn < maxSpawn) 
        {
            currentCD -= Time.deltaTime;
            if (currentCD <= 0)
            {
                Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
                currentSpawn++;
                currentCD = spawnCD;
            }
            
        }
        
    }
}
