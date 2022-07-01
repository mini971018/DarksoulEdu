using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniRx;
using UniRx.Triggers;

namespace Player_Move
{
    public class PlayerController : MonoBehaviour
    {
        [Header("카메라 암")]
        [SerializeField] private GameObject CameraTarget;

        [Header("캐릭터 이동속도")]
        [SerializeField] private float WalkSpeed;
        [SerializeField] private float RunSpeed;

        [Header("플레이어 리지드바디")]
        [SerializeField] private Rigidbody PlayerRigidbody;

        private IDisposable InputStream; // 이동용 인풋스트림
        private bool IsActive = false; // 플레이어 액티브 상태
        private Vector3 Direction; //플레이어 목적지



        private void Start()
        {
            Init();
            Active();
        }

        private void Init()
        {
            InputManager.Instance.OnPlayerMoveHorizontal += SetDirectionX;
            InputManager.Instance.OnPlayerMoveVertical += SetDirectionZ;

        }

        private void Active()
        {
            IsActive = true;
            PlayerMoveUpdate();
        }

        private IDisposable PlayerMoveUpdate()
        {
            return Observable.EveryUpdate()
                .Where(_ => IsActive)
                .Subscribe(_ =>
                {
                    PlayerMove();
                });
        }

        private void PlayerMove()
        {
            Vector3 moveDirection = (CameraTarget.transform.forward * Direction.z + CameraTarget.transform.right * Direction.x);

            PlayerRigidbody.velocity = WalkSpeed * moveDirection;
        }

        // 인풋매니저에서 값을 받아와 이동할 목적지 지정
        private void SetDirectionZ(float value)
        {
            Direction = new Vector3(Direction.x, 0, value);
        }

        private void SetDirectionX(float value)
        {
            Direction = new Vector3(value, 0, Direction.z);
        }
    }
}
