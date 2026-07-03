using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private Camera camera;

    private PlayerInput playerInput;
    private PlayerInput.OnFootActions onFoot;


    #region Look
    private float xRotation = 0f;
    #endregion


    #region Movement
    private Vector3 playerVelocity;
    private float gravity = -9.8f;
    #endregion



    private void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
        camera = GetComponentInChildren<Camera>();

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
        ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = new Vector3(input.x, 0, input.y);
        characterController.Move(transform.TransformDirection(moveDirection) *
            SettingsData.currentMoveSpeed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (characterController.isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2;
        characterController.Move(playerVelocity * Time.deltaTime);
    }

    public void ProcessLook(Vector2 input)
    {
        xRotation -= (input.y * Time.deltaTime) * SettingsData.currentVerticalSensitivity;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (input.x * Time.deltaTime) * SettingsData.currentHorizontalSensitivity);
    }

}
