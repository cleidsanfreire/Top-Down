using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [System.Serializable]
    public enum idiom
    {
        pt,
        en,
        es
    }

    public idiom language;

    [Header("Components")]
    public GameObject dialogueObj; //janela do dialogo
    public Image profileSprite; // sprite(foto) do perfil
    public Text speechText; // texto da fala
    public Text actorNameText; // nome do npc(personagem)

    [Header("Settings")]
    public float typingSpeed; // velocidade da fala

    // Variaveis de controle
    private bool isShowing; // se a janela esta visivel (ativar e desativar)
    private int index; // index das sentences(falas/textos)
    private string[] sentences;
    private string[] currentName;
    private Sprite[] spriteProfile;

    private Player player;

    public static DialogueControl instance;

    public bool IsShowing { get => isShowing; set => isShowing = value; }

    private void Awake()
    {
        instance = this;
        player = FindObjectOfType<Player>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator TypeSentence()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence() // pular pra proxima frase/fala
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                profileSprite.sprite = spriteProfile[index];
                actorNameText.text = currentName[index];
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // quando termina os textos
            {
                speechText.text = "";
                actorNameText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false;
                player.isPaused = false;
            }
        }
    }

    public void Speech(string[] txt, string[] actorName, Sprite[] actorProfile) // chamar a fala do npc
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            currentName = actorName;
            spriteProfile = actorProfile;
            profileSprite.sprite = spriteProfile[index];
            actorNameText.text = currentName[index];
            StartCoroutine(TypeSentence());
            isShowing = true;
            player.isPaused = true;
        }
    }
}
