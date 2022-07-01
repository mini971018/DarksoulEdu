using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace System
{
    public enum MoveState
    {
        Walk,
        Run,
        Crouch,
    }

    public class InputManager : SingletonBase<InputManager>
    {
        public delegate void OnPlayerMoveHorizontalDelegate(float value);
        public OnPlayerMoveHorizontalDelegate OnPlayerMoveHorizontal;
        public delegate void OnPlayerMoveVerticalDelegate(float value);
        public OnPlayerMoveVerticalDelegate OnPlayerMoveVertical;
        public delegate void OnPlayerStateDelegate(MoveState value);
        public OnPlayerStateDelegate OnPlayerMoveState;

        public bool IsKeyboardEnable = true;
        public bool IsMouseEnable = true;

        public override void OnCreated()
        {
            base.OnCreated();
            
            GetKeyboardStream();
        }

        // 키보드 스트림 
        private IDisposable GetKeyboardStream()
        {
            return Observable.EveryUpdate()
                .Where(_ => IsKeyboardEnable)
                .Subscribe(_ =>
                {
                    PlayerMove();
                });
        }

        private void PlayerMove()
        {
            float playerMoveZ = Input.GetAxis("Horizontal");
            float playerMoveX = Input.GetAxis("Vertical");

            OnPlayerMoveHorizontal.Invoke(playerMoveZ);
            OnPlayerMoveVertical.Invoke(playerMoveX);

            //if (OnPlayerMoveHorizontal != null)
            //{

            //    if (playerMoveZ != 0)
            //    {
            //    }
            //}

            //if (OnPlayerMoveVertical != null)
            //{

            //    if (playerMoveX != 0)
            //    {
                    
            //    }
            //}

            

        }

        //마우스 스트림
        private IDisposable GetMouseStream()
        {
            return Observable.EveryUpdate()
                .Where(_ => IsMouseEnable)
                .Subscribe(_ =>
                {

                });
        }

    }
}