using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "New Dialogue/Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor; // Passar qual o actor(Personagem) que vai falar

    [Header("Dialogue")]
    public Sprite speakerSprite; // sprite do falador
    public string setence; // a fala que o personagem vai falar

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName; // Nome do NPC ( DO ACTOR)
    public Sprite profile; // uma foto do personagem
    public Languages setence; // linguagem das falas
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI() // Sobrescrever o editor( criando um novo metodo)
    {
        DrawDefaultInspector(); // Chamando o Inspector default

        DialogueSettings ds = (DialogueSettings)target;

        Languages l = new  Languages(); // Instanciando um novo objeto idioma
        l.portuguese = ds.setence; // definindo qual o idioma sera adicionado por padrao

        Sentences s = new Sentences(); // Instanciando um novo objeto Frase
        s.profile = ds.speakerSprite; // definindo qual a foto do falante
        s.setence = l; // definindo a qual idioma a frase sera adicionada por padrao

        if(GUILayout.Button("Create Dialogue"))  // Criando um button e definindo o nome do button
        {
            if (ds.setence != "")  // Verificando se o campo de frase e diferente de vazio, evitando criar dialogos vazios...
            {
                ds.dialogues.Add(s); // se ouver a frase a mesma sera adionada na lista de dialogos

                ds.speakerSprite = null; // e apos adicionar limpar o campo de foto do falante
                ds.setence = ""; // e apos adicionar limpar o campo de frase
            }
            
        }
    }
}


#endif
