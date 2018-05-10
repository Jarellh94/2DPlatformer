using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CollectableType { POLLEN, HONEYCOMB}

public class Collector : MonoBehaviour {

    public Text pollenText;

    int pollenCount;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
                    pollenText.text = pollenCount.ToString();
                    break;
            }
        }
    }
}
