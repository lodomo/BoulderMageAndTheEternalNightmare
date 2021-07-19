using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class BoulderController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private int playerId;
    public int PlayerId => playerId;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpForce = 4100f; //4100 is a perfect 3 blocks
    [SerializeField] private float wallJumpForce = 250f; //250 seems money
    [SerializeField] private float fallForce = 250f; //250 seems money
    [SerializeField] private bool isJumping;
    [SerializeField] private bool onGround;
    [SerializeField] private bool onWall;
    [SerializeField] private bool canAirJump;
    [SerializeField] private bool canFlip = true;
    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    private WaitForSeconds _flipDisableTimer = new WaitForSeconds(0.25f);
    private GroundCheck groundCheck;
    private WallCheck wallCheck;
    private Vector3 ZeroVector = Vector3.zero;
    [Range(0, .3f)] [SerializeField] private float movementSmoothing = .05f;
    private Transform _transform;

    [SerializeField] private GameObject landDust;
    [SerializeField] private Transform landDustSpawnPoint;
    [SerializeField] private GameObject jumpDust;
    [SerializeField] private Transform jumpDustSpawnPoint;
    [SerializeField] private GameObject wallDust;
    [SerializeField] private Transform wallDustSpawnPoint;


    private void Awake()
    {
        _playerInput = GameObject.Find("Globals").GetComponent<Globals>().PlayerInputs[playerId];
        
        _playerInput.OnBDown += JumpPress;
        _playerInput.OnBUp += JumpRelease;
        
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        
        groundCheck = GetComponentInChildren<GroundCheck>();
        groundCheck.OnGround += HitGround;
        groundCheck.OffGround += LeaveGround;
        
        wallCheck = GetComponentInChildren<WallCheck>();
        wallCheck.OnWall += HitWall;
        wallCheck.OffWall += LeaveWall;

        _transform = transform;
    }

    private void Update()
    {
        HorizontalMovement(_playerInput.DPad.x);
        Falling();
        Facing();
    }

    private void HorizontalMovement(float input)
    {
        if (!canFlip) return;
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(input * moveSpeed, _rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        _rigidbody2D.velocity = Vector3.SmoothDamp(_rigidbody2D.velocity, targetVelocity, ref ZeroVector, movementSmoothing);
    }
    
    private void JumpRelease()
    {
        isJumping = false;
    }

    private void JumpPress()
    {
        isJumping = true;
        if (onGround)
        {
            StartCoroutine(Co_Jump());
        }
        else if (onWall)
        {
            WallJump();
        }
        else if (canAirJump)
        {
            canAirJump = false;
            StartCoroutine(Co_AirJump());
        }
        
    }

    private IEnumerator Co_Jump()
    {
        var realJumpForce = jumpForce;
        var velocity = _rigidbody2D.velocity;
        velocity.y = 0;
        _rigidbody2D.velocity = velocity;
        Instantiate(jumpDust, jumpDustSpawnPoint.position, jumpDustSpawnPoint.rotation);
        while (isJumping)
        {
            var deltaTime = Time.fixedDeltaTime;
            _rigidbody2D.AddForce(Vector2.up * (realJumpForce * deltaTime), ForceMode2D.Impulse);
            if (realJumpForce > 1)
            {
                realJumpForce /= 75 * deltaTime; //Jump Decay
            }
            else
            {
                isJumping = false;
            }
            yield return _waitForEndOfFrame;
        }
    }

    private IEnumerator Co_AirJump()
    {
        var realJumpForce = jumpForce;
        var velocity = _rigidbody2D.velocity;
        velocity.y = 0;
        _rigidbody2D.velocity = velocity;
        Instantiate(jumpDust, jumpDustSpawnPoint.position, jumpDustSpawnPoint.rotation);

        while (isJumping)
        {
            var deltaTime = Time.fixedDeltaTime;
            _rigidbody2D.AddForce(Vector2.up * (realJumpForce * deltaTime), ForceMode2D.Impulse);
            if (realJumpForce > 1)
            {
                realJumpForce /= 75 * deltaTime; //Jump Decay
            }
            else
            {
                isJumping = false;
            }
            yield return _waitForEndOfFrame;
        }
    }

    private void WallJump()
    {
        if (onGround) return;
        if (_rigidbody2D.velocity.y > 1)
        {
            if (!canAirJump) return;
            canAirJump = false;
            StartCoroutine(Co_AirJump());
        }
        else
        {
            var direction = new Vector2(-1 * _transform.localScale.x, 1);
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.AddForce(direction * wallJumpForce, ForceMode2D.Impulse);
            Flip();
            StartCoroutine(FlipDisable()); 
        }
    }

    private void HitGround()
    {
        onGround = true;
        canAirJump = true;
        Instantiate(landDust, landDustSpawnPoint.position, landDustSpawnPoint.rotation);
    }

    private void LeaveGround()
    {
        onGround = false;
    }
    
    private void HitWall()
    {
        onWall = true;
    }

    private void LeaveWall()
    {
        onWall = false;
    }

    private void Falling()
    {
        var velocity = _rigidbody2D.velocity;
        if (velocity.y > 1) return;
        
        if (onWall)
        {
            var slideVelocity = -125f;
            if (velocity.y < slideVelocity)
            {
                velocity.y = slideVelocity;
                _rigidbody2D.velocity = velocity;
            }
        }
        else
        {
            _rigidbody2D.AddForce(Vector2.down * (Time.deltaTime * fallForce), ForceMode2D.Impulse);
        }
    }

    private void Facing()
    {
        if (!canFlip) return;
        var localScale = _transform.localScale;
        var input = _playerInput.DPad.x;
        var velocity = _rigidbody2D.velocity.x;
        
        if (input > 0 && localScale.x < 0)
        {
            Flip();
        }
        else if (input < 0 && localScale.x > 0)
        {
            Flip();
        }
    }

    private void Flip()
    {
        var temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private IEnumerator FlipDisable()
    {
        canFlip = false;
        yield return _flipDisableTimer;
        canFlip = true;
    }
}
