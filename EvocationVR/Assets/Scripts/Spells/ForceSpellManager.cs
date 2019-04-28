using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class ForceSpellManager : SpellManager
{

    
    float growthRate = 0.4f;
    Vector3 originalScale;
    

    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }

    private void Awake()
    {
        originalScale = transform.lossyScale;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        expandForceField(growthRate);
        updatePosition();
    }

    public override void setUp(GameObject endOfWandObject, GameObject wandObject)
    {
        base.setUp(endOfWandObject, wandObject);
        Vector3 spawnPos = wandObject.transform.parent.transform.position;
        this.gameObject.transform.position = spawnPos;
        this.gameObject.SetActive(true);
    }

    public override void deactivate()
    {
        base.deactivate();
        transform.localScale = originalScale;
    }

    public override void pushCollider(Collider other)
    {
        base.pushCollider(other);
        applyForcePush(other);
        
    }

    void applyForcePush(Collider other) {

        

        Vector3 center = transform.position;
        Vector3 contactedAt = transform.GetComponent<CapsuleCollider>().ClosestPointOnBounds(other.transform.position);
        Vector3 pushedDirection = (contactedAt - center).normalized;
        other.GetComponent<Rigidbody>().AddForce(pushedDirection *  force, ForceMode.Impulse);
        //Debug.Log("center: " + center);
        //Debug.Log("contacted at: " + contactedAt);

        //other.gameObject.GetComponent<Rigidbody>().velocity = (pushedDirection * force)/other.gameObject.GetComponent<Rigidbody>().mass;


    }



    void expandForceField(float growthRate) {
        transform.localScale += new Vector3(1f,0f,1f) * growthRate;
    }

    void updatePosition() {
        transform.position = PlayerManager.instance.player.transform.position;
    }
}
