using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Dictionary<Vector3Int, TileLogic> tiles;
    public List<Floor> floors;
    public static Board instance;
    [HideInInspector] public Grid grid;

    private void Awake()
    {
        tiles = new Dictionary<Vector3Int, TileLogic>();
        instance = this;
        grid = GetComponent<Grid>();
    }


    public IEnumerator InitSequence(LoadState loadState)
    {
        yield return StartCoroutine(LoadFloors(loadState));
        yield return null;
        Debug.Log("foram criados -" + tiles.Count + " - tiles");
        ShadowOrdering();
        yield return null;

    }

    IEnumerator LoadFloors(LoadState loadState)
    {
        for (int i = 0; i < floors.Count; i++)
        {
            List<Vector3Int> floorTiles = floors[i].LoadTiles();
            yield return null;
            for (int j = 0; j < floorTiles.Count; j++)
            {
                if (!tiles.ContainsKey(floorTiles[j]))
                {
                    CreateTile(floorTiles[j], floors[i]);
                }
            }
        }
    }

    void CreateTile(Vector3Int pos, Floor floor)
        {
        Vector3 worldPos = grid.CellToWorld(pos);
        worldPos.y += (floor.tilemap.tileAnchor.y / 2) - 0.5f;
        TileLogic tileLogic = new TileLogic(pos, worldPos, floor);
        tiles.Add(pos, tileLogic);
    }

    void ShadowOrdering()
    {
        foreach (TileLogic item in tiles.Values)
        {
            int floorIndex = floors.IndexOf(item.floor);
            floorIndex -= 2;
             if(floorIndex >= floors.Count ||  floorIndex < 0)
            {
                continue;
            }

             Floor floorToCheck = floors[floorIndex];

            Vector3Int pos = item.pos;
            IsNECheck(floorToCheck, item, pos + Vector3Int.right);
            IsNECheck(floorToCheck, item, pos + Vector3Int.up);
            IsNECheck(floorToCheck, item, pos + Vector3Int.right + Vector3Int.up);
        }
    }

    void IsNECheck(Floor floor, TileLogic tileLogic, Vector3Int NEPosition)
    {
        if (floor.tilemap.HasTile(NEPosition))
        {
            tileLogic.contentOrder = floor.order;
        }
    }

    public static TileLogic GetTile(Vector3Int pos)
    {
        TileLogic tile = null;
        instance.tiles.TryGetValue(pos, out tile);

        return tile;

    }

}
