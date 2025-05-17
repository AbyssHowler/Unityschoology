using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float range = 5;
    public Transform explosion;
    public ParticleSystem expEffect;
    public AudioSource expAudio;

    // 1. 풀 매니저 참조
    private SimpleObjectPool pool;

    void Awake()
    {
        pool = FindObjectOfType<SimpleObjectPool>();
    }

    void Start()
    {
        if (explosion == null)
            explosion = GameObject.Find("Explosion").transform;
        if (expEffect == null)
            expEffect = explosion.GetComponent<ParticleSystem>();
        if (expAudio == null)
            expAudio = explosion.GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Castle") || coll.gameObject.CompareTag("Bomb"))
            return;

        int layerMask = 1 << LayerMask.NameToLayer("Drone");
        Collider[] drones = Physics.OverlapSphere(transform.position, range, layerMask);
        foreach (Collider drone in drones)
            Destroy(drone.gameObject);

        explosion.position = transform.position;
        expEffect.Play();
        expAudio.Play();

        // 2. 직접 풀로 반납
        pool.Despawn(gameObject);
    }
}
