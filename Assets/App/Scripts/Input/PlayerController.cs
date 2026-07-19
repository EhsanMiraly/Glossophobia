using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

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


        #region Add Camera To Player

        camera = FindAnyObjectByType<Camera>();
        camera.transform.parent = this.transform;
        camera.transform.localPosition = new Vector3(0, 1, 0);
        camera.transform.localRotation = Quaternion.identity;
        camera = GetComponentInChildren<Camera>();

        #endregion


        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        onFoot.Enable();
    }

    private void OnDisable()
    {
        #region Remove Camera From Player
        GameObject cameraParent = new GameObject();
        camera.transform.parent = cameraParent.transform;
        SceneManager.MoveGameObjectToScene(cameraParent, SceneManager.GetSceneByName("BaseScene"));
        #endregion

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
        xRotation -= input.y * SettingsData.currentVerticalSensitivity / 100f;
        xRotation = Mathf.Clamp(xRotation, -85f, 85f);
        camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * input.x * SettingsData.currentHorizontalSensitivity / 100f);
    }

}
