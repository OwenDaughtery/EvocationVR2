using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public float force = 1f;
    public int damage = 1;
    public float maxLifeTime;
    public float currentLifeTime;
    public string tag;
    [SerializeField]
    public List<string> tagsToIngore = new List<string>();
    public List<int> layersToIgnore = new List<int>();

    //public ObjectPoolerManager objectPool = ObjectPoolerManager.SharedInstance;

    private void OnEnable()
    {
        currentLifeTime = 0f;

    }

    // Start is called before the first frame update
    void Start()
    {
        tag = this.gameObject.transform.tag;
    }

    // Update is called once per frame
    public void Update()
    {
        checkActiveTime();
    }

    public virtual void deactivate() {
        //Debug.Log("deactivating spell " + transform.name);
        ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, transform.tag);
    }

    private void checkActiveTime() {

        /*print(Time.deltaTime);
        print(timeActivated + lifeTime);
        print("");*/
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime>=maxLifeTime) {
            //StartCoroutine(collided());
            deactivate();
            //ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, "TestSpell");

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        
        //if the item is destructible
        if (other.gameObject.layer==10) {
            //Debug.Log(other.transform.name);
            pushCollision(other);
            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, int.MaxValue);

        }

        //if the item is an enemy
        if (other.gameObject.layer == 13) {
            //Debug.Log(other.transform.name);
            pushCollision(other);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage, transform.gameObject.name);
        }

        //if the item is something that should destroy the spell
        if (!tagsToIngore.Contains(other.gameObject.tag)) {
            //Debug.Log(other.transform.name);
            if (!layersToIgnore.Contains(other.gameObject.layer)) {
                //pushCollision(other);
                //StartCoroutine(collided());
                deactivate();
            }
            
        }
        
    }

    public virtual void pushCollision(Collision other) {
        Debug.Log("Trigger collided with collision at: " + other.transform.position + " with name: " + other.transform.name);
    }

    public virtual void pushCollider(Collider other) {
        Debug.Log("Trigger collided with collider at: " + other.transform.position + " with name: " + other.transform.name);
    }

    private void OnTriggerEnter(Collider other)
    {


        //if the item is destructible
        if (other.gameObject.layer == 10)
        {

            other.gameObject.GetComponent<DestructibleManager>().reduceHealth(damage, int.MaxValue);//hard coded as max value to get past hardnesses.
            pushCollider(other);
        }
        //Or the item is on layer object
         if (other.gameObject.layer == 12)
        {
 
            pushCollider(other);
        }

        //if the item is an enemy
        if (other.gameObject.layer == 13)
        {

            pushCollider(other);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage, transform.gameObject.name);
        }

        //if the item is an enemy
        if (other.gameObject.layer == 13)
        {
            //Debug.Log(other.transform.name);
            pushCollider(other);
            other.gameObject.GetComponent<CharacterStats>().TakeDamage(damage, transform.gameObject.name);
        }

        //something that should destroy the spells
        if (!tagsToIngore.Contains(other.gameObject.tag)) {
            if (!layersToIgnore.Contains(other.gameObject.layer)) {

                //pushCollider(other);
                //StartCoroutine(collided());
                deactivate();
            }
            
        }
    }

    /*IEnumerator collided(){
        yield return new WaitForEndOfFrame();
        if (gameObject.GetComponent<MeshRenderer>().enabled) {

        
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            int numberOfChildren = this.transform.childCount;
            for (int i = 0; i < numberOfChildren; i++)
            {
                GameObject child = this.transform.GetChild(i).gameObject;
                child.SetActive(false);
            }
            yield return new WaitForEndOfFrame();
            ObjectPoolerManager.SharedInstance.returnToPool(this.gameObject, transform.tag);
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            for (int i = 0; i < numberOfChildren; i++)
            {
                GameObject child = this.transform.GetChild(i).gameObject;
                child.SetActive(true);
            }

            deactivated();
            
        }
        StopCoroutine(collided());
    }*/


}
