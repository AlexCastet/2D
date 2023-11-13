using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "SO/Vida")]
public class Vida : ScriptableObject
{
    public int m_VidaActual;
    public int m_VidaMax = 100;
}
