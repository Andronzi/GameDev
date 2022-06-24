
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    public GameObject blocks;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Block"))
        {
            Instantiate(blocks, transform.GetChild(0).position, Quaternion.identity);
            Instantiate(blocks, transform.GetChild(1).position, Quaternion.identity);
            Destroy(gameObject);
        }
        
    }
    
}
