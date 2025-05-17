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
                //Damage();  // 코루틴으로 실행 중
                break;
            case DroneState.DIE:
                // Die();  // 현재는 사용 안 함
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

            // 딜레이가 바로 쌓이면 바로 공격되므로, 지연 주기 위해 초기화
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
            // 폭발 위치 설정 및 실행
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

        mat.SetColor("_BaseColor", Color.red); // 빨간색으로 깜빡임
        yield return new WaitForSeconds(0.1f);
        mat.SetColor("_BaseColor", originalColor); // 원래 색상 복구

        state = DroneState.IDLE;
        currentTime = 0;
    }

    //void Die() { }
}
