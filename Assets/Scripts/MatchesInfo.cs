using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchesInfo
{
    private List<GameObject> matches;
    private BonusType BonusesContained { get; set; }

    public MatchesInfo()
    {
        matches = new List<GameObject>();
        BonusesContained = BonusType.None;
    }

    //public IEnumerable<GameObject> MatchedCandy 
    //{
    //    get {

    //    }
    //}
}
