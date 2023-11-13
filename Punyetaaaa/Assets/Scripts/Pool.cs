using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public GameObject PoolableObject;
    private List<GameObject> m_Pool;
    public int Capacitat = 10;

    public bool ReturnElement(GameObject element)
    {
        if (m_Pool.Contains(element))
        {
            element.SetActive(false);
            return true;
        }


        return false;
    }

    public GameObject GetElement()
    {
        foreach (GameObject objeto in m_Pool)
            if (!objeto.activeInHierarchy)
            {
                objeto.SetActive(true);
                return objeto;
            }


        return null;
    }


    private void Awake()
    {
        //Comprovar que al PoolableObject hi hagi
        //component de Poolable
        m_Pool = new List<GameObject>();
        if (PoolableObject.GetComponent<Pooleable>() == null)
        {
            Debug.LogError("Pool: " + gameObject + " poolable object does not have a Pooleable component.");
            return;
        }

        //inicialitzem la llista de valors (m_Pool)
        for (int i = 0; i < Capacitat; i++)
        {
            GameObject element = Instantiate(PoolableObject, gameObject.transform);
            element.GetComponent<Pooleable>().SetPool(this);
            m_Pool.Add(element);
            element.SetActive(false);
        }
    }
}
