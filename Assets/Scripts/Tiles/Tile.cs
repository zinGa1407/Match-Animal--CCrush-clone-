using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public BonusType Bonus { get; set; }

    public int Row { get; set; }
    public int Column { get; set; }

    //Which Type of Animal
    public string Type { get; set; }

    //public Tile()     //Constructor
    public void Awake()
    {
        Bonus = BonusType.None;
    }

    public bool IsSameType( Tile otherTile )
    {
        return string.Compare(this.Type, otherTile.Type) == 0;
    }

    public void Initialize(string type, int row, int column)
    {
        Type = type;
        Row = row;
        Column = column;
    }

    public static void SwapRowColumn( Tile t1, Tile t2 )
    {
        int temp = t1.Row;
        t1.Row = t2.Row;
        t2.Row = temp;

        temp = t1.Column;
        t1.Column = t2.Column;
        t2.Column = temp;
    }

}
