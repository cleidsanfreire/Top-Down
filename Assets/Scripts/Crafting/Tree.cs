using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHealth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;
    [SerializeField] private ParticleSystem leafs;


    private bool isCut;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnHit()
    {
        treeHealth--;
        anim.SetTrigger("tHit");
        leafs.Play();

        if (treeHealth <= 0)
        {
            // cria o toco e instancia os drops(objetos dropados(madeirinhas)) // transforme.position e rotation se refere a posição da arvore.
            for (int i = 0; i < totalWood; i++)
                {
                    Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f,1f),Random.Range(-1f,1f),0f), transform.rotation);
                }

            anim.SetTrigger("tCut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isCut)
        {
            //Debug.Log("Bateu"); // Para saber se o machado ta pegando ou nao
            OnHit();
        }
    }


}
