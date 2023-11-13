using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO", menuName = "SO/Onades")]
public class Onades : ScriptableObject
{
    public int m_Onades = 0;
    public int m_num_enemics;
    
    public int[] ConseguirEnemys()
    {
        int[] array;
        if(m_Onades == 0)
        {
            array = new int[3];
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
            m_num_enemics = array.Length;  
            return array;
        }
        else if(m_Onades == 1)
        {
            array = new int[3];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 1;
            }
            m_num_enemics = array.Length;
            return array;
        }
        else
        {
            array = new int[m_Onades];
            for (int i = 0;i < array.Length; i++)
            {
                if (Random.Range(1, 5) == 4)
                {
                    array[i] = 1;
                }
                else
                {
                    array[i] = 0;
                }
            }
            m_num_enemics = array.Length;
            return array;
        }
    }
}
