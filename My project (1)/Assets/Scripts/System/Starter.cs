using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace System
{
    public class Starter : MonoBehaviour
    {
        void Start()
        {
            InputManager.Init();
            SceneManager.Init();
        }
    }
}
