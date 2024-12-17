using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour
{
    [Header("Stats")]
    public LayerMask layer;
    public float radius;
    public float totalHealth;
    public float currenthealth;
    public Image healthBar;
    private bool _isDead;

    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControlSkeleton skeletonAnim;

    private Player player;
    private bool detectPlayer;


    public bool isDead { get => _isDead; set => _isDead = value; }

    // Start is called before the first frame update
    void Start()
    {
        currenthealth = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && detectPlayer) {
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);

            if (Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
            {
                // chegou no limite de distancia(perto do player) // skeleton para
                skeletonAnim.PlayeAnim(2);
              
            }
            else // skeleton anda // segue o player
            {
                skeletonAnim.PlayeAnim(1);
            }

            float posX = player.transform.position.x - transform.position.x;

            if (posX > 0)
            {
                transform.eulerAngles = new Vector2(0, 0);
            }
            else 
            {
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }
    public void DetectPlayer()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, radius, layer);
        
        if (collider != null)
        {
            // enxergou(vendo) o player
            detectPlayer = true;
        }
        else
        {
            // não está vendo o player
            detectPlayer = false;
            skeletonAnim.PlayeAnim(0);
            agent.isStopped = true;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
