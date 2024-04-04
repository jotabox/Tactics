using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    public static MapLoader Instance;
    public Unit unitPrefab;

    //job
    // objeto do mapa
    //localização das unidades

    private void Awake()
    {
        Instance = this;
    }


    public void CreateUnit()
    {
        GameObject holder = new GameObject("Units Holder");
        holder.transform.parent = Board.instance.transform;
        Unit unite = Instantiate(unitPrefab, Board.GetTile(new Vector3Int(-1, -6, 0)).worldPos, Quaternion.identity, holder.transform);
    }
}
