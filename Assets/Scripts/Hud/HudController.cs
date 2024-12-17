using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HudController : MonoBehaviour
{
    [Header("items")]
    [SerializeField] private Image waterUIBar;
    [SerializeField] private Image woodUIBar;
    [SerializeField] private Image carrotUIBar;
    [SerializeField] private Image fishUIBar;

    [Header("Tools")]
    //[SerializeField] private Image swordUITools;
    //[SerializeField] private Image axeUITools;
    //[SerializeField] private Image shoveUITools;
    //[serializefield] private image bucketuitools;

    public List<Image> toolsUi = new List<Image>();
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;

    private PlayerItens playerItens;
    private Player player;

    private void Awake()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        player = playerItens.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        waterUIBar.fillAmount = 0f;
        woodUIBar.fillAmount = 0f;
        carrotUIBar.fillAmount = 0f;
        fishUIBar.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        waterUIBar.fillAmount = playerItens.currentWater / playerItens.waterLimit;
        woodUIBar.fillAmount = playerItens.totalWood / playerItens.woodLimit;
        carrotUIBar.fillAmount = playerItens.carrots / playerItens.carrotLimit;
        fishUIBar.fillAmount = playerItens.fishes / playerItens.fishesLimit;

        //toolsUi[player.HandlingObj].color = selectColor;

        for (int i = 0; i < toolsUi.Count; i++)
        {
            if (i == player.HandlingObj)
            {
                toolsUi[i].color = selectColor;
            }
           else
            {
                toolsUi[i].color = alphaColor;
            }
        }
    }
}
