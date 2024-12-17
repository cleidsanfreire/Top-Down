using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Dialogue : MonoBehaviour
{

    public float dialogueRange; // Area do npc onde o player chega perto e entra na area para verificar a interacao
    public LayerMask playerLayer; // definindo a layer para fazer a conexao

    public DialogueSettings dialogue; // Fazendo interacao com outra classe DialogueSettings.cs

    bool playerHit; // para verificar se o player esta na area de colisao ou nao

    private List<string> sentences = new List<string>(); // Uma lista com todas as frases(texto) dos NPCs
    private List<Sprite> spriteActor = new List<Sprite>();
    private List<string> nameActor = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        GetNPCInfo(); // Chamando as frases do npc no idioma selecionado
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerHit) // verificando se o player chegou perto dentro do limite do raio e se apertou a tecla E para interagir
        {
            DialogueControl.instance.Speech(sentences.ToArray(), nameActor.ToArray(), spriteActor.ToArray()); // Transformando a lista de frases em uma array para passar 1 de cada vez na tela de dialogo
        }
    }

    void GetNPCInfo()
    {
        for (int i = 0; i < dialogue.dialogues.Count; i++) // Enquanto i for menor q a quantidade de dialogos ira somar mais 1 ate que toda a lista seja e exibida na tela e encerrar
        {
            switch (DialogueControl.instance.language) // verificando os idiomas e passando o idioma pra lista
            {
                case DialogueControl.idiom.pt:
                    sentences.Add(dialogue.dialogues[i].setence.portuguese);
                    break;

                case DialogueControl.idiom.en:
                    sentences.Add(dialogue.dialogues[i].setence.english);
                    break;

                case DialogueControl.idiom.es:
                    sentences.Add(dialogue.dialogues[i].setence.spanish);
                    break;
            }

            nameActor.Add(dialogue.dialogues[i].actorName);
            spriteActor.Add(dialogue.dialogues[i].profile);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        ShowDialogue(); // Abrindo e fechando a tela de dialogo
    }

    void ShowDialogue() // fazer um circulo de  colissao 
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, dialogueRange, playerLayer);

        if (hit != null)
        {
            //Debug.Log("Player na area de colisa");
            playerHit = true; // verificando se o player esta na area de colisao definindo como verdadeiro
        }
        else
        {
            playerHit = false; // verificando se o player esta na area de colisao definindo como falso
        }
    }

    // Desenhando o circulo ao redor do NPC para melhor visualizar o tamanho da area de colissao ( dialogueRange )
    private void OnDrawGizmosSelected()
    {
        //     Draw Wire Sphere  desenhando a sphera na tela para ter controle
        Gizmos.DrawWireSphere(transform.position, dialogueRange);
    }

}
