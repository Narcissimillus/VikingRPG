using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    Animator animator;
    Slider healthBar;
    public float radius = 30f;
    public float attackDistanceThreshold = 1f;
    public Renderer[] renderers;
    bool tookHit = false;
    float distance;
    public delegate void EnemyManager(string enemyName);
    public EnemyManager onDie;
    float dissolveScale = 0;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<Slider>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(Patrol(10f));
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = 2;
        float speedh = transform.InverseTransformVector(agent.velocity).magnitude / agent.speed;
        animator.SetFloat("speedh", speedh, 0.1f, Time.deltaTime);

        distance = Vector3.Distance(player.position, transform.position);
        Vector3 targetDir = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(targetDir, transform.forward);

        // Attack
        if (agent.enabled && player.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled == true && 
            ((distance <= radius && angleToPlayer >= -90 && angleToPlayer <= 90) || tookHit == true))
        {
            StopCoroutine(Patrol(10f));
            agent.speed = 3.5f;
            agent.SetDestination(player.position);
            attackDistanceThreshold = 1 + Mathf.Sin(Time.time) * .5f;
            if (distance <= attackDistanceThreshold)
            {
                animator.SetTrigger("attack");
                FindObjectOfType<AudioManager>().Play("AttackMiss");
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(targetDir.normalized.x, 0, targetDir.normalized.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }

        // Reset time since last hit
        animator.SetFloat("timeSinceLastHit", animator.GetFloat("timeSinceLastHit") + Time.deltaTime);
        var stateNfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateNfo.IsName("TakeHit"))
        {
            StopCoroutine(ResetTakeHit());
            agent.speed = 0f;
            tookHit = true;
            StartCoroutine(ResetTakeHit());
        }
        if (stateNfo.IsName("Fall"))
        {
            if (onDie != null)
            {
                onDie.Invoke(this.name); //notifica despre  modificare
            }
            agent.enabled = false;
            transform.rotation = Quaternion.identity;
            StartCoroutine(WaitDeath(5f));
        }
        healthBar.value = animator.GetInteger("HP");
    }

    IEnumerator WaitDeath(float t)
    {
        yield return new WaitForSeconds(t);
        if(dissolveScale > 0 && dissolveScale < 0.05f)
            AudioManager.instance.Play("Dissolve");
        dissolveScale = Mathf.Lerp(dissolveScale, 1, 0.5f * Time.deltaTime);
        foreach(Renderer rends in renderers)
        {
            rends.material.SetFloat("Vector1_4099437C", dissolveScale);
        }
        gameObject.GetComponentInChildren<Collider>().enabled = false;
        Destroy(gameObject, 3f);
    }

    IEnumerator Patrol(float t, int m = 0)
    {
        distance = Vector3.Distance(player.position, transform.position);
        if (distance > radius && tookHit == false && agent.enabled == true)
        {
            switch (m)
            {
                case 0:
                    agent.destination = new Vector3(-0.68f + 8, 0.64f, -48.2f + 8);
                    break;
                case 1:
                    agent.destination = new Vector3(transform.position.x + 8, transform.position.y, transform.position.z - 8);
                    break;
                case 2:
                    agent.destination = new Vector3(-0.68f, 0.64f, -48.2f);
                    break;
            }
        }
        m = (m + 1) % 3;
        yield return new WaitForSeconds(t);
        yield return StartCoroutine(Patrol(10f, m));
    }

    IEnumerator ResetTakeHit()
    {
        yield return new WaitForSeconds(4.5f);
        tookHit = false;
        StartCoroutine(Patrol(10f));
    }
}
