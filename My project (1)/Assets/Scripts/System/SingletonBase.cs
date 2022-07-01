using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Resources;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace System
{
    public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
    {
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }

                if (instance == null)
                {
#if UNITY_EDITOR
                    Debug.LogError(typeof(T).Name + " instance is null, Init plz");
#endif
                }

                return instance;
            }
        }

        private static T instance;

        public static void Init()
        {
            if (instance != null)
                return;

            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(typeof(T).Name);
            handle.Completed += (result) =>
            {
                instance = result.Result.GetComponent<T>();
                DontDestroyOnLoad(instance);

                (instance as SingletonBase<T>).OnCreated();
            };
        }

        public virtual void OnCreated()
        {

        }


    }
}