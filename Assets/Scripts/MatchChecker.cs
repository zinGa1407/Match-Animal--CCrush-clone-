using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker
{
    public static IEnumerator AnimatePotentialMatches(IEnumerable<GameObject> potentialMatches)
    {
        for (float i = 1.0f; i >= 0.3f; i -= 0.1f)
        {
            foreach(var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i;
                item.GetComponent<SpriteRenderer>().color = c;
            }
            yield return new WaitForSeconds(GameVariables.OpacityAnimationDelay);
        }

        for (float i = 0.3f; i <= 1.0f; i += 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i;
                item.GetComponent<SpriteRenderer>().color = c;
            }
            yield return new WaitForSeconds(GameVariables.OpacityAnimationDelay);
        }
    }

    public static bool AreHorizontalOrVerticalNeighbors( Tile t1, Tile t2 )
    {
        return (t1.Column == t2.Column || t1.Row == t2.Row) 
            && (Mathf.Abs(t1.Column - t2.Column) <= 1) 
            && (Mathf.Abs(t1.Row - t2.Row) <= 1);
    }



}
