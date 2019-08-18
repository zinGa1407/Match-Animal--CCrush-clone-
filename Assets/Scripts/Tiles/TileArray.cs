using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class TileArray
{
    GameObject[,] tiles = new GameObject[GameVariables.Rows, GameVariables.Columns];

    //Backup Tiles during swap
    private GameObject object1;
    private GameObject object2;

    public GameObject this[int row, int column] {
        get {
            try
            {
                return tiles[row, column];
            }
            catch (Exception e)
            {
                throw;
            }
        }
        set {
            tiles[row, column] = value;
        }
    }

    public void Swap(GameObject g1, GameObject g2)
    {
        object1 = g1;
        object2 = g2;

        Tile g1Tile = g1.GetComponent<Tile>();
        Tile g2Tile = g2.GetComponent<Tile>();

        int g1Row = g1Tile.Row;
        int g1Column = g1Tile.Column;
        int g2Row = g2Tile.Row;
        int g2Column = g2Tile.Column;

        //Swap inside TileArray
        var temp = tiles[g1Row, g1Column];
        tiles[g1Row, g1Column] = tiles[g2Row, g2Column];    //Assign tile 1 to tile 2
        tiles[g2Row, g2Column] = temp;                      //Assign tile 2 to tile 1 (stored)

        //Swap Tile stored values
        Tile.SwapRowColumn(g1Tile, g2Tile);
    }

    public void UndoSwap()
    {
        Swap(object1, object2);
    }

    private IEnumerable<GameObject> GetMatchesHorizontally(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(go);

        var goTile = go.GetComponent<Tile>();

        if (goTile.Column != 0) //Search Left
        {
            for (int column = goTile.Column - 1; column >= 0; column--)     //Check everything left of the tile
            {
                if (tiles[goTile.Row, column].GetComponent<Tile>().IsSameType(goTile))  //Check if it matches the type
                {
                    matches.Add(tiles[goTile.Row, column]);
                }
                else break; //early exit
            }
        }

        if (goTile.Column != GameVariables.Columns - 1) // Search Right
        {
            for (int column = goTile.Column + 1; column < GameVariables.Columns; column++)  //Check everything right of the tile
            {
                if (tiles[goTile.Row, column].GetComponent<Tile>().IsSameType(goTile))  //Check if it matches the type
                {
                    matches.Add(tiles[goTile.Row, column]);
                }
                else break; //early exit
            }
        }

        if (matches.Count < GameVariables.MinimumMatches)
        {
            matches.Clear();
        }

        return matches.Distinct();
    }

    private IEnumerable<GameObject> GetMatchesVertically(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(go);

        var goTile = go.GetComponent<Tile>();


        if (goTile.Row != 0) //Search Down
        {
            for (int row = goTile.Row - 1; row >= 0; row--)     //Check everything left of the tile
            {
                if (tiles[row, goTile.Column].GetComponent<Tile>().IsSameType(goTile))  //Check if it matches the type
                {
                    matches.Add(tiles[row, goTile.Column]);
                }
                else break; //early exit
            }
        }

        if (goTile.Row != GameVariables.Rows - 1) // Search Up
        {
            for (int row = goTile.Row + 1; row < GameVariables.Rows; row++)  //Check everything right of the tile
            {
                if (tiles[row, goTile.Column].GetComponent<Tile>().IsSameType(goTile))  //Check if it matches the type
                {
                    matches.Add(tiles[row, goTile.Column]);
                }
                else break; //early exit
            }
        }


        if (matches.Count < GameVariables.MinimumMatches)
        {
            matches.Clear();
        }

        return matches.Distinct();
    }

    private bool ContainsDestroyWholeRowColumnBonus(List<GameObject> matches)
    {
        if(matches.Count >= GameVariables.MinimumMatches)
        {
            foreach (var item in matches)
            {
                if (BonusTiles.ContainsDestroyWholeRowColumn(item.GetComponent<Tile>().Bonus))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private List<GameObject> GetEntireRow( GameObject tile )
    {
        List<GameObject> matches = new List<GameObject>();

        int row = tile.GetComponent<Tile>().Row;
        for ( int column = 0; column < GameVariables.Columns; column++)
        {
            matches.Add(tiles[row, column]);
        }

        return matches;
    }

    private List<GameObject> GetEntireColumn(GameObject tile)
    {
        List<GameObject> matches = new List<GameObject>();

        int column = tile.GetComponent<Tile>().Column;
        for (int row = 0; row < GameVariables.Rows; row++)
        {
            matches.Add(tiles[row, column]);
        }

        return matches;
    }

    public void Remove(GameObject tile)
    {
        tiles[tile.GetComponent<Tile>().Row, tile.GetComponent<Tile>().Column] = null;
    }

    public AlteredTileInfo Collapse(IEnumerable<int> columns)
    {
        AlteredTileInfo collapseInfo = new AlteredTileInfo();

        foreach( var column in columns )
        {
            for( int row = 0; row < GameVariables.Rows - 1; row++)
            {
                if(tiles[row, column] == null)
                {
                    for(int row2 = row + 1; row2 < GameVariables.Rows; row2++)
                    {
                        if(tiles[row2, column] != null)
                        {
                            tiles[row, column] = tiles[row2, column];
                            tiles[row2, column] = null;

                            if(row2 - row > collapseInfo.maxDistance)
                            {
                                collapseInfo.maxDistance = row2 - row;
                            }
                               
                            tiles[row, column].GetComponent<Tile>().Row = row;
                            tiles[row, column].GetComponent<Tile>().Column = column;

                            collapseInfo.AddTile(tiles[row, column]);
                            break; 
                        }
                    }
                    

                }
            }
        }

        return collapseInfo;
    }

    public IEnumerable<EmptyTileInfo> GetEmptyItemsOnColumn(int column)
    {
        List<EmptyTileInfo> emptyItems = new List<EmptyTileInfo>();

        for (int row = 0; row < GameVariables.Rows; row++)
        {
            if(tiles[row,column] == null)
            {
                emptyItems.Add(new EmptyTileInfo() { Row = row, Column = column });
            }
        }

        return emptyItems;
    }



}
