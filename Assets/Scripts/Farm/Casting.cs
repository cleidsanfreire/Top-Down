using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{

    [SerializeField] private int percentage; // porcentagem de chance de pescar um peixe a cada tentativa
    [SerializeField] private GameObject fishPrefab;

    private bool detectingPlayer;

    private PlayerItens player;
    private PlayerAnim playerAnim;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerItens>();
        playerAnim = player.GetComponent<PlayerAnim>();
    }

    // Update is called once per frame
    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnim.OnCastingStart();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if (randomValue <= percentage)
        {
            // conseguiu pescar um peixe
            var positionX = Random.Range(-3, 3);
            Instantiate(fishPrefab, player.transform.position + new Vector3(positionX, 0f,0f), Quaternion.identity);

        }
        else
        {
            //falhou (pescou vento)
            Debug.Log("Pescou nada");
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
