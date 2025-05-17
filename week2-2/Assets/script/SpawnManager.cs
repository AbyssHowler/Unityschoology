using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnPoint = new Transform[5];
    public GameObject enemy;
    float spawnTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTime = 3.0f;
        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator Spawn()
    {
        while (spawnTime > 0)
        {
            int point = Random.Range(0, spawnPoint.Length); // 배열 크기에 맞게 수정
            Instantiate(enemy, spawnPoint[point].position, spawnPoint[point].rotation);
            yield return new WaitForSeconds(spawnTime);
            spawnTime -= 0.01f;
        }

        yield return null;
    }
}
