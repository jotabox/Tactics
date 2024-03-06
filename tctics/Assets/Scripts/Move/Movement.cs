using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Movement : MonoBehaviour
{
    const float moveSpeed = 0.5f;
    const float jumpHeight = 0.5f;
    public bool teste;
    public List<Vector3Int> path;
    SpriteRenderer spriteRenderer;
    Transform jumper;
    TileLogic tileAtual;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        jumper = transform.Find("Jumper");
    }
 
    private void Update()
    {
        if (teste)
        {
            teste = false;
            StopAllCoroutines();
            StartCoroutine(Move());
        }
    }

    IEnumerator Move()
    {
        tileAtual = Board.GetTile(path[0]);
        transform.position = tileAtual.worldPos;

        for (int i = 1; i < path.Count; i++)
        {
            TileLogic to = Board.GetTile(path[i]);
            if (to == null)
            {
                continue;
            }

            tileAtual.content = null;

            if(tileAtual.floor != to.floor)
            {
                yield return StartCoroutine(Jump(to));
            }
            else
            {
                yield return StartCoroutine(Walk(to));
            }
        }
    }


    IEnumerator Walk(TileLogic to)
    {
        int id = LeanTween.move(transform.gameObject, to.worldPos, moveSpeed).id;
        tileAtual = to;
        
        yield return new WaitForSeconds(moveSpeed * 0.5f);
        spriteRenderer.sortingOrder = to.contentOrder;

        while (LeanTween.descr(id) != null)
        {
            yield return null;
        }

        to.content = this.gameObject;
    }

    IEnumerator Jump(TileLogic to)
    {
        int id = LeanTween.move(transform.gameObject, to.worldPos, moveSpeed).id;
        LeanTween.moveLocalY(jumper.gameObject, jumpHeight, moveSpeed * 0.5f).
            setLoopPingPong(1).setEase(LeanTweenType.easeInOutQuad);

        float timeOrderUpdate = moveSpeed;

        if(tileAtual.floor.tilemap.tileAnchor.y > to.floor.tilemap.tileAnchor.y)
        {
            timeOrderUpdate *= 0.85f;
        }
        else
        {
            timeOrderUpdate *= 0.2f;
        }

        yield return new WaitForSeconds(timeOrderUpdate);
        tileAtual = to;
        spriteRenderer.sortingOrder = to.contentOrder;

        while(LeanTween.descr(id) != null)
        {
            yield return null;
        }

        to.content = this.gameObject;

    }


}
