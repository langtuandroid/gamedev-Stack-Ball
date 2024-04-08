using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace multi_tower_attack_3d
{
    public class PlatformScript : MonoBehaviour
    {

        public List<MeshCollider> glassColliderList = new List<MeshCollider>();

        public bool isCalled = false;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ChangeColliderStatusTrue()
        {
            if (!isCalled)
            {
                for (int i = 0; i < glassColliderList.Count; i++)
                {
                    glassColliderList[i].isTrigger = true;
                }
                isCalled = true;
            }
        }

        public void ChangeColliderStatusFalse()
        {

            if (isCalled)
            {
                for (int i = 0; i < glassColliderList.Count; i++)
                {
                    glassColliderList[i].isTrigger = false;
                }
                isCalled = false;
            }

        }
    }
}
