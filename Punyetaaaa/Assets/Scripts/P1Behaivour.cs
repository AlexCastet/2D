using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P1Behaivour : MonoBehaviour
{
    private enum States { IDLE, WALK, ATTACK1, ATTACK2, REBRE, COMBO1, COMBO2 }
    private States m_CurrentState;
    [SerializeField]
    private InputActionAsset m_inputasset;

    private InputActionAsset m_inputAction;
    private InputAction m_Moviment;

    Rigidbody2D m_Rigidbody2d;
    Animator m_Animator;

    [Header("Character Values")]
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_Invulnerability;
    

    // Start is called before the first frame update
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody2d = GetComponent<Rigidbody2D>();
        m_inputAction = Instantiate(m_inputasset);
        m_inputAction.FindActionMap("Land").FindAction("Movement").performed += Movement;
        m_inputAction.FindActionMap("Land").FindAction("Movement").canceled += StopMovement;
        m_inputAction.FindActionMap("Land").FindAction("AtacFort").performed += Atacar;
        m_inputAction.FindActionMap("Land").FindAction("AtacFluix").performed += AtacFluix;
        m_Moviment = m_inputAction.FindActionMap("Land").FindAction("Movement");
        m_inputAction.FindActionMap("Land").Enable();
    }
    private void OnDestroy()
    {
        m_inputAction.FindActionMap("Land").FindAction("Movement").performed -= Movement;
        m_inputAction.FindActionMap("Land").FindAction("Movement").canceled -= StopMovement;
        m_inputAction.FindActionMap("Land").FindAction("AtacFort").performed -= Atacar;
        m_inputAction.FindActionMap("Land").FindAction("AtacFluix").performed -= AtacFluix;
        m_inputAction.FindActionMap("Land").Disable();
    }

    private void AtacFluix(InputAction.CallbackContext context)
    {
        switch (m_CurrentState)
        {
            case States.REBRE:

            case States.IDLE:

            case States.WALK:
                ChangeState(States.ATTACK2);
                break;
        }
    }

    private void StopMovement(InputAction.CallbackContext context)
    {
       
        m_Rigidbody2d.velocity = Vector2.zero;  
        
    }

    private void Atacar(InputAction.CallbackContext context)
    {
        //Atacar
        
        switch (m_CurrentState)
        {
            case States.REBRE:

            case States.IDLE:
                
            case States.WALK:
                ChangeState(States.ATTACK1);
                break;
            case States.ATTACK1:
               
               
                break;
            case States.ATTACK2:
                
                break;
        }
    }
    public void AplicarCombo() 
    {
        switch (m_CurrentState)
        {
            case States.ATTACK1:
                ChangeState(States.COMBO1);

                break;
            case States.ATTACK2:
                ChangeState(States.COMBO2);
                break;
        }
    }
    public void HeAcabatAttack()
    {
        ChangeState(States.IDLE);
    }

    private void Movement(InputAction.CallbackContext context)
    {
        if(m_CurrentState == States.ATTACK1 || m_CurrentState == States.ATTACK2 || m_CurrentState == States.REBRE){return;}
        else
        {
            Vector2 direccio = m_Moviment.ReadValue<Vector2>();
            m_Rigidbody2d.velocity = direccio.normalized*m_Speed;
        }
        
        
    }

    

    // Update is called once per frame
    void Update()
    {
        UpdateState(m_CurrentState);
    }
    void FixedUpdate()
    {
        if(m_CurrentState != States.REBRE)
        {
            if (m_Rigidbody2d.velocity != Vector2.zero)
            {
                ChangeState(States.WALK);
            }
            else
            {
                if (m_CurrentState == States.ATTACK1 || m_CurrentState == States.ATTACK2)
                {
                    return;
                }
                else
                {
                    ChangeState(States.IDLE);
                }
            }
        }
        
        
    }
    private void ChangeState(States newState)
    {
        if (newState == m_CurrentState)
            return;

        ExitState(m_CurrentState);
        InitState(newState);
    }
    private void InitState(States initState)
    {
        m_CurrentState = initState;

        switch (m_CurrentState)
        {
            case States.IDLE:
                m_Animator.Play("IDLE");
                break;
            case States.WALK:
                m_Animator.Play("WALK");
                break;
            case States.ATTACK1:
                m_Rigidbody2d.velocity = Vector2.zero;
                m_Animator.Play("ATTACK");
                break;
            case States.ATTACK2:
                Debug.Log("combeao");
                break;
            case States.REBRE:
                m_Animator.Play("REBRE MAL");
                
                StartCoroutine(Damaged());
                
                break;
            case States.COMBO1:
                break;
            case States.COMBO2:
                break;
        }
    }

    
    public void RebreImpuls(Vector2 direccio, float Impuls)
    {
        m_Rigidbody2d.AddForce(direccio* Impuls);
        ChangeState(States.REBRE);
    }
    public void TornarIDLE()
    {
        ChangeState(States.IDLE);
    }

    private void UpdateState(States updateState)
    {


        switch (updateState)
        {
            case States.IDLE:

                break;
            case States.WALK:
                if(m_Rigidbody2d.velocity.x < 0)
                {
                    this.gameObject.transform.right = Vector3.left;
                }
                else if(m_Rigidbody2d.velocity.x > 0)
                {
                    this.gameObject.transform.right = Vector3.right;
                }
                break;
            case States.ATTACK1:

                break;
            case States.ATTACK2:

                break;
            case States.REBRE:

                break;

        }
    }

    private void ExitState(States exitState)
    {
        switch (exitState)
        {
            case States.IDLE:

                break;
            case States.WALK:

                break;
            case States.ATTACK1:

                break;
            case States.ATTACK2:

                break;
            case States.REBRE:
                StopAllCoroutines();
                break;
        }
    }
    IEnumerator Damaged()
    {
        while (true)
        {    
            yield return new WaitForSeconds(m_Invulnerability);
            ChangeState(States.IDLE);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Atac")
        {
            
           int Damage = collision.gameObject.GetComponentInParent<EnemyBehaivourr>().GetDamage();
            GameManager.Instance.RestarVida(Damage);
            int Impuls = collision.gameObject.GetComponentInParent<EnemyBehaivourr>().GetDamage();
           Vector3 positionImpuls = collision.gameObject.transform.position;
            Vector3 direccio = this.transform.position - positionImpuls;
            RebreImpuls(direccio.normalized, Impuls);
        }
    }

}
