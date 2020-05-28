using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCity
{
    public class FlatTile : MonoBehaviour
    {

        public Transform tilePrefab;
        private MyCityManager cityManager;
     
        // Start is called before the first frame update

        
        void Start()
        {
            int x = Mathf.RoundToInt(transform.position.x + 20.01f); //these values are used in checkSlot and SetSlot from MyCityManager
            int y = Mathf.RoundToInt(transform.position.y);
            int z = Mathf.RoundToInt(transform.position.z + 20.01f);
            cityManager = MyCityManager.Instance; // static function
            //ROAD

            if (!cityManager.CheckSlot(x, y, z))
            {
            
              
                cityManager.SetSlot(x, y, z, false); //making the free spaces false becuase we're putting a block there
                for (int i = 0; i < 10; i--)
                {
                    Instantiate(tilePrefab, new Vector3(0, 0, i), Quaternion.identity, this.transform);
                }
            }
            else
            {
                //if a slot in the 280,280,280 array is being used then..
                Destroy(gameObject); //destroying any object thats already there
            }

          
        }


    }
}