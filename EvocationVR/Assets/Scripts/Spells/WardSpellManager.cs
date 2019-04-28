using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WardSpellManager : SpellManager
{
 

    public override void setUp(GameObject endOfWandObject, GameObject wandObject)
    {
        base.setUp(endOfWandObject, wandObject);
        //set position
        Vector3 wandPos = endOfWandObject.transform.position;
        Vector3 wandDirection = endOfWandObject.transform.forward;
        float direction = wandObject.transform.eulerAngles.y + 90f;
        Vector3 spawnPos = wandPos;

        this.gameObject.transform.position = spawnPos;

        this.gameObject.transform.localEulerAngles = new Vector3(0f, direction, 0f);
        this.gameObject.SetActive(true);
    }
}
