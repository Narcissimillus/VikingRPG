using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Transform arrow;
    public GameObject gameOverMenu;
    [HideInInspector] public Transform target = null;
    [HideInInspector] public Transform lastEnemyTarget = null;
    Animator animator;
    NavMeshAgent agent;
    Slider healthBar;
    public bool rightHandEquipped = false;
    public bool leftHandEquipped = false;
    public bool hasBowEquipped = false;
    public int protectionArmour = 0;
    [HideInInspector] public bool tookHit;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        healthBar = GetComponentInChildren<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.speed = 4.25f;
        float forward = transform.InverseTransformVector(agent.velocity).magnitude / agent.speed;
        animator.SetFloat("forward", forward, 0.1f, Time.deltaTime);
        if (Input.GetMouseButtonDown(0)) //la apasarea click stanga
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            target = null;
            Ray mouseClickRay = Camera.main.ScreenPointToRay(Input.mousePosition); //creaza o raza printr-un punct de pe ecran
            RaycastHit hit;

            if (Physics.Raycast(mouseClickRay, out hit))
            {
                //misca player-ul pana la destinatie
                agent.SetDestination(hit.point);
                agent.stoppingDistance = 1;
                DialogueManager.instance.panel.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(1)) //la apasarea click dreapta
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Ray mouseClickRay = Camera.main.ScreenPointToRay(Input.mousePosition); //creaza o raza printr-un punct de pe ecran
            RaycastHit hit;

            if (Physics.Raycast(mouseClickRay, out hit))
            {
                if (hit.collider.GetComponent<InteractionObject>() != null)
                {
                    hit.collider.GetComponent<InteractionObject>().player = transform;
                    hit.collider.GetComponent<InteractionObject>().done = false;
                    target = hit.transform;
                    lastEnemyTarget = target;
                    agent.stoppingDistance = 1;
                    StartCoroutine(FollowTarget()); //follow target pana la destinatie
                    if (hit.collider.name.StartsWith("SKELETON"))
                    {
                        hit.collider.gameObject.GetComponentInChildren<Canvas>().enabled = true;
                        // healthBar.transform.GetComponentInParent<Canvas>().enabled = true;
                        StartCoroutine(ShowHealth(15f));
                        agent.stoppingDistance = 2;
                        if (hasBowEquipped == true)
                        {
                            agent.stoppingDistance = 6.5f;
                            StartCoroutine(AutoBowAttack());
                        }
                    }
                }
                else
                {
                    DialogueManager.instance.panel.SetActive(false);
                }
            }

        }

        if (Input.GetKeyDown(KeyCode.U) && (hasBowEquipped || rightHandEquipped || leftHandEquipped))
        {
            agent.isStopped = true;
            if (hasBowEquipped || (leftHandEquipped && rightHandEquipped == false))
                animator.SetTrigger("unequipLeftHand");
            else if (rightHandEquipped && !leftHandEquipped)
                animator.SetTrigger("unequipRightHand");
            else
                animator.SetTrigger("unequipBothHands");
            FindObjectOfType<AudioManager>().Play("UnequipSword");
            PickUpObject[] itemsEquipped = transform.GetComponentsInChildren<PickUpObject>();
            foreach (PickUpObject itemEquipped in itemsEquipped)
            {
                if (itemEquipped != null)
                {
                    leftHandEquipped = false;
                    rightHandEquipped = false;
                    hasBowEquipped = false;
                    protectionArmour = 0;
                    animator.SetBool("hasShield", false);
                    animator.SetBool("hasBow", false);
                    InventoryManager.instance.Add(itemEquipped.item);
                    Destroy(itemEquipped.gameObject, 0.5f);
                }
            }
        }

        animator.SetFloat("timeSinceLastHit", animator.GetFloat("timeSinceLastHit") + Time.deltaTime);
        var stateNfo = animator.GetCurrentAnimatorStateInfo(0);
        if (stateNfo.IsName("NoShieldTakeHit") || stateNfo.IsName("ShieldTakeHit"))
        {
            StopCoroutine(ResetTakeHit());
            agent.speed = 0f;
            tookHit = true;
            StartCoroutine(ResetTakeHit());
        }
        if (stateNfo.IsName("Death"))
        {
            agent.enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
            transform.rotation = Quaternion.identity;
            StopAllCoroutines();
            this.enabled = false;
            gameOverMenu.SetActive(true);
            PauseMenu.GameOver = true;
        }
        healthBar.value = animator.GetInteger("HP");
    }
    IEnumerator ResetTakeHit()
    {
        yield return new WaitForSeconds(4.5f);
        tookHit = false;
    }

    IEnumerator FollowTarget()
    {
        while (target != null)
        {
            agent.SetDestination(target.position);
            yield return null;
        }
        yield return 0;
    }

    IEnumerator AutoBowAttack()
    {
        while (target != null && hasBowEquipped == true && target.GetComponent<Animator>().GetBool("Died") == false)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), 360 * Time.deltaTime);
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("BowAttack") && Vector3.Distance(target.position, this.transform.position) < 7f)
            {
                animator.SetTrigger("attack");
            }
            yield return null;
        }
        if (target != null && target.GetComponent<Animator>().GetBool("Died"))
        {
            FindObjectOfType<AudioManager>().Play("Victory");
            animator.SetTrigger("victory");
        }
        yield return 0;
    }
    public IEnumerator ShowHealth(float t)
    {
        healthBar.transform.GetComponentInParent<Canvas>().enabled = true;
        yield return new WaitForSeconds(t);
        if(animator.GetFloat("timeSinceLastHit") > 5)
        {
            healthBar.transform.GetComponentInParent<Canvas>().enabled = false;
        }
        else
        {
            yield return ShowHealth(5);
        }
    }
}
