using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poisonZombieMovement : ZombieMovement
{
    private GameObject defenseActive;
    public bool canPoison = true;
    public float poisonTime = 3.0f;
    public float poisonInterval = 1.0f;
    public float poisonDamage = 4.0f;

    // this function is called when the enemy dies and is being used to start the poison coroutine on death
    // it works by overriding the onCharDeath function from the parent class and adds the start poison coroutine
    public override void onCharDeath()
    {
        if(canPoison)
        {
            StartCoroutine(shouldPoison(defenseActive));
        }
        
 
        base.onCharDeath();
    }

    // this function is a coroiutine that runs when the enemy is attacking
    // it works by overriding the attack coroutine from the parent class and adding a poison effect
    // while the enemy is attacking and the defense is there then it should do the regular damage but also start the poison effect coroutine
    public override IEnumerator attack(GameObject defense)
    {
        defenseActive = defense;
        while (attacking && defense != null)
        {
            shouldDamage(defense, damageDealt);
           

            yield return new WaitForSeconds(1.0f);
        }
    }

    // this function is another coroutine that applies poison damage over time
    // it keeps track of the time and while the time is less than the poison time and the defense exists 
    // if the time is less than the poison time and the defense exists then apply poison damage and increase interval time
    private IEnumerator shouldPoison(GameObject defense)
    {
        float activeTime = 0.0f;
        while(activeTime < poisonTime && defense != null) {
            shouldDamage(defense, poisonDamage);
            yield return new WaitForSeconds(poisonInterval);
            activeTime += poisonInterval;
        }
    }

    // this function is used as a helper function to deal damage to the defenses
    // it gets the damage components
    // then if the components are in range of the enemy do damage
    private void shouldDamage(GameObject defense, float damage)
    {
        axe_knight_attack axeKnightComponent = defense.GetComponent<axe_knight_attack>();
        temporaryDefense defenseComp = defense.GetComponent<temporaryDefense>();

        if(defenseComp != null )
        {
            defenseComp.takeDamage(damage );

        } else if(axeKnightComponent != null )
        {
            axeKnightComponent.TakeDamage(damage);
        } 
    }
}
