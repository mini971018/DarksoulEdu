using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace System
{
    public class SceneManager : SingletonBase<SceneManager>
    {
        public override void OnCreated()
        {
            base.OnCreated();

            ChangeMainScene();
        }

        private void ChangeMainScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Main");
        }
    }
}
