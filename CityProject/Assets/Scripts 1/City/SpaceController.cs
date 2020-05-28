using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCity
{
    public class SpaceController : MonoBehaviour
    {
        #region Fields
        public Transform dummyPivot;
        public MyCityManager cityManager;
        private static SpaceController _instance;

        //Environment
        public Material[] skyMatArray;
        public Material[] floorMatArray;
        public Renderer floorRenderer;
        public int previousSkyIndex = 0;
        public int previousFloorIndex = 0;
        public BuildingProfile[] profileLibraryArray;
        #endregion

        #region Properties	
        public static SpaceController Instance
        {
            get
            {
                return _instance;
            }
        }

        #endregion

        #region Methods
        #region Unity Methods

        void Start()
        {
            //randomize seed
            Random.InitState(System.Environment.TickCount);
            //Start our game loop coroutine:
            StartCoroutine(GameLoop());
        }

        private void Awake()
        {
            

            if (_instance == null)
            {
                _instance = this;
            }

            else
            {
                Destroy(gameObject);
                Debug.LogError("Multiple GameController instances in Scene. Destroying clone!");
            };
        }


        #endregion
        IEnumerator GameLoop()
        {
            //We start with Game State "Title" (see _gameState initialization)
            Random.InitState(System.Environment.TickCount);
           

            //Randomize game environment for a new game:
            RandomizeStage();
            RandomizeTowerFlavours();
          
            yield return new WaitForSeconds(2f);
            cityManager.BuildTowers(Random.Range(25, 45));
            yield return new WaitForSeconds(2f);
            cityManager.BuildRoads();
            
          


        }


        public void RandomizeStage()
        {
            Material newSkyMaterial;
            bool skySuccess = false;
            while (!skySuccess) //keep 'throwing the die' until sky index is different than the previous one 
            {
                int skyIndex = Random.Range(0, skyMatArray.Length);
                if (skyIndex != previousSkyIndex)
                {
                    newSkyMaterial = skyMatArray[skyIndex];
                    previousSkyIndex = skyIndex;
                    RenderSettings.skybox = newSkyMaterial;
                    skySuccess = true; //we can exit the while loop now
                }
            }

            Material newFloorMaterial;
            bool floorSuccess = false;
            while (!floorSuccess) //keep 'throwing the die' until floor index is different than the previous one 
            {
                int floorIndex = Random.Range(0, floorMatArray.Length);
                if (floorIndex != previousFloorIndex)
                {
                    newFloorMaterial = floorMatArray[floorIndex];
                    previousFloorIndex = floorIndex;
                    floorRenderer.material = newFloorMaterial;
                    floorSuccess = true; //we can exit the while loop now
                }
            }
        }

        public void RandomizeTowerFlavours()
        {
            int arraySize = Random.Range(1, profileLibraryArray.Length);
            BuildingProfile[] profileArray = new BuildingProfile[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                int rnd = Random.Range(0, profileLibraryArray.Length);
                profileArray[i] = profileLibraryArray[rnd];  //assigns each in indice in an array of tower profiles to be a random tower profile from profileLibraryArrays
            }
            cityManager.gameProfileArray = profileArray;
        }
        #endregion
    }
}
