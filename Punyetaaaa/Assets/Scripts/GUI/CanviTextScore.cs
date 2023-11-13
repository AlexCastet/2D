using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanviTextScore : MonoBehaviour
{
    TextMeshProUGUI textMP;
    [SerializeField]
    public Punts punts;
    void Awake()
    {
        textMP = GetComponent<TextMeshProUGUI>();
        CanviText();
    }
    public void CanviText()
    {
        textMP.text = "Score: " + punts.m_Score;
    }

}
