using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private float speed;
    [SerializeField] private float timeMove;
    private float timeCount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerItens = collision.GetComponent<PlayerItens>();
        if (collision.CompareTag("Player"))
        {
            audioSource.Play();
            if (playerItens.totalWood < playerItens.woodLimit)
            {
                playerItens.totalWood++;
            }
            Destroy(gameObject, 0.1f);
        }
    }
    void Update()
    {
        timeCount += Time.deltaTime;

        if (timeCount < timeMove) 
        { 
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
