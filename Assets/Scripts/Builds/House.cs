using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int woodAmount; // total de madeira para que possa ser possivel contruir a casa
    [SerializeField] private float timeAmount;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    [Header("Components")]
    [SerializeField] private GameObject houseColl;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;


    [SerializeField] private bool detectingPlayer;
    private Player player;
    private PlayerAnim playerAnim;
    private PlayerItens playerItens;

    private float timeCount;
    private bool isBeginig;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnim>();
        playerItens = player.GetComponent<PlayerItens>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItens.totalWood >= woodAmount)
        {
            // Contruçao e inicializada
            isBeginig = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.transform.rotation = point.rotation;
            // isPaused passando na clase House ou direto no playerAnim
            player.isPaused = true;
            playerItens.totalWood -= woodAmount;
        }

        if (isBeginig)
        {
            timeCount += Time.deltaTime;

            if (timeCount >= timeAmount)
            {
                //casa finalizada
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused = false;
                houseColl.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            detectingPlayer = false;
        }
    }
}
