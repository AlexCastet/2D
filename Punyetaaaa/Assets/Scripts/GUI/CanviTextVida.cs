using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanviTextGUI : MonoBehaviour
{
   
    [SerializeField]
    Vida Vida;
    
    TextMeshProUGUI textMP;
    void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
        CanviText();

    }
    public void CanviText()
    {
        textMP.text = Vida.m_VidaActual + "/" + Vida.m_VidaMax;
    }

    
}
