using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 6;
    //public float coyoteTime = 0.05f;
    //public float _coyoteDuration = 0;
    [Space(3f)]
    public float jumpStrength = 8;
    public float jumpRest = 0.1f;
    private float _standingDuration = 0;
    [Space(5f)]
    public float mouseSensivity = 0.1f;
    [Space(5f)]
    public float rotationSpeed = 0.5f;
    private Vector2 _targetRotation;
    [Space(5f)]
    [Header("Physics Settings")]
    //public float gravity = 4;
    public float gravityPower = 1;
    [Space(5f)]
    [Header("Input Links")]
    public GameObject cameraRoot;
    public GameObject armature;
    private Animator _armatureAnimator;
    [Space(5f)]
    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference rotateCamera;
    [Space(5f)]
    [Header("Input Links")]
    [Min(0.01f)] public float animationBlendSpeed = 1;
    private float _blendingDuration = 0;
    private CharacterController _character;
    private float _displayRatio;

    //public int frameRate = 60;




    private void Awake()
    {
        _character = GetComponent<CharacterController>();
        _armatureAnimator = armature.GetComponent<Animator>();

        _displayRatio = (float)Display.main.systemHeight / (float)Display.main.systemWidth;
    }



    private void Update()
    {
        //Application.targetFrameRate = frameRate;
        Vector2 moveDirection = move.action.ReadValue<Vector2>();
        moveDirection = RotateMovementToCameraView(moveDirection);

        bool isMoving = moveDirection != Vector2.zero;
        _armatureAnimator.SetBool("Walking", isMoving);
        _armatureAnimator.SetFloat("WalkBland", _blendingDuration);
        _armatureAnimator.SetFloat("Standing", _standingDuration);
        if (isMoving)
        {
            _targetRotation = moveDirection;
            _blendingDuration = Mathf.Min(_blendingDuration + (animationBlendSpeed * Time.deltaTime), 1);
        }
        else
        {
            _blendingDuration = Mathf.Max(_blendingDuration - (animationBlendSpeed * Time.deltaTime), 0);
        }

        Vector3 moveSpeed = new Vector3(moveDirection.x * speed, _character.velocity.y, moveDirection.y * speed);


        _armatureAnimator.SetBool("Grounded", _character.isGrounded);
        if (_character.isGrounded)
        {
            bool toJump = 
                jump.action.phase == InputActionPhase.Performed && _standingDuration > jumpRest;
            _armatureAnimator.SetBool("Jump", toJump);

            if (toJump)
            {
                moveSpeed.y = jumpStrength;
            }
            else
            {
                moveSpeed.y = 0;
            }


            _standingDuration += Time.deltaTime;
        }
        else
        {
            _standingDuration = 0;
        }


        moveSpeed.y -= Time.deltaTime * gravityPower;


        _character.Move(moveSpeed * Time.deltaTime);
        RotateCamera();
        RotateArmature();
    }

    private void RotateCamera()
    {
        if (Mouse.current == null)
        {
            Debug.Log("Mouse dont exist");
            return;
        }

        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseDelta = Mouse.current.delta.ReadValue();
            Vector2 mouseMovement = mouseDelta * mouseSensivity;

            Vector3 rotation = cameraRoot.transform.rotation.eulerAngles;

            rotation.x = rotation.x - (mouseMovement.y * _displayRatio);

            rotation.y = rotation.y + mouseMovement.x;

            cameraRoot.transform.rotation = Quaternion.Euler(rotation);
        }
    }

    private void RotateArmature()
    {
        float desiredAngle = -Vector2.SignedAngle(Vector2.up, _targetRotation);
        Quaternion targetQuaternion = Quaternion.Euler(0, desiredAngle, 0);

        armature.transform.rotation = Quaternion.Slerp(
            armature.transform.rotation,
            targetQuaternion,
            rotationSpeed * Time.deltaTime
        );
    }


    private Vector2 RotateMovementToCameraView(Vector2 vector)
    {
        float angle = -cameraRoot.transform.rotation.eulerAngles.y;
        Vector2 result = Quaternion.Euler(0, 0, angle) * vector;
        
        return result;
    }

}
