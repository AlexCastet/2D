using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBehaivourr : MonoBehaviour
{
    private enum States { IDLE,Patrullar,TRACK, ATTACK}
    private States m_CurrentState;

    private Rigidbody2D m_Rigidbody;
    private Animator m_Animator;
    private Transform m_Objectiu;
    private SpriteRenderer m_SpriteRenderer;


    private TrackBehaivour trackBehaivour;
 
    private AttackBehaivour attackBehaivour;

    [SerializeField]
    private Collider2D MeleeTrack;
    [SerializeField]
    private Collider2D MeleeAttack;
    [SerializeField]
    private Collider2D RangedTrack;
    [SerializeField]
    private Collider2D RangedAttack;


    [SerializeField]
    private LayerMask layerHurt;


    private float Impuls;
    private float Damage;
    private float Speed;
    private bool isRanged;
    private int Punts;
    //[SerializeField]
    //private float m_Vida;
    // Start is called before the first frame update
    void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();


        
    }
    

    private void OnDestroy()
    {
        trackBehaivour.OnEntrar -= EntraAreaPersecucio;
        trackBehaivour.OnSortir -= SurtAreaPersecucio;
        attackBehaivour.OnEntrar -= EntraAreaAtac;
        attackBehaivour.OnSortir -= SurtAreaAtack;
        
    }
    private void ReturnPool()
    {
        trackBehaivour.OnEntrar -= EntraAreaPersecucio;
        trackBehaivour.OnSortir -= SurtAreaPersecucio;
        attackBehaivour.OnEntrar -= EntraAreaAtac;
        attackBehaivour.OnSortir -= SurtAreaAtack;
       
    }

    // Update is called once per frame
    void Update()
    {
        UpdateState(m_CurrentState);
        
    }
    

    private void Patrullar()
    {
        Vector3 Punt = GameManager.Instance.PuntsPatrulla[0].position;
        if(this.gameObject.transform.position == Punt)
        {
            Punt = GameManager.Instance.PuntsPatrulla[1].position;
        }
        Vector2 DireccioPatrulla = Punt - this.gameObject.transform.position;
        m_Rigidbody.velocity = DireccioPatrulla.normalized * Speed;
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

        if(!isRanged)
        {
            switch (m_CurrentState)
            {
                case States.IDLE:
                    Debug.Log("Estoy en Idle");
                    break;
                
                case States.TRACK:

                    Debug.Log("Teveooo");
                    break;
                case States.ATTACK:
                    Debug.Log("TeAcato");
                    m_Rigidbody.velocity = Vector3.zero;
                    break;
            }
        }
        else
        {
            switch (m_CurrentState)
            {
                case States.IDLE:
                    Debug.Log("Estoy en Idle");
                    break;
               
                case States.TRACK:

                    Debug.Log("Teveooo");
                    break;
                case States.ATTACK:
                    Debug.Log("TeAcato");
                    StartCoroutine(DisparoCorutine());
                    m_Rigidbody.velocity = Vector3.zero;
                    break;
            }
        }
       
    }
    IEnumerator DisparoCorutine()
    {
        //Ha disparar
        Debug.Log("Estoy disparando");
        while (true)
        {
            GameObject spawned = GameManager.Instance.m_ProyectilPool.GetElement();
            //spawned.GetComponent<Rigidbody2D>().velocity = new Vector3(-5, 0, 0);
            if (spawned != null)
            {
                spawned.transform.position = this.gameObject.transform.position;
                Vector2 direccio = m_Objectiu.position - spawned.transform.position;
                spawned.GetComponent<Rigidbody2D>().velocity = direccio.normalized*7;
                
            }
            yield return new WaitForSeconds(3);
        }

    }
    private void UpdateState(States updateState)
    {

        if(!isRanged)
        {
            switch (updateState)
            {
                case States.IDLE:
                    m_Rigidbody.velocity = Vector3.zero;

                    break;
               
                   
                    
                case States.TRACK:
                    m_Rigidbody.velocity = DireccioObjectiu();
                    break;
                case States.ATTACK:

                    break;

            }
        } 
        else
        {
            switch (updateState)
            {
                case States.IDLE:
                    m_Rigidbody.velocity = Vector3.zero;
                    StartCoroutine(WaitCorrutine());
                    ChangeState(States.Patrullar);
                    StopCoroutine(WaitCorrutine());
                    break;
                
                    
                case States.Patrullar:
                    Patrullar();
                    break;
                case States.TRACK:
                    m_Rigidbody.velocity = DireccioObjectiu();
                    break;
                case States.ATTACK:

                    break;

            }
        }
        
    }
    IEnumerator WaitCorrutine()
    {

        yield return new WaitForSeconds(3);

    }

    private Vector2 DireccioObjectiu()
    {
        Vector2 direccio = m_Objectiu.position - this.transform.position;
        return direccio.normalized*Speed;
    }

    private void ExitState(States exitState)
    {
        if(!isRanged)
        {
            switch (exitState)
            {
                case States.IDLE:

                    break;
                
                case States.TRACK:
                    
                    break;
                case States.ATTACK:
                    
                    break;
            }
        }
        else
        {
            switch (exitState)
            {
                case States.IDLE:

                    break;
               
                case States.TRACK:
                    
                    break;
                case States.ATTACK:
                    
                    StopCoroutine(DisparoCorutine());
                    break;
            }
        }
        
    }
    public void EntraAreaPersecucio(GameObject objectiu)
    {
        m_Objectiu = objectiu.transform;
        Debug.Log("Entro en Persecucion " + objectiu.name);
        ChangeState(States.TRACK); 
    }
    public void SurtAreaPersecucio()
    {
        ChangeState(States.IDLE);
    }
    public void EntraAreaAtac(GameObject objectiu)
    {
        ChangeState(States.ATTACK);
        
        
    }
    public void SurtAreaAtack()
    {
        ChangeState(States.TRACK);
    }
    public void GetHurt()
    {
        
        Debug.Log("AAAAA MUEROOOO");
        this.gameObject.GetComponent<Pooleable>().ReturnToPool();
        ReturnPool();
        GameManager.Instance.SumaScore(Punts);
        GameManager.Instance.SumKills();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "AtacP1")
        {
            
            GetHurt();
        }
       
    }
    public void RellenarInfo(EnemyInfo Info)
    {
        Impuls = Info.Impuls;
        Damage = Info.Damage;
        Speed = Info.Speed;
        isRanged = Info.isRanged;
        Punts = Info.Score;
        m_SpriteRenderer.color = Info.Color;
        if(isRanged)
        {
            MeleeAttack.enabled = false;
            MeleeTrack.enabled = false;
            RangedTrack.enabled = true;
            RangedAttack.enabled = true;
            trackBehaivour = RangedTrack.gameObject.GetComponent<TrackBehaivour>();
            attackBehaivour = RangedAttack.gameObject.GetComponent<AttackBehaivour>();
        }
        else
        {
            MeleeAttack.enabled = true;
            MeleeTrack.enabled = true;
            RangedTrack.enabled = false;
            RangedAttack.enabled = false;
            trackBehaivour = MeleeTrack.gameObject.GetComponent<TrackBehaivour>();
            attackBehaivour = MeleeAttack.gameObject.GetComponent<AttackBehaivour>();
        }
        trackBehaivour.OnEntrar += EntraAreaPersecucio;
        trackBehaivour.OnSortir += SurtAreaPersecucio;
        attackBehaivour.OnEntrar += EntraAreaAtac;
        attackBehaivour.OnSortir += SurtAreaAtack;
        ChangeState(States.IDLE);
    }

    public int GetDamage()
    {
        return (int)Damage;
    }
    public int GetImpuls()
    {
        return (int)Impuls;
    }
    
}

