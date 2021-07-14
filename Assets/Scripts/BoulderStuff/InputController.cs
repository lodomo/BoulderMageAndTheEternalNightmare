using System;
using Rewired;
using UnityEngine;

namespace Input
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private int playerId = 0;
        private Player _player;
        [SerializeField] private Vector2 moveVector;
        public Vector2 MoveVector => moveVector;

        private bool _jumpInputDown;
        private bool _jumpInputUp;
        public Action OnJumpDown;
        public Action OnJumpUp;
        

        void Awake()
        {
            _player = ReInput.players.GetPlayer(playerId);
        }

        void Update()
        {
            GetInput();
            ProcessInput();
        }

        private void GetInput()
        {
            moveVector.x = _player.GetAxis("HorizontalMove");
            moveVector.y = _player.GetAxis("VerticalMove");
            _jumpInputDown = _player.GetButtonDown("Jump");
            _jumpInputUp = _player.GetButtonUp("Jump");
        }

        private void ProcessInput()
        {
            InvokeAction(_jumpInputDown, OnJumpDown);
            InvokeAction(_jumpInputUp, OnJumpUp);
        }
        
        void InvokeAction(bool b, Action a)
        {
            if (!b) return;
            a?.Invoke();
            print("Button Pressed");
        }
    }
}
