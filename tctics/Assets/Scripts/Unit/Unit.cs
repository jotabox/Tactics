using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [HideInInspector]
    public Stats stats;

    private void Awake()
    {
        stats = GetComponentInChildren<Stats>();
    }
}
