using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float maxHealth = 100;
    [SerializeField]
    public float currentHealth;

    public Stat damage;
    public Stat armour;
    public Stat bluntResistance;

    private void Awake()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damage, string dealerTag) {

        //If the damage dealer isn't a spell (aka a flying object), check that it's travelling faster than blunt resistance, otherwise just reduce armour.
        if (dealerTag != "Spell") {
            if (damage > bluntResistance.getValue())
            {
                damage -= armour.getValue();
            }
            else {
                damage = 0f;
            }
            
        }
        else {
            damage -= armour.getValue();
        }

        
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " from " + dealerTag);
        if (currentHealth <= 0) {
            die();
        }
    }

    public virtual void die() {
        // die in some way
        //this method is meant to be overwritten
        Debug.Log(transform.name + " died.");
    }
}
