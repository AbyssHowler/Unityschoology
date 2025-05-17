using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class DroneA1 : MonoBehaviour
{
    enum DroneState
    {
        IDLE,
        MOVE,
        ATTACK,
        DAMAGE,
        DIE
    }

    DroneState state = DroneState.IDLE;

    public float idleDelayTime = 2;
    float currentTime = 0;

    public float moveSpeed = 1;
    Transform tower;
    NavMeshAgent agent;

    public float attackRange = 3;
    public float attackDelayTime = 2;

    int hp = 3;

    Transform explosion;
    ParticleSystem expEffect;
    AudioSource expAudio;

    void Start()
    {
        tower = GameObject.Find("ForestTower_Red").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = false;
        agent.speed = moveSpeed;

        explosion = GameObject.Find("Explosion").transform;
        expEffect = explosion.GetComponent<ParticleSystem>();
        expAudio = explosion.GetComponent<AudioSource>();
    }

    void Update()
    {
        switch (state)
        {
            case DroneState.IDLE:
                Idle();
                break;
            case DroneState.MOVE:
                Move();
                break;
            case DroneState.ATTACK:
                Attack();
                break;
            case DroneState.DAMAGE:
                //Damage();  // �ڷ�ƾ���� ���� ��
                break;
            case DroneState.DIE:
                // Die();  // ����� ��� �� ��
                break;
        }
    }

    void Idle()
    {
        currentTime += Time.deltaTime;
        if (currentTime > idleDelayTime)
        {
            state = DroneState.MOVE;
            agent.enabled = true;
        }
    }

    void Move()
    {
        agent.SetDestination(tower.position);

        if (Vector3.Distance(transform.position, tower.position) < attackRange)
        {
            state = DroneState.ATTACK;
            agent.enabled = false;

            // �����̰� �ٷ� ���̸� �ٷ� ���ݵǹǷ�, ���� �ֱ� ���� �ʱ�ȭ
            currentTime = attackDelayTime;
        }
    }

    void Attack()
    {
        currentTime += Time.deltaTime;
        if (currentTime > attackDelayTime)
        {
            // attack
            Tower.Instance.HP--;
            currentTime = 0;
        }
    }

    public void OnDamageProcess()
    {
        hp--;

        if (hp > 0)
        {
            state = DroneState.DAMAGE;
            StopAllCoroutines();
            StartCoroutine(Damage());
        }
        else
        {
            // ���� ��ġ ���� �� ����
            explosion.position = transform.position;
            expEffect.Play();
            expAudio.Play();

            Destroy(gameObject);
        }
    }

    IEnumerator Damage()
    {
        agent.enabled = false;
        Material mat = GetComponent<Renderer>().material;
        Color originalColor = mat.color;

        mat.SetColor("_BaseColor", Color.red); // ���������� ������
        yield return new WaitForSeconds(0.1f);
        mat.SetColor("_BaseColor", originalColor); // ���� ���� ����

        state = DroneState.IDLE;
        currentTime = 0;
    }

    //void Die() { }
}
