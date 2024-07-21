using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Slax.InputSystem
{
    // [CreateAssetMenu(menuName = "Create Once/InputManager")]
    public class InputHandler : ScriptableObject, @GameInput.IGameplayActions, @GameInput.IDialogueActions, @GameInput.IMenuActions, @GameInput.IPauseActions
    {
        public static UnityAction<string> OnInputActionChanged;

        // Gameplay
        public event UnityAction<Vector2> Gameplay_MoveEvent = delegate { };
        public event UnityAction Gameplay_InteractEvent = delegate { };
        public event UnityAction Gameplay_AttackEvent = delegate { };
        public event UnityAction Gameplay_DodgeEvent = delegate { };
        public event UnityAction Gameplay_PauseEvent = delegate { };
        public event UnityAction<Vector2> Gameplay_HotBarNavigateEvent = delegate { };
        public event UnityAction Gameplay_NorthActionEvent = delegate { };
        public event UnityAction Gameplay_LeftTriggerEvent = delegate { };
#if UNITY_EDITOR
        public event UnityAction DEBUG_ACTION_1Event = delegate { };
        public event UnityAction DEBUG_ACTION_2Event = delegate { };
        public event UnityAction DEBUG_ACTION_3Event = delegate { };
        public event UnityAction DEBUG_ACTION_4Event = delegate { };
#endif

        // Dialogue
        public event UnityAction Dialogue_AdvanceEvent = delegate { };
        public event UnityAction<Vector2> Dialogue_SelectOptionEvent = delegate { };
        public event UnityAction Dialogue_ConfirmOptionEvent = delegate { };
        public event UnityAction Dialogue_SkipEvent = delegate { };

        // Menu
        public event UnityAction<Vector2> Menu_NavigateEvent = delegate { };
        public event UnityAction Menu_ConfirmSelectionEvent = delegate { };
        public event UnityAction Menu_CancelSelectionEvent = delegate { };
        public event UnityAction<Vector2> Menu_TabSwitchEvent = delegate { };

        // Pause Menu
        public event UnityAction<Vector2> PauseMenu_NavigateEvent = delegate { };
        public event UnityAction PauseMenu_ConfirmSelectionEvent = delegate { };
        public event UnityAction PauseMenu_BackEvent = delegate { };
        public event UnityAction PauseMenu_UnpauseEvent = delegate { };

        GameInput _gameInput;
        public string CurrentInputAction { get; private set; }

        #region Configuration

        void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
                _gameInput.Dialogue.SetCallbacks(this);
                _gameInput.Menu.SetCallbacks(this);
                _gameInput.Pause.SetCallbacks(this);
            }
        }

        void OnDisable()
        {
            DisableAllInput();
        }

        public void EnableGameplayInput()
        {
            DisableAllInput();
            _gameInput.Gameplay.Enable();
            CurrentInputAction = "Gameplay";
            OnInputActionChanged?.Invoke(CurrentInputAction);
        }

        public void EnableDialogueInput()
        {
            DisableAllInput();
            _gameInput.Dialogue.Enable();
            CurrentInputAction = "Dialogue";
            OnInputActionChanged?.Invoke(CurrentInputAction);
        }

        public void EnableMenuInput()
        {
            DisableAllInput();
            _gameInput.Menu.Enable();
            CurrentInputAction = "Menu";
            OnInputActionChanged?.Invoke(CurrentInputAction);
        }

        public void EnablePauseMenuInput()
        {
            DisableAllInput();
            _gameInput.Pause.Enable();
            CurrentInputAction = "PauseMenu";
            OnInputActionChanged?.Invoke(CurrentInputAction);
        }

        public void DisableAllInput()
        {
            CurrentInputAction = "None";

            _gameInput.Gameplay.Disable();
            _gameInput.Dialogue.Disable();
            _gameInput.Menu.Disable();
            _gameInput.Pause.Disable();
        }

        #endregion

        #region Dialogue

        public void OnDialogue_Advance(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Dialogue_AdvanceEvent.Invoke();
            }
        }

        public void OnDialogue_ConfirmOption(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Dialogue_ConfirmOptionEvent.Invoke();
            }
        }

        public void OnDialogue_SelectOption(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Dialogue_SelectOptionEvent.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnDialogue_Skip(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Dialogue_SkipEvent.Invoke();
            }
        }

        #endregion

        #region Gameplay

        public void OnGameplay_Attack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_AttackEvent.Invoke();
            }
        }

        public void OnGameplay_Dodge(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_DodgeEvent.Invoke();
            }
        }

        public void OnGameplay_HotBarNavigate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_HotBarNavigateEvent.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnGameplay_Interact(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_InteractEvent.Invoke();
            }
        }

        public void OnGameplay_LeftTrigger(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_LeftTriggerEvent.Invoke();
            }
        }

        public void OnGameplay_Move(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_MoveEvent.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnGameplay_NorthAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_NorthActionEvent.Invoke();
            }
        }

        public void OnGameplay_Pause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Gameplay_PauseEvent.Invoke();
            }
        }

#if UNITY_EDITOR
        #region Debug
        public void OnDEBUG_ACTION_1(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DEBUG_ACTION_1Event.Invoke();
            }
        }

        public void OnDEBUG_ACTION_2(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DEBUG_ACTION_2Event.Invoke();
            }
        }

        public void OnDEBUG_ACTION_3(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DEBUG_ACTION_3Event.Invoke();
            }
        }

        public void OnDEBUG_ACTION_4(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                DEBUG_ACTION_4Event.Invoke();
            }
        }
        #endregion
#endif

        #endregion

        #region Menu

        public void OnMenu_CancelSelection(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Menu_CancelSelectionEvent.Invoke();
            }
        }

        public void OnMenu_ConfirmSelection(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Menu_ConfirmSelectionEvent.Invoke();
            }
        }

        public void OnMenu_Navigate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Menu_NavigateEvent.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnMenu_TabSwitch(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Menu_TabSwitchEvent.Invoke(context.ReadValue<Vector2>());
            }
        }

        #endregion

        #region Pause Menu

        public void OnPauseMenu_Back(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PauseMenu_BackEvent.Invoke();
            }
        }

        public void OnPauseMenu_ConfirmSelection(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PauseMenu_ConfirmSelectionEvent.Invoke();
            }
        }

        public void OnPauseMenu_Navigate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PauseMenu_NavigateEvent.Invoke(context.ReadValue<Vector2>());
            }
        }

        public void OnPauseMenu_Unpause(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                PauseMenu_UnpauseEvent.Invoke();
            }
        }

        public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
        {
        }

        public void OnTrackedDevicePosition(InputAction.CallbackContext context)
        {
        }

        public void OnRightClick(InputAction.CallbackContext context)
        {
        }

        public void OnMiddleClick(InputAction.CallbackContext context)
        {
        }

        public void OnScrollWheel(InputAction.CallbackContext context)
        {
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                if (CurrentInputAction == "Menu")
                    Menu_ConfirmSelectionEvent.Invoke();
                else if (CurrentInputAction == "PauseMenu")
                    PauseMenu_ConfirmSelectionEvent.Invoke();
            }
        }

        public void OnPoint(InputAction.CallbackContext context)
        {
        }

        #endregion
    }
}