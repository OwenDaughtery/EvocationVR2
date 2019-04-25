using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpellManager : SpellManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void deactivate()
    {
        base.deactivate();
        
    }


    public override void pushCollision(Collision other)
    {
        base.pushCollision(other);
        applyForcePush(other);

    }

    void applyForcePush(Collision other)
    {

        //Vector3 center = transform.position;
        //Vector3 contactedAt = transform.GetComponent<SphereCollider>().ClosestPointOnBounds(other.transform.position);
        //Vector3 pushedDirection = (gameObject.GetComponent<Rigidbody>().velocity).normalized;
        //Debug.Log("center: " + center);
        //Debug.Log("contacted at: " + contactedAt);

        //other.gameObject.GetComponent<Rigidbody>().velocity = (pushedDirection * force) / other.gameObject.GetComponent<Rigidbody>().mass;

    }
}
