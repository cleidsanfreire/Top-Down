using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControlSkeleton : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private PlayerAnim player;
    private Skeleton skeleton;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerAnim>();
        skeleton = GetComponentInParent<Skeleton>();
    }
    public void PlayeAnim(int value)
    {
            anim.SetInteger("transition", value);
    }

    void OnAttack()
    {
        if (!skeleton.isDead)
        {
            Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

            if (hit != null)
            {
                // detecta colisao com player
                player.OnHit();
            }
        }
    }
    public void OnHitSkeleton()
    {
        
        if (skeleton.currenthealth <= 0)
        {
            skeleton.isDead = true;
            anim.SetTrigger("death");

            Destroy(skeleton.gameObject,10f);
        }
        else
        {
            anim.SetTrigger("hit");
            skeleton.currenthealth--;

            skeleton.healthBar.fillAmount = skeleton.currenthealth / skeleton.totalHealth;

        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}
