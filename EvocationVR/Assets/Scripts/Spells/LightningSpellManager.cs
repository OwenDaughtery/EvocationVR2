using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpellManager : SpellManager
{
    public int maxJumps;
    private int currentJumps = 0;
    public float maxRange;
    private List<GameObject> avoidTargets;
    public LineRenderer lineRenderer;

    public void Start()
    {
        
    }

    public void OnEnable()
    {
        avoidTargets = new List<GameObject>();
    }

    public void Update()
    {
        base.Update();
        //lineRenderer.SetPosition(0, this.gameObject.transform.position);
    }

    public override void setUp(GameObject endOfWandObject, GameObject wandObject)
    {
        base.setUp(endOfWandObject, wandObject);
        //set position
        Vector3 wandPos = endOfWandObject.transform.position;
        Vector3 wandDirection = endOfWandObject.transform.forward;
        Vector3 spawnPos = wandPos;

        this.gameObject.transform.position = spawnPos;

        this.gameObject.SetActive(true);

        
        jumpTarget(this.gameObject);
    }

    public override void deactivate()
    {
        resetLineRenderer();
        currentLifeTime = 0f;
        base.deactivate();
        
    }

    public void jumpTarget(GameObject currentGameObject) {
        lineRenderer.positionCount += 1;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, currentGameObject.transform.position);

        if (currentJumps < maxJumps)
        {
            avoidTargets.Add(currentGameObject);
  

            currentJumps += 1;
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject closestTarget = null;
            float distance;
            foreach (GameObject target in targets)
            {
                if (!avoidTargets.Contains(target))
                {
                    distance = Vector3.Distance(target.transform.position, currentGameObject.transform.position);
                    if (distance <= maxRange)
                    {
                        if (!closestTarget)
                        {
                            closestTarget = target;
                        }
                        else if (distance <= Vector3.Distance(closestTarget.transform.position, currentGameObject.transform.position))
                        {
                            closestTarget = target;
                        }

                    }
                }
            }

            //check if closest target is in front of player
            //jump from closest target to another target
            if (closestTarget)
            {
                //Debug.Log("lightning strikes target at " + closestTarget.transform.position);


                closestTarget.GetComponent<CharacterStats>().TakeDamage(damage, transform.gameObject.tag);
                jumpTarget(closestTarget);
            }
            else {
                currentJumps = 0;
            }
        }
        else {
            currentJumps = 0;

        }
    }

    public void resetLineRenderer() {
        lineRenderer.positionCount = 0;
        
    }
}
