using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get { return m_Instance; }
    }
    [SerializeField]
    private Pool Enemy;
    public Pool m_EnemyPool => Enemy;

    [SerializeField]
    private Pool Proyectil;
    public Pool m_ProyectilPool=> Proyectil;

    public int EnemysDied = 0;

    [SerializeField]
    private Punts m_Score;
    [SerializeField]
    private Vida m_vida;
    [SerializeField]
    private Onades m_onades;
    [SerializeField]
    public Transform[] PuntsPatrulla;

    [Header("GameEvents")]
    [SerializeField]
    private GameEvent m_CanviVida;
    [SerializeField]
    private GameEvent m_CanviTextScore;
    [SerializeField]
    private GameEvent m_CanviOnada;
    // Start is called before the first frame update
    void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
            m_Score.m_Score = 0;
        }     
        else
        {
            Destroy(gameObject);
            return;
        }

        
        DontDestroyOnLoad(gameObject);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemysDied == m_onades.m_num_enemics)
        {
            SumaOnades();
            SumaScore(10);
        }
    }
    public void SumaScore(int sum)
    {
        m_Score.m_Score += sum*m_onades.m_Onades;
        m_CanviTextScore.Raise();
    }
    public void RestarVida(int sum)
    {
        m_vida.m_VidaActual-=sum;
        m_CanviVida.Raise();
    }
    public void SumaOnades()
    {
        m_onades.m_Onades++;
        EnemysDied = 0;
        m_CanviOnada.Raise();
    }
    public void GameOver()
    {
        if(m_Score.m_Score > m_Score.m_HighScore)
        {
            m_Score.m_HighScore = m_Score.m_Score;
            Debug.LogError("HAS ACONSEGUIT SUPERAR EL RECORD");
        }
    }
    public void SumKills()
    {
        EnemysDied++;
    }
}
