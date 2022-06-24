using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponMenager : MonoBehaviour
{
    [SerializeField] private List<Weapson_Slot> _slotPref;
    [SerializeField] private Weapon _weaponPref;
    [SerializeField] private Transform _slotParent,_weaponParent;
    void Start()
    {
        Spawn();

    }

    void Spawn()
    {
        var randomSet = _slotPref.OrderBy(s => Random.value).Take(3).ToList();
        for (int i = 0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(randomSet[i], _slotParent.GetChild(i).position, Quaternion.identity);
            
            var spawnedWeapon = Instantiate(_weaponPref, _weaponParent.GetChild(i).position, Quaternion.identity);
            spawnedWeapon.init(spawnedSlot);
        }
    }
}
