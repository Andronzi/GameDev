using UnityEngine;
using Random = UnityEngine.Random;

namespace Generation
{
   public class RoomSpawn : MonoBehaviour
   {
      public Direction direction;
   
      public enum Direction
      {
         Top,
         Bottom,
         Left,
         Right,
         None,
      }

      private RoomVariants _variants;
      private int _rand;
      private bool spawnd;
      private float waitTime = 3f;
      private void Start()
      {
         _variants = GameObject.FindWithTag("Rooms").GetComponent<RoomVariants>();
         Destroy(gameObject,waitTime);
         Invoke("Spawn",0.2f);
      }

      public void Spawn()
      {
         if (!spawnd)
         {
            if (direction == Direction.Top)
            {
               _rand = Random.Range(0, _variants.topRooms.Length);
               Instantiate(_variants.topRooms[_rand], transform.position, _variants.topRooms[_rand].transform.rotation);
            }
            else if (direction == Direction.Bottom)
            {
               _rand = Random.Range(0, _variants.bottomRooms.Length);
               Instantiate(_variants.bottomRooms[_rand], transform.position, _variants.bottomRooms[_rand].transform.rotation);
            }
            else if (direction == Direction.Right)
            {
               _rand = Random.Range(0, _variants.rightRooms.Length);
               Instantiate(_variants.rightRooms[_rand], transform.position, _variants.rightRooms[_rand].transform.rotation);
            }
            else if (direction == Direction.Left)
            {
               _rand = Random.Range(0, _variants.leftRooms.Length);
               Instantiate(_variants.leftRooms[_rand], transform.position, _variants.leftRooms[_rand].transform.rotation);
            }

            spawnd = true;

         }
      }

      private void OnTriggerStay2D(Collider2D other)
      {
         if (other.CompareTag("RoomPoint") && other.GetComponent<RoomSpawn>().spawnd)
         {
            Destroy(gameObject);
         }
      }
   }
}
