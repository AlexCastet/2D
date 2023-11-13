using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float m_SpawnRate;
    //  private float m_SpawnRateDelta = 3f;
    
    [SerializeField]
    private Transform[] m_SpawnPoints;
    [SerializeField]
    private EnemyInfo m_EnemyInfoMeele;
    [SerializeField]
    private EnemyInfo m_EnemyInfoRanged;
    [SerializeField]
    private Onades m_Onades;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    // Update is called once per frame

    IEnumerator SpawnCoroutine()
    {
        
        int[] Enemys = m_Onades.ConseguirEnemys();
        foreach (int enemy in Enemys)
        {
            GameObject spawned = GameManager.Instance.m_EnemyPool.GetElement();
            //spawned.GetComponent<Rigidbody2D>().velocity = new Vector3(-5, 0, 0);
            if (spawned != null)
            {
                spawned.transform.position = m_SpawnPoints[Random.Range(0, m_SpawnPoints.Length)].position;

                if (enemy == 1)
                {
                    spawned.GetComponent<EnemyBehaivourr>().RellenarInfo(m_EnemyInfoRanged);
                }
                else
                {
                    spawned.GetComponent<EnemyBehaivourr>().RellenarInfo(m_EnemyInfoMeele);

                }

                yield return new WaitForSeconds(m_SpawnRate);
            }
        }
           
            
        
    }

    public void StartCor()
    {
        StartCoroutine(SpawnCoroutine());   
    }
}
