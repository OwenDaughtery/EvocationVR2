using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandManager : MonoBehaviour
{

    public int speed;
    public Transform endOfWand;
    public GameObject camera;
    public float startAngleX;
    public Vector3 startAngle;
    public List<GameObject> activeSpells;
    public Dictionary<string, float> spellCosts = new Dictionary<string, float>();
    public ManaManager manaManager;

    // Start is called before the first frame update
    void Start()
    {
        startAngleX = this.transform.eulerAngles.x;
        startAngle = new Vector3(startAngleX, 0f, 0f);
        endOfWand = transform.GetChild(0);
        camera = GameObject.FindGameObjectWithTag("MainCamera");

        spellCosts.Add("TestSpell", 0.1f); //1
        spellCosts.Add("ArcSpell", 0.1f); //2 
        spellCosts.Add("WardSpell", 0.1f); //1.25
        spellCosts.Add("ForceSpell", 0.1f); //5
        spellCosts.Add("LightningSpell", 0.1f); //4
    }

    // Update is called once per frame
    void Update()
    {
        //print(camera.transform.eulerAngles.x);
        //this.transform.eulerAngles.Set(camera.transform.eulerAngles.x, 0f, 0f);
        this.transform.eulerAngles = camera.transform.eulerAngles + startAngle;
        anyKeyPressed();
    }

    private void anyKeyPressed() {
        if (Input.GetKeyDown("1"))
        {
            shootSpell("TestSpell");
        }
        /*if (Input.GetKeyDown("q")) {
            shootSpell("ArcSpell");
        }*/
        if (Input.GetKeyDown("2")){
            shootSpell("ForceSpell");
        }
        if (Input.GetKeyDown("3")) {
            shootSpell("LightningSpell");
        }
        if (Input.GetKeyDown("4"))
        {
            shootSpell("WardSpell");
        }
    }

    private void shootSpell(string tag)
    {
        if (manaManager.subtractMana(spellCosts[tag])) {
            GameObject spell = ObjectPoolerManager.SharedInstance.GetPooledObject(tag);
            //spell.transform.parent = this.gameObject.transform;
            if (spell) {
                spell.GetComponent<SpellManager>().setUp(endOfWand.gameObject, this.gameObject);
            }
        }
        


    }
}
