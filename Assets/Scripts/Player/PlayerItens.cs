using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItens : MonoBehaviour
{
    [Header("Amounts")]
    [SerializeField] private int _totalWood;
    [SerializeField] private int _carrots;
    [SerializeField] private int _fishes;
    

    [SerializeField] private float _currentWater;

    [Header("Limits")]
    [SerializeField] private float _waterLimit = 50f;
    [SerializeField] private float _carrotLimit = 10f;
    [SerializeField] private float _woodLimit = 15f;
    [SerializeField] private float _fishesLimit = 5f;


    #region Getter and Setters
    public int totalWood { get => _totalWood; set => _totalWood = value; }
    public int carrots { get => _carrots; set => _carrots = value; }
    public int fishes { get => _fishes; set => _fishes = value; }

    public float currentWater { get => _currentWater; set => _currentWater = value; }

    public float waterLimit { get => _waterLimit; set => _waterLimit = value; }
    public float carrotLimit { get => _carrotLimit; set => _carrotLimit = value; }
    public float woodLimit { get => _woodLimit; set => _woodLimit = value; }
    public float fishesLimit { get => _fishesLimit; set => _fishesLimit = value; }
    #endregion
    public void WaterLimit(float water)
    {
        if (currentWater < waterLimit)
        {
            currentWater += water;
        }
    }
}
