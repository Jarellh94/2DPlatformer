using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipableType { GUN, MELEE, ARMOR }

//Class placed on player to handle picking up equipable items.

public class Equip : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D item)
    {
        Equipable equipped = item.GetComponent<Equipable>();

        if(equipped != null)
        {
            switch (equipped.myType)
            {
                case EquipableType.GUN:
                    Gun playerGun = GetComponentInChildren<Gun>();
                    playerGun.gameObject.SetActive(true);
                    playerGun.SetProjectile(equipped.myItem);
                    playerGun.SetSprite(equipped.Collected());
                    playerGun.SetFirePoint(equipped.firePoint.localPosition);
                    break;
                case EquipableType.MELEE:
                    break;
                case EquipableType.ARMOR:
                    PlayerHealth health = GetComponent<PlayerHealth>();
                    health.ArmorUp(1);
                    equipped.Collected();
                    break;
            }
        }
    }
}
