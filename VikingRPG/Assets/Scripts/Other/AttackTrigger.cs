using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public string compareTo;
    public int damage = 15;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer(compareTo))
        {
            Animator oppAnimator = other.GetComponentInParent<Animator>();
            if(oppAnimator != null)
            {
                if (oppAnimator.GetFloat("timeSinceLastHit") > 0.5f)
                {
                    oppAnimator.SetTrigger("takeHit");
                    oppAnimator.SetInteger("HP", oppAnimator.GetInteger("HP") - damage);
                    if (oppAnimator.GetComponent<PlayerController>() != null && oppAnimator.GetComponentInChildren<UnityEngine.UI.Slider>().value > 0)
                    {
                        ParticleSysManager.instance.Play(0);
                        FindObjectOfType<AudioManager>().Play("AttackSwordHit");
                        oppAnimator.SetInteger("HP", oppAnimator.GetInteger("HP") + oppAnimator.GetComponent<PlayerController>().protectionArmour);
                    }
                    else
                    {
                        ParticleSysManager.instance.Play(1);
                        FindObjectOfType<AudioManager>().Play("SkeletonAttackHit");
                    }
                }
            }
        }
    }
}
