using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Animator anim;
    bool flickering = true;
    public List<string> triggerNames; //trigger1, trigger2, trigger3
    public float minTime, maxTime; //chosen betwwen minTime and maxTime

    void Start()
    {
        StartCoroutine(flickerLights());
    }

    IEnumerator flickerLights()
    {
        while (flickering == true)
        
        {
            float randTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(randTime);
            
            string name = triggerNames[Random.Range(0, triggerNames.Count)];
            Debug.Log("Triggering: " + name);
            anim.SetTrigger(name);
            
        }
    }
}
