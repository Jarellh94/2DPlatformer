using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CollectableType { POLLEN, HONEYCOMB}

public class Collector : MonoBehaviour {
    
    //Text counts of number of colletable

    int pollenCount = 0;
    int honeyCount = 0;

    PlayerUIManager playerUi;

    void Awake()
    {
        playerUi = GetComponent<PlayerUIManager>();
        playerUi.UpdatePollenCount(pollenCount);
        playerUi.UpdateHoneyCount(honeyCount);
    }

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
                    //pollenText.text = pollenCount.ToString();
                    playerUi.UpdatePollenCount(pollenCount);
                    break;
                case CollectableType.HONEYCOMB:
                    honeyCount += collect.Collected();
                    //honeyText.text = honeyCount.ToString();
                    playerUi.UpdateHoneyCount(honeyCount);
                    break;
            }
        }
    }

    public int GetPollenCount()
    {
        return pollenCount;
    }

    public int GetHoneyCount()
    {
        return honeyCount;
    }
}
