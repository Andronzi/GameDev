
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int healh;

    public float[] position;

    public PlayerData(Player player)
    {
        healh = player.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
        

    }

}