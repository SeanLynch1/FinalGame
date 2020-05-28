using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCity
{
    public class MyCityManager : MonoBehaviour
    {
        #region Fields
        private static MyCityManager _instance;

        public Transform tilePrefab;
        public Transform slopeUpPrefab;
        public Transform slopeDownPrefab;
        public BuildingProfile wallProfile;
        public BuildingProfile towerProfile;
        public GameObject buildingPrefab;
        public GameObject complexPrefab;
        public Transform gridVisPrefab;
        public BuildingProfile[] gameProfileArray;
        public GameObject[] particleEffectPrefab;
        public GameObject roadPrefab;
        public Transform startLocation;
        public Transform point;
        public Transform location;
        private bool[,,] cityArray = new bool[280, 280, 280];
        //increased array size to allow for larger city volume

        public static MyCityManager Instance
        {
            get
            {
                return _instance;
            }
        }
        #endregion

        #region Methods
        #region Unity Methods

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            else
            {
                Destroy(gameObject);
                Debug.LogError("Multiple NavigationCityManager instances in Scene. Destroying clone!");
            };
        }
        void Start()
        {
            firstCity();
            testBuild();
            cityGap();



        }
        #endregion
        //checks if there are slots out of the array
        public bool CheckSlot(int x, int y, int z)
        {
            if (x < 0 || x > 280 || y < 0 || y > 280 || z < 0 || z > 280) return false; //This means the value extends the size of our array
            else
            {
                return cityArray[x, y, z]; 
            }

        }

        //checks if slots are occupied
        //if so a orange box is built around them
        public void SetSlot(int x, int y, int z, bool occupied)
        {
            if (!(x < 0 || x > 280 || y < 0 || y > 280 || z < 0 || z > 280)) //if its in this grid
            {
                cityArray[x, y, z] = occupied;// its occupied
                if (occupied)
                {
                    Instantiate(gridVisPrefab, new Vector3(x -20, y, z -20), Quaternion.identity, SpaceController.Instance.dummyPivot); //bui;ds a box on FREE spaces
                }
            }

        }
        public void SetGap(int x, int y, int z, bool occupied)
        {
            if (!(x < 0 || x > 280 || y < 0 || y > 280 || z < 0 || z > 280)) //if its in this grid
            {
                cityArray[x, y, z] = occupied;// its occupied
                if (occupied)
                {
                    Instantiate(gridVisPrefab, new Vector3(x - 3 , y, z - 68), Quaternion.identity, SpaceController.Instance.dummyPivot); //bui;ds a box on FREE spaces
                }
            }

        }
        public void BuildTowers(int numTowers)
        {
            for (int i = 0; i < 300; i++)
            {
                int RandomX = Random.Range(-40, 40);
                int RandomZ = Random.Range(-40, 40);
                int RandomY = Random.Range(70, 90);
                int random = Random.Range(0, gameProfileArray.Length);
                if (!((RandomX > -5) && (RandomX < 5) && (RandomZ > -5) && (RandomZ < 5)))
                {
                    Instantiate(buildingPrefab, new Vector3(RandomX, RandomY, RandomZ), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(gameProfileArray[random]); //Instantiate a random tower from profile array in game controller
                }

            }
           
        }
        
        public void cityGap()
        {
           // Debug.Log("Setting Gap");
            for (int ix = 0; ix < 7; ix++)
            {
                for (int iy = 100; iy < 106; iy++)
                {
                    SetGap(ix, iy, 0, true);
                  
                   
                }
                
            }
            

        }


        public void testBuild()
        {
            for (int j = -150; j < -3; j += 1)
            {
                complexPrefab.transform.localScale = new Vector3(3, 1, 1);
                
                Instantiate(complexPrefab, new Vector3(0, 100, j), Quaternion.identity);
            }
        }
        public void BuildRoads()
        {
            Instantiate(roadPrefab, startLocation);
        }
        public void firstCity()
        {
          
          
            //CITY CROWN
            for (int i = -87; i < 88; i += 174)
            {
                //first pitch
                for (int j = -86; j < -7; j += 1)
                {
                    Instantiate(buildingPrefab, new Vector3(i, j + 86, j), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(wallProfile);
                    Instantiate(particleEffectPrefab[0], new Vector3(i, j + 86, j), Quaternion.identity);
                }
                for (int j = -8; j < 9; j++)
                {
                    Instantiate(buildingPrefab, new Vector3(i, 78, j), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(wallProfile);
                    Instantiate(particleEffectPrefab[0], new Vector3(i, 78, j), Quaternion.identity);
                }
                for (int j = 8; j < 87; j++)
                {
                    Instantiate(buildingPrefab, new Vector3(i, 86 - j, j), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(wallProfile);
                    Instantiate(particleEffectPrefab[0], new Vector3(i, 86 - j, j), Quaternion.identity);
                }
                //second pitch

                for (int j = -87; j < -7; j += 1)
                {
                    Instantiate(buildingPrefab, new Vector3(j, j + 86, i), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(wallProfile);
                    Instantiate(particleEffectPrefab[0], new Vector3(j, j + 86, i), Quaternion.identity);
                }
                for (int j = -8; j < 9; j++)
                {
                    Instantiate(buildingPrefab, new Vector3(j, 78, i), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(wallProfile);
                    Instantiate(particleEffectPrefab[0], new Vector3(j, 78, i), Quaternion.identity);
                }
                for (int j = 8; j < 87; j++)
                {
                    Instantiate(buildingPrefab, new Vector3(j, 86 - j, i), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(wallProfile);
                    Instantiate(particleEffectPrefab[0], new Vector3(j, 86 - j, i), Quaternion.identity);
                }
            }
            //BUILDING SQUARE
            
                for (int i = -67; i < 68; i += 134)
                {
                    for (int j = -66; j < 67; j += 2)
                    {
                        Instantiate(buildingPrefab, new Vector3(i, 100, j), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(towerProfile);
                        Instantiate(particleEffectPrefab[1], new Vector3(i, 98, j), Quaternion.identity);
                    }
                    for (int j = -67; j < 68; j += 2)
                    {
                        Instantiate(buildingPrefab, new Vector3(j, 100, i - 1), Quaternion.identity).GetComponent<SpaceTowerBlock>().SetProfile(towerProfile);
                        Instantiate(particleEffectPrefab[1], new Vector3(j, 98, i), Quaternion.identity);
                    }
                }

       

           
        }
    }
   
    #endregion

}
