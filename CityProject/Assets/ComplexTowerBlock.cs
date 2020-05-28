﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyCity
{
    public class ComplexTowerBlock : MonoBehaviour
    {
        #region fields
        public BuildingProfile myProfile;
        public Transform basePrefab;
        public int recursionLevel = 0;
        private int maxLevel = 3;
        private MyCityManager cityManager;
        private Renderer myRenderer;
        private MeshFilter myMeshFilter;
        private Mesh myMesh;
        private Material myMaterial;
        #endregion

        #region Methods
        public void SetProfile(BuildingProfile profile)
        {
            myProfile = profile;
            maxLevel = myProfile.maxHeight;
        }
        // Start is called before the first frame update
        void Start()
        {
            int x = Mathf.RoundToInt(transform.position.x + 20.01f); //these values are used in checkSlot and SetSlot from MyCityManager
            int y = Mathf.RoundToInt(transform.position.y);
            int z = Mathf.RoundToInt(transform.position.z + 20.01f);
            cityManager = MyCityManager.Instance; // static function
            Transform child;

            if (recursionLevel == 0)
            {

                if (!cityManager.CheckSlot(x, y, z))
                {
                    int meshNum = myProfile.groundBlocks.Length;
                    int matNum = myProfile.groundMaterials.Length;
                    myMesh = myProfile.groundBlocks[Random.Range(0, meshNum)];
                    myMaterial = myProfile.groundMaterials[Random.Range(0, matNum)];
                    cityManager.SetSlot(x, y, z, false);
                }
                else
                {
                    //if a slot in the 280,280,280 array is being used then..
                    Destroy(gameObject);
                }
            }
            myMeshFilter.mesh = myMesh;
            myRenderer.material = myMaterial;
            if (recursionLevel < maxLevel)
            {
                if (recursionLevel == maxLevel - 1)
                {
                    if (!cityManager.CheckSlot(x, y + 1, z))
                    {
                        child = Instantiate(basePrefab, transform.position + Vector3.up * 1f, Quaternion.identity, this.transform);
                        int meshNum = myProfile.roofBlocks.Length;
                        int matNum = myProfile.roofMaterials.Length;
                        child.GetComponent<SpaceTowerBlock>().Initialize(recursionLevel + 1, myProfile.roofMaterials[Random.Range(0, matNum)], myProfile.roofBlocks[Random.Range(0, meshNum)]);

                        cityManager.SetSlot(x, y + 1, z, false);
                    }
                }
                else
                {
                    if (!cityManager.CheckSlot(x, y + 1, z))
                    {
                        child = Instantiate(basePrefab, transform.position + Vector3.up * 1f, Quaternion.identity, this.transform);
                        int meshNum = myProfile.mainBlocks.Length;
                        int matNum = myProfile.mainMaterials.Length;
                        child.GetComponent<SpaceTowerBlock>().Initialize(recursionLevel + 1, myProfile.mainMaterials[Random.Range(0, matNum)], myProfile.mainBlocks[Random.Range(0, meshNum)]);

                        cityManager.SetSlot(x, y + 1, z, false);
                    }
                }
            }
        }

    
        #endregion
    }
}