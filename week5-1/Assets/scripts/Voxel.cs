using UnityEngine;

public class Voxel : MonoBehaviour
{
    public float speed = 5.0f;
    public float destroyTime = 3.0f;
    float currentTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    void OnEnable()
    {
        currentTime = 0;
        Vector3 direction = Random.insideUnitSphere;
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = direction * speed;

        //Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > destroyTime)
        {
            gameObject.SetActive(false);
            Floor.voxelPool.Add(gameObject);
        }
    }
}
