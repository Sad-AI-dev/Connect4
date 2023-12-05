using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridTile : MonoBehaviour
{
    public int ownerID = 0;

    [Header("External Components")]
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //guarentee valid sprite renderer ref
        if (!spriteRenderer) { spriteRenderer = GetComponent<SpriteRenderer>(); }
    }
}
