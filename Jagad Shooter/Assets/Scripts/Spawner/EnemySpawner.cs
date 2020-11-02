using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnInterval = 2f;
    public float addSpeed = .1f;
    public float increaseSpeedInterval = 10f;
    public float time;

    public GameObject[] objectToSpawn;
    public GameObject quad;
    public float screenX;
    public bool canSpawn = true;

    private void Start()
    {
        time = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn)
        {
            SpawnObject();
        }
    }

    void SpawnObject()
    {
        canSpawn = false;
        int objectSpawned = Random.Range(0, objectToSpawn.Length);
        MeshCollider mesh = quad.GetComponent<MeshCollider>();
        screenX = Random.Range(mesh.bounds.min.x, mesh.bounds.max.x);
        
        if (screenX + 
            (objectToSpawn[objectSpawned].GetComponent<BoxCollider2D>().size.x / 2 ) >= 
            Camera.main.orthographicSize)
        {
            screenX -= (objectToSpawn[objectSpawned].GetComponent<BoxCollider2D>().size.x / 2);
        }
        else if (screenX +
            (objectToSpawn[objectSpawned].GetComponent<BoxCollider2D>().size.x / 2) <=
            0)
        {
            screenX += (objectToSpawn[objectSpawned].GetComponent<BoxCollider2D>().size.x / 2);
        }
        Vector2 pos = new Vector2(screenX, Camera.main.orthographicSize);
        GameObject enemy = Instantiate(objectToSpawn[objectSpawned], pos, objectToSpawn[objectSpawned].transform.rotation);
        if (Time.time - time >= increaseSpeedInterval)
        {
            enemy.GetComponent<EnemyMovement>().speed += addSpeed;
        }
        StartCoroutine(WaitToSpawn());
    }

    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(spawnInterval);
        canSpawn = true;
    }
}
