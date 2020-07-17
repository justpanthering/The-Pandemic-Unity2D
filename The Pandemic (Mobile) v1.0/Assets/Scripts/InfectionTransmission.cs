using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionTransmission : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger");
        //For NPC
        if(collision.CompareTag("NPC"))
        {
            Debug.Log("NPC in contact");

            //collision.GetComponent<NPCTraits>()._isInfected
            //Will throw a runtime error, because GetComponent<T> gets a component that is attached to a gameobject. Since NPCTraits does not inherit monoehaviour and is not attached to NPC gameobjects, it will throw an error
            //Alternate method: call function in NPCControl of the infectee (vs infector)

            collision.GetComponent<NPCControl>().Infection(GetComponentInParent<NPCControl>().getSpreadChance());
        }
        //For Player
        else if(collision.CompareTag("Player"))
        {
            Debug.Log("Player in contact");
            collision.GetComponent<PlayerControl>().Infection(GetComponentInParent<NPCControl>().getSpreadChance());
        }
    }
}
