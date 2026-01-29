using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviourSystem : MonoBehaviour {
        public List<IBehaviourCommand> CommandQueue = new();
        public List<IBehaviourCommand> ActiveCommands = new();
        
        private PlayerController _playerController;
        private TimerController _timerController;
        
        private void Start() {
            _playerController = GetComponent<PlayerController>();
            _timerController = GetComponent<TimerController>();
        }

        private void Update() {
            foreach (IBehaviourCommand command in CommandQueue) {
                CheckIfActive(command);
            }
            
            foreach (IBehaviourCommand command in ActiveCommands) {
                command.ExecuteInUpdate();
            }
        }

        public void FixedUpdate() {
            foreach (IBehaviourCommand command in ActiveCommands) {
                command.ExecuteInFixedUpdate();
            }
        }

        private void CheckIfActive(IBehaviourCommand command) {
            
        }
}