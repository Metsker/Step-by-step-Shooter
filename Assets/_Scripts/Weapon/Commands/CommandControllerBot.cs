using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.PlayerLogic;
using _Scripts.Utility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Weapon.Commands
{
    public class CommandControllerBot : MonoBehaviour
    {
        private Gun _gun;
        private Camera _camera;
        
        private Command _currentCommand;
        private readonly Queue<Command> _commands = new Queue<Command>();

        private void Awake()
        {
            _camera = Camera.main;
            _gun = GetComponent<Gun>();
        }
        private void Start()
        {
            Player.Instance.PlayerControls.Actions.Fire.performed += ListenForCommands;
        }
        private void Update()
        {
            ProcessCommands();
        }
        private void ProcessCommands()
        {
            if (_currentCommand != null && _currentCommand.IsInProcess) return;
            if (!_commands.Any()) return;
            
            _currentCommand = _commands.Dequeue();
            _currentCommand.Execute();
        }
        private void ListenForCommands(InputAction.CallbackContext context)
        {
            if (GameStateManager.CurrentGameState != GameStateManager.GameState.Shooting) return;
            Vector3 rayPos;
#if UNITY_EDITOR || UNITY_STANDALONE
            rayPos = Mouse.current.position.ReadValue();
#elif UNITY_ANDROID || UNITY_IOS
            rayPos = Touchscreen.current.primaryTouch.position.ReadValue();
#endif
            var ray = _camera.ScreenPointToRay(rayPos);
            var shootCommand = new ShootCommand(_gun, ray);
            _commands.Enqueue(shootCommand);
        }
    }
}