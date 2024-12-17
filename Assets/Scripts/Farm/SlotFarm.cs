using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audios")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] private int digAmount; // quantidade de "escavação"
    [SerializeField] private int waterAmount; // total de agua para nascer uma cenoura
  

    [SerializeField] private bool detecting;
    [SerializeField] private bool isPlayer; // fica verdadeiro quando o player esta encostando

    private bool dugHole;
    private bool plantedCarrot;

    private int initialDigAmount;
    private float currentWater;

    PlayerItens playerItens;

    private void Start()
    {
        playerItens = FindAnyObjectByType<PlayerItens>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            // encheu o total de agua nescessario
            if (currentWater >= waterAmount && !plantedCarrot)
            {
                spriteRender.sprite = carrot;
                audioSource.PlayOneShot(holeSFX);

                plantedCarrot = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && plantedCarrot && isPlayer)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRender.sprite = hole;
                playerItens.carrots++;
                currentWater = 0f;
                plantedCarrot = false;
            }
        }
    }

    void OnHite()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRender.sprite = hole;
            dugHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHite();
        }

        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }

        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }
        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}
