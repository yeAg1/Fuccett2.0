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

    // Start is called before the first frame update
    void Start()
    {
        currentCD = spawnCD;
    }

    // Update is called once per frame
    void Update()
    {
        currentCD -= Time.deltaTime;
        if(currentCD <= 0 )
        {
            Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
            currentCD = spawnCD;
        }
    }
}
