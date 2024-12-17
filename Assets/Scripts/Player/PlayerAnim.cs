using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPointSword;

    [SerializeField] private float radius;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;

    private bool isHitting;
    private float timeCount;
    private float recoreryTime = 1f;

    private Casting cast;

    public bool IsHitting { get => isHitting; set => isHitting = value; }

    private void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();

        cast = FindObjectOfType<Casting>();
    }

    private void Update()
    {
            OnMove();
            OnRun();

        if (isHitting)
        {
            timeCount += Time.deltaTime;
            if (timeCount >= recoreryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement
    void OnMove()
    {

        if (player.isRolling)
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("roll"))
            {
                anim.SetTrigger("isRoll");
            }
        }

        if (player.direcion.sqrMagnitude > 0)
        {
            anim.SetInteger("iTransition", 1);
        }
        else 
        {
            anim.SetInteger("iTransition", 0);
        }

        if (player.direcion.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direcion.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.isCutting)
        {
            anim.SetInteger("iTransition", 3);
        }

        if (player.isDigging)
        {
            anim.SetInteger("iTransition", 4);
        }

        if (player.isWatering)
        {
            anim.SetInteger("iTransition", 5);
        }

        if (player.isAttack)
        {
            anim.SetInteger("iTransition", 6);
        }
    }

    void OnRun()
    {
        if (player.isRunning && player.direcion.sqrMagnitude > 0)
        {
            anim.SetInteger("iTransition", 2);
        }
    }

    // é chamado quando o jogador pressiona o botão de açao na agua
    public void OnCastingStart()
    {
        anim.SetTrigger("isCasting");
        player.isPaused = true;
    } 

    // é chamado quando termina de executar a animação de pescaria
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("isHammering", true);
        //player.isPaused = true;
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("isHammering", false);
        //player.isPaused = false;
    }
    public void OnHit()
    {
        if (!isHitting)
        {
            anim.SetTrigger("hit");
            isHitting = true;
        }
    }
    #endregion

    #region Attack
    public void OnAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPointSword.position, radius, enemyLayer);

        if (hit != null)
        {
            // atacou o enemy
            hit.GetComponentInChildren<AnimationControlSkeleton>().OnHitSkeleton();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointSword.position, radius);
    }
    #endregion
}
