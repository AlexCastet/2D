using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooleable : MonoBehaviour
{
    private Pool m_Propietari;


    public void SetPool(Pool owner)
    {
        m_Propietari = owner;
    }

    public void ReturnToPool()
    {
        if (!m_Propietari.ReturnElement(gameObject))
            Debug.LogError(gameObject + ": Pool no correctamente configurada al devolver.");
    }
}
