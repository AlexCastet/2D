using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaivour : MonoBehaviour
{
    public Action<GameObject> OnEntrar;
    public Action OnSortir;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEntrar?.Invoke(collision.gameObject);

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OnSortir?.Invoke();
    }
}
