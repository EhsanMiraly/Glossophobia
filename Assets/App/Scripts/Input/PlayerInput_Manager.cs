using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput_Manager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;

    private PlayerController playerController;


    private void OnEnable()
    {
        playerController = GetComponent<PlayerController>();

        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        onFoot.Enable();

    }

    private void OnDisable()
    {
        onFoot.Disable();
    }


    private void Update()
    {
        playerController.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        playerController.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        //playerController.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        // playerController.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }
}
