using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public float arrowSpeed = 10f;
    public PlayerController playerController;
    Transform targetPosition;
    public string compareTo;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.target != null)
        {
            targetPosition = playerController.target;
        }
        else
        {
            targetPosition = playerController.lastEnemyTarget;
        }
        if(targetPosition != null)
        {
            transform.position = Vector3.Slerp(transform.position, new Vector3(targetPosition.position.x, targetPosition.position.y + 1, targetPosition.position.z), arrowSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(compareTo))
        {
            Animator oppAnimator = other.GetComponentInParent<Animator>();
            if (oppAnimator != null)
            {
                if (oppAnimator.GetFloat("timeSinceLastHit") > 0.5f)
                {
                    oppAnimator.SetTrigger("takeHit");
                    if (oppAnimator.name == "SKELETON")
                        ParticleSysManager.instance.Play(1);
                    else
                        ParticleSysManager.instance.Play(2);
                    FindObjectOfType<AudioManager>().Play("AttackArrowHit");
                    oppAnimator.SetInteger("HP", oppAnimator.GetInteger("HP") - damage);
                }
            }
            transform.SetParent(other.transform);
            Destroy(gameObject, 1f);
        }
    }
}
