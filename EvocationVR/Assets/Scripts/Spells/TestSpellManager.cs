using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpellManager : SpellManager
{
    public float velocity;

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

    public override void setUp(GameObject endOfWandObject, GameObject wandObject)
    {
        //set position of bullet
        Vector3 wandPos = endOfWandObject.transform.position;
        Vector3 wandDirection = endOfWandObject.transform.forward;
        Quaternion wandRotation = endOfWandObject.transform.rotation;
        Vector3 spawnPos = wandPos;

        this.gameObject.transform.position = spawnPos;

        this.gameObject.transform.rotation = wandRotation;

        this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.transform.TransformDirection(new Vector3(0, velocity, 0));

        this.gameObject.SetActive(true);
    }



    public override void pushCollision(Collision other)
    {
        base.pushCollision(other);
        applyForcePush(other);

    }

    void applyForcePush(Collision other)
    {
        print("test");
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 center = transform.position;
        Vector3 contactedAt = transform.GetComponent<SphereCollider>().ClosestPointOnBounds(other.transform.position);
        Vector3 pushedDirection = (gameObject.GetComponent<Rigidbody>().velocity).normalized;


        other.gameObject.GetComponent<Rigidbody>().AddForce(pushedDirection * force, ForceMode.Impulse);


        //Vector3 center = transform.position;
        //Vector3 contactedAt = transform.GetComponent<SphereCollider>().ClosestPointOnBounds(other.transform.position);
        //Vector3 pushedDirection = (gameObject.GetComponent<Rigidbody>().velocity).normalized;
        //Debug.Log("center: " + center);
        //Debug.Log("contacted at: " + contactedAt);

        //other.gameObject.GetComponent<Rigidbody>().velocity = (pushedDirection * force) / other.gameObject.GetComponent<Rigidbody>().mass;

    }
}
