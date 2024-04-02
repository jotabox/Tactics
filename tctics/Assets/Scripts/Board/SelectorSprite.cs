using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorSprite : MonoBehaviour
{

    public static SelectorSprite instance;
    public Vector3Int position;
    public TileLogic tileLogic;
    
    [HideInInspector]public SpriteRenderer spriteRenderer;


    private void Awake()
    {
        instance = this;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
}
