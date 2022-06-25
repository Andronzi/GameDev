using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Magaz : MonoBehaviour
{
    public int money = Player.playerMoney;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI inventory;
 

    public void additem(string item)
    {
        moneyText.text = Player.playerMoney.ToString();
        inventory.text += "\n" +  item;
    }

}
