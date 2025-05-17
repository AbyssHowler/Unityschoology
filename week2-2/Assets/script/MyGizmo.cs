using UnityEngine;

public class MyGizmo : MonoBehaviour
{
    public Color color = Color.green;
    public float radius = 0.02f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
