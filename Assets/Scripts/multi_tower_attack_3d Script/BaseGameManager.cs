using System;
using UnityEngine;
namespace multi_tower_attack_3d
{
    public class BaseGameManager<T> : MonoBehaviour where T : BaseGameManager<T>
    {
        private static T s_instance;

        public static T GetInstance()
        {
            if (s_instance == null)
            {
                Debug.Log("Instance new");
                s_instance = FindObjectOfType<T>();
            }
            if (FindObjectsOfType<T>().Length > 1)
            {
                Debug.Log("More than 1!");
                return s_instance;
            }
            if (s_instance == null)
            {
                string instanceName = typeof(T).Name;
                //Debug.Log("Instance Name: " + instanceName);
                GameObject instanceGO = GameObject.Find(instanceName);
                if (instanceGO == null)
                    instanceGO = new GameObject(instanceName);
                s_instance = instanceGO.AddComponent<T>();
                DontDestroyOnLoad(instanceGO);
            }
            return s_instance;



            //if (s_instance == null)
            //{
            //    GameObject original = Resources.Load(typeof(T).Name) as GameObject;
            //    if ((original == null) || (original.GetComponent<T>() == null))
            //    {
            //        Debug.LogError("Prefab for game manager " + typeof(T).Name + " is not found");
            //    }
            //    else
            //    {
            //        GameObject target = GameObject.Find("GameManagers");
            //        if (target == null)
            //        {
            //            target = new GameObject("GameManagers");
            //            UnityEngine.Object.DontDestroyOnLoad(target);
            //        }
            //        GameObject obj4 = UnityEngine.Object.Instantiate<GameObject>(original);
            //        obj4.transform.parent = target.transform;
            //        s_instance = obj4.GetComponent<T>();
            //    }
            //}
            //return s_instance;
        }
    }
}
