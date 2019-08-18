using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlteredTileInfo
{
    private List<GameObject> newTile;
    public int maxDistance { get; set; }

    public AlteredTileInfo()
    {
        newTile = new List<GameObject>();
    }

    public IEnumerable<GameObject> AlteredTile
    {
        get{
            return newTile.Distinct();
        }
    }

    public void AddTile( GameObject tile )
    {
        if(!newTile.Contains(tile))
        {
            newTile.Add(tile);
        }
    }
}
