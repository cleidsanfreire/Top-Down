using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Test
//using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool isPaused;

    private Rigidbody2D rig;
    private PlayerItens playerItens;

    [Header("Moviment")]
    [SerializeField] private float speed;
    [SerializeField] private float runSpeed;
    private float initalSpeed;

    #region Boolean
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;
    private bool _isCasting;
    private bool _isAttack;
    #endregion

    private Vector2 _direcion = new Vector2(0, 0);
    //[HideInInspector] // Se a informaçao tem que ser publica mas nao e util para o Inspector utilize o [HideInInspector]
    private int handlingObj;

    #region Getter/Setters

    public Vector2 direcion
    {
        get { return _direcion; }
        set { _direcion = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public bool isCutting // Outra forma de fazer um getter and setter
    {
        get => _isCutting;
        set => _isCutting = value;
    }
    public bool isDigging 
    { 
        get => _isDigging; 
        set => _isDigging = value; 
    }
    public bool isWatering 
    { 
        get => _isWatering; 
        set => _isWatering = value; 
    }
    public int HandlingObj { get => handlingObj; set => handlingObj = value; }
    public bool isCasting { get => _isCasting; set => _isCasting = value; }
    public bool isAttack { get => _isAttack; set => _isAttack = value; }
    #endregion

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItens = GetComponent<PlayerItens>();
        initalSpeed = speed;
    }

    private void Update()
    {
        if (!isPaused) 
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                HandlingObj = 0;
            }
        
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                HandlingObj = 1;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                HandlingObj = 2;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                HandlingObj = 3;
            }

            OnInput();
            OnRun();
            OnRoll();
            OnCutting();
            OnDig();
            OnWatering();
            OnAttack();
        }

        // Pra mudar de cena importar o using UnityEngine.SceneManagement;
        // e verificar se o player apertou alguma tecla, ou se abriu e passou por alguma porta, ou se teletrasportou
        // SceneManager.LoadScene("teste"); pra chamar a nova cena(trocar de cena)
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    SceneManager.LoadScene("teste");
        //}
    }

    private void FixedUpdate()
    {
        if (!isPaused) 
        {
            OnMove();
        }
    }

    #region Movement
    void OnInput()
    {
        direcion = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + direcion * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
            speed = runSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
            speed = initalSpeed;
        }
    }

    void OnRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isRolling = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isRolling = false;
        }
    }
    void OnAttack()
    {
        if (HandlingObj == 0) 
        { 
            if (Input.GetMouseButtonDown(0))
            {
                isAttack = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isAttack = false;
            }
        }
        else
        {
            isAttack = false;
        }
        
     }
    void OnCutting()
    {
        if(HandlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isCutting = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isCutting = false;
                speed = initalSpeed;
            }
        }
        else
        {
            isCutting = false;
        }
    }
    void OnDig()
    {
        if (HandlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initalSpeed;
            }
        }
        else
        {
            isDigging = false;
        }
    }
    void OnWatering()
    {
        if (HandlingObj == 3)
        {
            if (Input.GetMouseButtonDown(0) && playerItens.currentWater > 0)
            {
                isWatering = true;
                speed = 0;
            }
            if (Input.GetMouseButtonUp(0) || playerItens.currentWater < 0)
            {
                isWatering = false;
                speed = initalSpeed;
            }

            if (isWatering)
            {
                playerItens.currentWater -= 0.01f;

            }
            else
            {
                isWatering = false;
            }
        }
    }
    #endregion
}
