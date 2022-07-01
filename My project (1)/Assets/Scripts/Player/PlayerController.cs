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
        [Header("ī�޶� ��")]
        [SerializeField] private GameObject CameraTarget;

        [Header("ĳ���� �̵��ӵ�")]
        [SerializeField] private float WalkSpeed;
        [SerializeField] private float RunSpeed;

        [Header("�÷��̾� ������ٵ�")]
        [SerializeField] private Rigidbody PlayerRigidbody;

        private IDisposable InputStream; // �̵��� ��ǲ��Ʈ��
        private bool IsActive = false; // �÷��̾� ��Ƽ�� ����
        private Vector3 Direction; //�÷��̾� ������



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

        // ��ǲ�Ŵ������� ���� �޾ƿ� �̵��� ������ ����
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
