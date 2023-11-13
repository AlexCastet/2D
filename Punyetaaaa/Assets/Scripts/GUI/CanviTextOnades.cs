using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanviTextOnades : MonoBehaviour
{
    

    TextMeshProUGUI textMP;
    [SerializeField]
    private Onades m_Onade;
    void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
        CanviText();

    }
    public void CanviText()
    {
        textMP.text = "Onades: " + m_Onade.m_Onades;
    }

}
