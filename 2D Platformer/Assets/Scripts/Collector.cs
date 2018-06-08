using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CollectableType { POLLEN, HONEYCOMB}

public class Collector : MonoBehaviour {
    
    //Text counts of number of colletable
    public Text pollenText; 
    public Text honeyText; 

    int pollenCount;
    int honeyCount;

    void OnTriggerEnter2D(Collider2D item)
    {
        Collectable collect;

        collect = item.GetComponent<Collectable>();

        if(collect != null)
        {
            switch(collect.type)
            {
                case CollectableType.POLLEN:
                    pollenCount += collect.Collected();
                    pollenText.text = pollenCount.ToString();
                    break;
                case CollectableType.HONEYCOMB:
                    honeyCount += collect.Collected();
                    honeyText.text = honeyCount.ToString();
                    break;
            }
        }
    }
}
