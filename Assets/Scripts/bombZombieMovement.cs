using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class bombZombieMovement : ZombieMovement
{
    public float bombDamage = 30.0f;
    public float bombSize = 3.0f;

    public override void onCharDeath()
    {
        explode();
        base.onCharDeath();
    }

    private void explode()
    {
        Collider2D[] storedHits = Physics2D.OverlapCircleAll(transform.position, bombSize);
        foreach(Collider2D hit in storedHits)
        {
            if (hit.CompareTag("defense"))
            {
                
                axe_knight_attack axeKnightComponent = hit.GetComponent<axe_knight_attack>();
                GameOverObject gameEnder = hit.GetComponent<GameOverObject>();
                temporaryDefense defense = hit.GetComponent<temporaryDefense>();

                if(defense != null ) {
                    defense.takeDamage(bombDamage);

                }else if(axeKnightComponent != null )
                {
                    axeKnightComponent.TakeDamage(bombDamage);
                }
            }
        }
    }
}
