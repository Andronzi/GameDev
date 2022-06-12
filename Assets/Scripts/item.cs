using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public int cost;
    public string itemName;

    public void Buy()
    {
        if (GetComponentInParent<Magaz>().money >= cost)
        {
            GetComponentInParent<Magaz>().money -= cost;
            GetComponentInParent<Magaz>().additem(itemName);
        }
    }
}
