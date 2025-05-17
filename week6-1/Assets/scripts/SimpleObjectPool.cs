using UnityEngine;
using System.Collections.Generic;

public class SimpleObjectPool : MonoBehaviour
{
    [Tooltip("�� Ǯ���� ����� ��ź ������")]
    public GameObject bombPrefab;
    [Tooltip("Ǯ�� �̸� ������ ��ź ����")]
    public int poolSize = 3;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var obj = Instantiate(bombPrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    /// <summary>
    /// Ǯ���� �ϳ� ���� Ȱ��ȭ�ϰ� ���� �ӵ� �ʱ�ȭ
    /// </summary>
    public GameObject Spawn(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            Debug.LogWarning("SimpleObjectPool: ��� ������ ��ź�� �����ϴ�.");
            return null;
        }

        var obj = pool.Dequeue();
        obj.transform.SetPositionAndRotation(position, rotation);

        // Rigidbody�� ������ �ӵ���ȸ�� �ʱ�ȭ
        var rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// ��ź���� ȣ��Ǯ�� �ݳ��ϰ� ��� �罺��
    /// </summary>
    public void Despawn(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);

        // ��� �罺��
        Spawn(transform.position, transform.rotation);
    }
}
