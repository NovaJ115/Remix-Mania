using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovementV2 : MonoBehaviour
{
    [Header("References")]
    public PlayerMovementStatsV2 moveStats;
    public StatRandomizer statRandomizer;
    [SerializeField] private Collider2D feetColl;
    [SerializeField] private Collider2D headColl;
    [SerializeField] private Collider2D bodyColl;
    [SerializeField] private TextMeshProUGUI jumpCounterText;
    [SerializeField] private Image jumpIcon;

    [Header("FX")]
    [SerializeField] private GameObject jumpParticles;
    [SerializeField] private GameObject secondJumpParticles;
    [SerializeField] private GameObject landParticles;
    [SerializeField] private Transform particleSpawnTransform;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private ParticleSystem speedParticles;
    [SerializeField] private GameObject dashParticles;
    [SerializeField] private AudioSource dashSound;
    [SerializeField] private AudioSource stepSound1;
    [SerializeField] private AudioSource stepSound2;
    [SerializeField] private AudioSource wallSlideSound;
    [SerializeField] private AudioSource wallGrabSound;

    [Header("Height Tracker")]
    [SerializeField] private Transform heightTracker;

    [Header("EnabledAbilities")]
    [SerializeField] private bool canRun;
    [SerializeField] private bool canDash;
    [SerializeField] private bool canWallSlide;

    private Rigidbody2D rb;
    private Animator anim;

    //movement vars
    public Vector2 horizontalVelocity { get; private set; }
    public bool isFacingRight;

    //collision check vars
    private RaycastHit2D groundHit;
    private RaycastHit2D headHit;
    private RaycastHit2D wallHit;
    private RaycastHit2D lastWallHit;
    public bool isGrounded;
    private bool bumpedHead;
    private bool isTouchingWall;
    private Vector2 feetCheckDirection;

    //jump vars
    public float verticalVelocity { get; private set; }
    public bool isJumping;
    private bool isFastFalling;
    private bool isFalling;
    private float fastFallTime;
    private float fastFallReleaseSpeed;
    private int numberOfJumpsUsed;
    public int timesJumped;
    //apex vars
    private float apexPoint;
    private float timePastApexThreshold;
    private bool isPastApexThreshold;
    //jump buffer vars
    private float jumpBufferTimer;
    private bool jumpReleasedDuringBuffer;
    //coyote time vars
    private float coyoteTimer;

    //wall jump vars
    public bool isWallSliding { get; private set; }
    private bool isWallSlideFalling;
    private bool useWallJumpMoveStats;
    private bool isWallJumping;
    private float wallJumpTime;
    private bool isWallJumpFastFalling;
    private bool isWallJumpFalling;
    private float wallJumpFastFallTime;
    private float wallJumpFastFallReleaseSpeed;

    private float wallJumpPostBufferTimer;

    private float wallJumpApexPoint;
    private float timePastWallJumpApexThreshold;
    private bool isPastWallJumpApexThreshold;

    public bool isLimitedWallJumps;

    public bool isReverseWallSlide;
    public float increasedGravityCounterAmount = 1;
    //dash vars
    private bool isDashing;
    private bool isAirDashing;
    private float dashTimer;
    private float dashOnGroundTimer;
    private int dashDirectionMult;
    private int numberOFDashesUsed;
    private Vector2 dashDirection;
    private bool isDashFastFalling;
    private float dashFastFallTime;
    private float dashFastFallReleaseSpeed;


    // Add a field to track the highest point
    private float highestPoint;
    private float heightTrackerStartingPoint;

    private void Awake()
    {
        isFacingRight = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        statRandomizer = FindFirstObjectByType<StatRandomizer>();
        trailRenderer.emitting = false;
    }
    private void Update()
    {
        CountTimers();

        JumpChecks();
        LandCheck();
        WallSlideCheck();
        WallJumpCheck();
        DashCheck();
        JumpCounter();
        //ResetJumpedThisFrame();
    }
    private void FixedUpdate()
    {
        //Debug.Log(numberOfJumpsUsed);
        CollisionChecks();
        Jump();
        Fall();

        WallSlide();
        WallJump();
        Dash();
        if (isGrounded)
        {
            Move(moveStats.groundAcceleration, moveStats.groundDeceleration, InputManager.movement);
        }
        else
        {
            //Walljumping
            if (useWallJumpMoveStats)
            {
                Move(moveStats.wallJumpMoveAcceleration, moveStats.wallJumpMoveDeceleration, InputManager.movement);
            }

            //AIREBORNE
            else
            {
                Move(moveStats.airAcceleration, moveStats.airDeceleration, InputManager.movement);
            }
        }
        ApplyVelocity();
        
    }
    private void OnDrawGizmos()
    {
        if (moveStats.showWalkJumpArc)
        {
            DrawJumpArc(moveStats.maxWalkSpeed, Color.white);
        }

        if (moveStats.showRunJumpArc)
        {
            DrawJumpArc(moveStats.maxRunSpeed, Color.red);
        }
    }

    private void IncrementVerticalVelocity(float incrementAmount)
    {
        verticalVelocity += incrementAmount;
        //Debug.Log(verticalVelocity);
    }

    private void ChangeVerticalVelocity(float velAmount)
    {
        verticalVelocity = velAmount;
        //Debug.Log(verticalVelocity);
    }

    private void ApplyVelocity()
    {
        //Clamp speed
        if (!isDashing)
        {
            ChangeVerticalVelocity(Mathf.Clamp(verticalVelocity, -moveStats.maxFallSpeed, 50f));
        }
        else
        {
            ChangeVerticalVelocity(Mathf.Clamp(verticalVelocity, -50f, 50f));
        }
        if (!statRandomizer.isReverseGravity)
        {
            rb.linearVelocity = new Vector2(horizontalVelocity.x, verticalVelocity);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontalVelocity.x, -verticalVelocity);
        }
        
    }
    #region Bounce Pad
    public void BouncePad(float velocity)
    {
        ChangeVerticalVelocity(velocity);
    }
    #endregion

    #region Fall and Land Checks

    private void Fall()
    {
        //NORMAL GRAVITY (without jumping)
        if (!isGrounded && !isJumping && !isWallSliding && !isWallJumping && !isDashing && !isDashFastFalling)
        {
            if (!isFalling)
            {
                isFalling = true;
                trailRenderer.emitting = true;
                anim.ResetTrigger("land");
                anim.SetTrigger("fall");
                
            }

            IncrementVerticalVelocity(moveStats.gravity * Time.fixedDeltaTime);
        }
    }

    private void LandCheck()
    {
        //LANDED
        if ((isJumping || isFalling || isWallJumpFalling || isWallJumping || isWallSlideFalling || isWallSliding || isDashFastFalling) && isGrounded && verticalVelocity <= 0f)
        {
            ResetJumpValues();
            StopWallSliding();
            ResetWallJumpValues();
            ResetDashes();

            ChangeVerticalVelocity(Physics2D.gravity.y);

            numberOfJumpsUsed = 0;

            trailRenderer.emitting = false;

            if (isDashFastFalling && isGrounded)
            {
                if (!anim.GetBool("isAirDashFalling"))
                {
                    ResetDashValues();
                    return;
                }
            }

            ResetDashValues();

            //new
            anim.SetTrigger("land");
            Instantiate(landParticles, particleSpawnTransform.position, Quaternion.identity);

            //height tracker
            if (moveStats.debugShowHeightLogOnLand)
            {
                Debug.Log("Highest Point : " + (highestPoint - heightTrackerStartingPoint));
            }


        }
    }

    #endregion

    #region Movement

    private void Move(float acceleration, float deceleration, Vector2 moveInput)
    {
        if (!isDashing)
        {
            if ((Mathf.Abs(moveInput.x) < moveStats.moveThreshold) && InputManager.runIsHeld && canRun)
            {
                anim.SetBool("isWalking", false);

                horizontalVelocity = Vector2.Lerp(horizontalVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);

                //ApplymovementVelocity(horizontalVelocity);
            }

            else if ((Mathf.Abs(moveInput.x) < moveStats.moveThreshold) && !InputManager.runIsHeld)
            {
                anim.SetBool("isWalking", false);
                anim.SetBool("isRunning", false);
                if (speedParticles.isPlaying)
                {
                    speedParticles.Stop();
                }

                horizontalVelocity = Vector2.Lerp(horizontalVelocity, Vector2.zero, deceleration * Time.fixedDeltaTime);

                //ApplymovementVelocity(horizontalVelocity);
            }

            else if ((Mathf.Abs(moveInput.x) >= moveStats.moveThreshold))
            {
                TurnCheck(moveInput);

                anim.SetBool("isWalking", true);
                Vector2 targetVelocity = Vector2.zero;

                if (InputManager.runIsHeld && canRun)
                {
                    anim.SetBool("isRunning", true);

                    targetVelocity = new Vector2(moveInput.x, 0f) * moveStats.maxRunSpeed;


                    if (Mathf.Abs(horizontalVelocity.x) >= moveStats.maxRunSpeed - 2f && !isJumping && !isFalling)
                    {
                        if (!speedParticles.isPlaying)
                        {
                            speedParticles.Play();
                        }
                    }
                    else
                    {
                        if (speedParticles.isPlaying)
                        {
                            speedParticles.Stop();
                        }
                    }
                }
                else
                {

                    targetVelocity = new Vector2(moveInput.x, 0f) * moveStats.maxWalkSpeed;

                }

                horizontalVelocity = Vector2.Lerp(horizontalVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
                //ApplymovementVelocity(horizontalVelocity);
            }
        }

        if (!InputManager.runIsHeld)
        {
            anim.SetBool("isRunning", false);
            if (speedParticles.isPlaying)
            {
                speedParticles.Stop();
            }
        }
    }

    private void TurnCheck(Vector2 moveInput)
    {
        if (isFacingRight && moveInput.x < 0)
        {
            Turn(false);
        }
        else if (!isFacingRight && moveInput.x > 0)
        {
            Turn(true);
        }

        //Debug.Log(isFacingRight);
    }

    public void Turn(bool turnRight)
    {
        if (turnRight)
        {
            isFacingRight = true;
            transform.Rotate(0f, 180f, 0f);
        }

        else
        {
            isFacingRight = false;
            transform.Rotate(0f, -180f, 0f);
        }
    }

    #endregion

    #region Jump

    public void JumpCounter()
    {
        //Debug.Log(numberOfJumpsUsed);
        if(jumpCounterText != null && jumpIcon != null)
        {
            int jumpsLeft = (moveStats.numberOfJumpsAllowed - numberOfJumpsUsed);
            
            if (isWallSliding)
            {
                if(jumpsLeft <= 0)
                {
                    jumpsLeft = 1;
                }
            }
            if(jumpsLeft == moveStats.numberOfJumpsAllowed && isFalling)
            {
                jumpsLeft = jumpsLeft - 1;
            }
            if (jumpsLeft < 0)
            {
                jumpsLeft = 0;
            }
            jumpCounterText.text = "" + jumpsLeft;
            if (jumpsLeft == moveStats.numberOfJumpsAllowed)
            {
                jumpIcon.color = Color.green;
            }
            if (jumpsLeft < moveStats.numberOfJumpsAllowed && jumpsLeft > 0)
            {
                jumpIcon.color = Color.white;
            }
            if (jumpsLeft == 0)
            {
                jumpIcon.color = Color.red;
            }
        }
        
    }
    private void JumpChecks()
    {
        if (InputManager.jumpWasPressed)
        {
            //cancel if we should jump from a post wall jump buffer
            if (isWallSlideFalling && wallJumpPostBufferTimer >= 0f)
            {
                return;
            }

            //cancel if we are wall sliding or touching a wall in the air
            else if (isWallSliding || (isTouchingWall && !isGrounded))
            {
                return;
            }

            jumpBufferTimer = moveStats.jumpBufferTime;
            jumpReleasedDuringBuffer = false; //
        }

        //START FAST FALLING
        if (InputManager.jumpWasReleased)
        {
            if (jumpBufferTimer > 0f)
            {
                jumpReleasedDuringBuffer = true;
            }

            if (isJumping && verticalVelocity > 0f)
            {
                if (isPastApexThreshold)
                {
                    isPastApexThreshold = false;
                    isFastFalling = true;
                    fastFallTime = moveStats.timeForUpwardsCancel;

                    //gets rid of floatiness
                    ChangeVerticalVelocity(0f);

                }
                else
                {
                    isFastFalling = true;
                    fastFallReleaseSpeed = verticalVelocity;
                }
            }
        }

        //ACTUAL JUMP WITH COYOTE TIME AND JUMP BUFFER
        if (jumpBufferTimer > 0f && !isJumping && (isGrounded || coyoteTimer > 0f))
        {
            InitiateJump(1, jumpParticles);
            if (isDashFastFalling)
            {
                isDashFastFalling = false;
            }

            //Debug.Log("normal jump");
            if (jumpReleasedDuringBuffer)
            {
                isFastFalling = true;
                fastFallReleaseSpeed = verticalVelocity;
            }

            heightTrackerStartingPoint = heightTracker.position.y;
            highestPoint = heightTracker.position.y;
        }

        //ACTUAL JUMP WITH DOUBLE JUMP
        else if (jumpBufferTimer > 0f && (isJumping || isWallJumping || isWallSlideFalling || isAirDashing || isDashFastFalling) && !isTouchingWall && numberOfJumpsUsed < moveStats.numberOfJumpsAllowed)
        {
            //Debug.Log("air jump");

            isFastFalling = false;
            InitiateJump(1, secondJumpParticles);
            
            if (isDashFastFalling)
            {
                isDashFastFalling = false;
            }
        }

        //handle air jump AFTER the coyote time has lapsed (take off an extra jump so we don't get a bonus jump)
        else if (jumpBufferTimer > 0f && isFalling && !isWallSlideFalling && numberOfJumpsUsed < moveStats.numberOfJumpsAllowed - 1)
        {
            //Debug.Log("edge case jump");

            isFastFalling = false;
            InitiateJump(2, jumpParticles);
            
        }
    }

    private void InitiateJump(int theNumberOfJumpsUsed, GameObject particlesToSpawn)
    {
        if (!isJumping)
        {
            isJumping = true;
        }
        
        ResetWallJumpValues();
        timesJumped += 1;
        jumpBufferTimer = 0f;
        numberOfJumpsUsed += theNumberOfJumpsUsed;
        ChangeVerticalVelocity(moveStats.initialJumpVelocity);

        //FX
        anim.SetTrigger("jump");
        anim.ResetTrigger("land");
        trailRenderer.emitting = true;

        Instantiate(particlesToSpawn, particleSpawnTransform.position, Quaternion.identity);
        Instantiate(Resources.Load("JumpSFX"));
    }

    private void Jump()
    {
        //APPLY JUMP GRAVITY
        if (isJumping)
        {
            //HIT HEAD
            if (bumpedHead)
            {
                isFastFalling = true;
            }

            //GRAVITY IN ASCENDING
            if (verticalVelocity >= 0f)
            {
                //APEX CONTROLS
                apexPoint = Mathf.InverseLerp(moveStats.initialJumpVelocity, 0f, verticalVelocity);

                if (apexPoint > moveStats.apexThreshold)
                {
                    if (!isPastApexThreshold)
                    {
                        isPastApexThreshold = true;
                        timePastApexThreshold = 0f;
                    }

                    if (isPastApexThreshold)
                    {
                        timePastApexThreshold += Time.fixedDeltaTime;
                        if (timePastApexThreshold < moveStats.apexHangTime)
                        {
                            ChangeVerticalVelocity(0f);
                        }
                        else
                        {
                            ChangeVerticalVelocity(-0.01f);
                        }
                    }
                }

                //GRAVITY IN ASCENDING BUT NOT PAST APEX THRESHOLD
                else if (!isFastFalling)
                {
                    IncrementVerticalVelocity(moveStats.gravity * Time.fixedDeltaTime);

                    if (isPastApexThreshold)
                    {
                        isPastApexThreshold = false;
                    }
                }

            }

            //GRAVITY ON DESCENDING
            else if (!isFastFalling)
            {
                IncrementVerticalVelocity(moveStats.gravity * moveStats.gravityOnReleaseMultiplier * Time.fixedDeltaTime);
            }

            else if (verticalVelocity < 0f)
            {
                if (!isFalling)
                    isFalling = true;
            }

            if (heightTracker.position.y > highestPoint)
            {
                highestPoint = heightTracker.position.y;
            }
        }

        //HANDLE JUMP CUT TIME
        if (isFastFalling)
        {
            if (fastFallTime >= moveStats.timeForUpwardsCancel)
            {
                IncrementVerticalVelocity(moveStats.gravity * moveStats.gravityOnReleaseMultiplier * Time.fixedDeltaTime);
            }
            else if (fastFallTime < moveStats.timeForUpwardsCancel)
            {
                ChangeVerticalVelocity(Mathf.Lerp(fastFallReleaseSpeed, 0f, (fastFallTime / moveStats.timeForUpwardsCancel)));
            }

            fastFallTime += Time.fixedDeltaTime;
        }
    }

    private void ResetJumpValues()
    {
        isJumping = false;
        isFalling = false;
        isFastFalling = false;
        fastFallTime = 0f;
        isPastApexThreshold = false;
    }

    #endregion

    #region Wall Slide

    private void WallSlideCheck()
    {
        if (isTouchingWall && !isGrounded && !isDashing && canWallSlide)
        {
            if (verticalVelocity < 0f && !isWallSliding)
            {
                ResetJumpValues();
                ResetWallJumpValues();
                ResetDashValues();

                if (moveStats.resetDashOnWallSlide)
                {
                    ResetDashes();
                }

                isWallSlideFalling = false;
                isWallSliding = true;
                

                anim.SetBool("isWallSliding", true);

                if (moveStats.resetJumpsOnWallSlide)
                {
                    numberOfJumpsUsed = 0;
                }
                
            }
        }

        else if (isWallSliding && !isTouchingWall! && !isGrounded && !isWallSlideFalling)
        {
            isWallSlideFalling = true;
            StopWallSliding();
        }

        else
        {
            StopWallSliding();
        }
    }

    private void WallSlide()
    {
        if (isWallSliding)
        {
            if (!isReverseWallSlide)
            {
                ChangeVerticalVelocity(Mathf.Lerp(verticalVelocity, -moveStats.wallSlideSpeed, moveStats.wallSlideDecelerationSpeed * Time.fixedDeltaTime));
            }
            else
            {
                if (statRandomizer.isIncreasedGravity)
                {
                    ChangeVerticalVelocity(Mathf.Lerp(verticalVelocity, moveStats.wallSlideSpeed * increasedGravityCounterAmount, moveStats.wallSlideDecelerationSpeed * Time.fixedDeltaTime));
                    //Debug.Log("Is Working");
                }
                else
                {
                    ChangeVerticalVelocity(Mathf.Lerp(verticalVelocity, moveStats.wallSlideSpeed, moveStats.wallSlideDecelerationSpeed * Time.fixedDeltaTime));
                }
                
            }
            
            
        }
        else
        {
            wallSlideSound.Stop();
        }
    }

    private void StopWallSliding()
    {
        if (isWallSliding)
        {
            numberOfJumpsUsed++;

            isWallSliding = false;
            anim.SetBool("isWallSliding", false);
        }
    }


    #endregion

    #region WallJump

    private void WallJumpCheck()
    {
        if (canWallSlide)
        {
            if (ShouldApplyPostWallJumpBuffer())
            {
                wallJumpPostBufferTimer = moveStats.wallJumpPostBufferTime;
            }

            //START FAST FALLING
            if (InputManager.jumpWasReleased && !isWallSliding && !isTouchingWall && isWallJumping)
            {
                if (isWallJumping && verticalVelocity > 0f)
                {
                    if (isPastWallJumpApexThreshold)
                    {
                        isPastWallJumpApexThreshold = false;
                        isWallJumpFastFalling = true;
                        wallJumpFastFallTime = moveStats.timeForUpwardsCancel;

                        //gets rid of floatiness
                        ChangeVerticalVelocity(0f);
                    }
                    else
                    {
                        isWallJumpFastFalling = true;
                        wallJumpFastFallReleaseSpeed = verticalVelocity;
                    }
                }
            }

            //ACTUAL JUMP WITH POST WALL JUMP BUFFER TIME
            if (InputManager.jumpWasPressed && wallJumpPostBufferTimer > 0f && canWallSlide)
            {
                InitiateWallJump();
                timesJumped += 1;
            }
        }
        
    }

    private void WallJump()
    {
        //APPLY WALL JUMP GRAVITY
        if (isWallJumping && canWallSlide)
        {
            //TIME TO TAKE OVER movement CONTROLS WHILE WALL JUMPING
            wallJumpTime += Time.fixedDeltaTime;
            if (wallJumpTime >= moveStats.timeTillJumpApex)
            {
                useWallJumpMoveStats = false;
            }

            //HIT HEAD
            if (bumpedHead)
            {
                isWallJumpFastFalling = true;
                useWallJumpMoveStats = false;
            }

            //GRAVITY IN ASCENDING
            if (verticalVelocity >= 0f)
            {
                //APEX CONTROLS
                wallJumpApexPoint = Mathf.InverseLerp(moveStats.wallJumpDirection.y, 0f, verticalVelocity);

                if (wallJumpApexPoint > moveStats.apexThreshold)
                {
                    if (!isPastWallJumpApexThreshold)
                    {
                        isPastWallJumpApexThreshold = true;
                        timePastWallJumpApexThreshold = 0f;
                    }

                    if (isPastWallJumpApexThreshold)
                    {
                        timePastWallJumpApexThreshold += Time.fixedDeltaTime;
                        if (timePastWallJumpApexThreshold < moveStats.apexHangTime)
                        {
                            ChangeVerticalVelocity(0f);
                        }
                        else
                        {
                            ChangeVerticalVelocity(-0.01f);
                        }
                    }
                }

                //GRAVITY IN ASCENDING BUT NOT PAST APEX THRESHOLD
                else if (!isWallJumpFastFalling)
                {
                    IncrementVerticalVelocity(moveStats.wallJumpGravity * Time.fixedDeltaTime);

                    if (isPastWallJumpApexThreshold)
                    {
                        isPastWallJumpApexThreshold = false;
                    }
                }

            }

            //GRAVITY ON DESCENDING
            else if (!isWallJumpFastFalling)
            {
                IncrementVerticalVelocity(moveStats.wallJumpGravity * Time.fixedDeltaTime);
            }

            else if (verticalVelocity < 0f)
            {
                if (!isWallJumpFalling)
                    isWallJumpFalling = true;
            }

        }
        if (canWallSlide)
        {
            //HANDLE JUMP CUT TIME
            if (isWallJumpFastFalling)
            {
                if (wallJumpFastFallTime >= moveStats.timeForUpwardsCancel)
                {
                    IncrementVerticalVelocity(moveStats.wallJumpGravity * moveStats.wallJumpGravityOnReleaseMultiplier * Time.fixedDeltaTime);
                }
                else if (wallJumpFastFallTime < moveStats.timeForUpwardsCancel)
                {
                    ChangeVerticalVelocity(Mathf.Lerp(wallJumpFastFallReleaseSpeed, 0f, (wallJumpFastFallTime / moveStats.timeForUpwardsCancel)));
                }

                wallJumpFastFallTime += Time.fixedDeltaTime;
            }
        }

        
    }

    private void InitiateWallJump(GameObject particlesToSpawn = null)
    {

        //Limited Wall Jumps Is Enabled
        if (isLimitedWallJumps && canWallSlide)
        {
            
            if(statRandomizer.currentWallJumpsLeft > 0)
            {
                if (!isWallJumping)
                {
                    isWallJumping = true;
                    useWallJumpMoveStats = true;
                }
                
                StopWallSliding();

                ResetJumpValues();
                wallJumpTime = 0f;
                ChangeVerticalVelocity(moveStats.initialWallJumpVelocity);

                int dirMultiplier = 0;
                Vector2 hitDir = lastWallHit.collider.ClosestPoint(bodyColl.bounds.center);

                if (hitDir.x > transform.position.x)
                {
                    dirMultiplier = -1;
                }
                else { dirMultiplier = 1; }

                horizontalVelocity = new Vector2((Mathf.Abs(moveStats.wallJumpDirection.x) * dirMultiplier), 0f);

                //FX
                anim.SetTrigger("jump");
                anim.ResetTrigger("land");
                trailRenderer.emitting = true;
                Instantiate(Resources.Load("JumpSFX"));
                //Instantiate(particlesToSpawn, particleSpawnTransform.position, Quaternion.identity);
                statRandomizer.currentWallJumpsLeft -= 1;
            }
        }
        //Limited Wall Jumps Is Disabled
        if (!isLimitedWallJumps && canWallSlide)
        {
            if (!isWallJumping)
            {
                isWallJumping = true;
                useWallJumpMoveStats = true;
            }
            
            StopWallSliding();

            ResetJumpValues();
            wallJumpTime = 0f;
            ChangeVerticalVelocity(moveStats.initialWallJumpVelocity);

            int dirMultiplier = 0;
            Vector2 hitDir = lastWallHit.collider.ClosestPoint(bodyColl.bounds.center);

            if (hitDir.x > transform.position.x)
            {
                dirMultiplier = -1;
            }
            else { dirMultiplier = 1; }

            horizontalVelocity = new Vector2((Mathf.Abs(moveStats.wallJumpDirection.x) * dirMultiplier), 0f);

            //FX
            anim.SetTrigger("jump");
            anim.ResetTrigger("land");
            trailRenderer.emitting = true;
            Instantiate(Resources.Load("JumpSFX"));
            //Instantiate(particlesToSpawn, particleSpawnTransform.position, Quaternion.identity);
        }

    }

    private bool ShouldApplyPostWallJumpBuffer()
    {

        if (!isGrounded && (isTouchingWall || isWallSliding) && canWallSlide)
        {
            return true;
        }
        else { return false; }
    }

    private void ResetWallJumpValues()
    {
        isWallSlideFalling = false;
        useWallJumpMoveStats = false;
        isWallJumping = false;
        isWallJumpFastFalling = false;
        isWallJumpFalling = false;
        isPastWallJumpApexThreshold = false;

        wallJumpFastFallTime = 0f;
        wallJumpTime = 0f;
    }

    #endregion

    #region Dash

    private void DashCheck()
    {
        if (InputManager.dashWasPressed && canDash)
        {
            //ground dash
            if (isGrounded && dashOnGroundTimer < 0 && !isDashing)
            {
                InitiateDash();
            }

            //air dash
            else if (!isGrounded && !isDashing && numberOFDashesUsed < moveStats.numberOfDashes)
            {
                isAirDashing = true;
                InitiateDash();

                //you left a wallslide but dashed within the wallJumpPostBufferTimer
                if (wallJumpPostBufferTimer > 0f)
                {
                    numberOfJumpsUsed--;
                    if (numberOfJumpsUsed < 0)
                    {
                        numberOfJumpsUsed = 0;
                    }
                }
            }
        }
    }

    private void InitiateDash()
    {
        //isJumping = false;
        //isFastFalling = false;


        dashDirection = InputManager.movement;

        Vector2 closestDirection = Vector2.zero;
        float minDistance = Vector2.Distance(dashDirection, moveStats.dashDirections[0]);

        for (int i = 0; i < moveStats.dashDirections.Length; i++)
        {
            //skip if we hit it bang on
            if (dashDirection == moveStats.dashDirections[i])
            {
                closestDirection = dashDirection;
                break;
            }

            float distance = Vector2.Distance(dashDirection, moveStats.dashDirections[i]);

            // Check if this is a diagonal direction and apply bias
            bool isDiagonal = (Mathf.Abs(moveStats.dashDirections[i].x) == 1 && Mathf.Abs(moveStats.dashDirections[i].y) == 1);
            if (isDiagonal)
            {
                distance -= moveStats.dashDiagonallyBias;
            }

            else if (distance < minDistance)
            {
                minDistance = distance;
                closestDirection = moveStats.dashDirections[i];
            }
        }

        //handle direction if we have no input
        if (closestDirection == Vector2.zero)
        {
            if (isFacingRight)
            {
                closestDirection = Vector2.right;
            }
            else { closestDirection = Vector2.left; }
        }

        dashDirectionMult = 1; //this may not be needed
        dashDirection = new Vector2(closestDirection.x * dashDirectionMult, closestDirection.y * dashDirectionMult);

        numberOFDashesUsed++;
        isDashing = true;
        dashTimer = 0f;
        dashOnGroundTimer = moveStats.timeBtwDashesOnGround;

        //FX
        Quaternion particleRot = Quaternion.FromToRotation(Vector2.right, -dashDirection);
        Instantiate(dashParticles, transform.position, particleRot);
        dashSound.Play();
        anim.SetBool("isDashing", true);

        ResetJumpValues();
        ResetWallJumpValues();
        StopWallSliding();
    }

    private void ResetDashes()
    {
        numberOFDashesUsed = 0;
    }

    private void ResetDashValues()
    {
        isDashFastFalling = false;
        dashOnGroundTimer = -0.01f;
        anim.SetBool("isAirDashFalling", false);
    }

    private void Dash()
    {
        if (isDashing)
        {
            //stop the dash after the timer
            dashTimer += Time.fixedDeltaTime;
            if (dashTimer >= moveStats.dashTime)
            {
                if (isGrounded)
                {
                    ResetDashes();
                }
                else { anim.SetBool("isAirDashFalling", true); }

                isAirDashing = false;
                isDashing = false;

                anim.SetBool("isDashing", false);

                //start the time for upwards cancel
                if (!isJumping && !isWallJumping)
                {
                    dashFastFallTime = 0f;
                    dashFastFallReleaseSpeed = verticalVelocity;

                    if (!isGrounded)
                        isDashFastFalling = true;
                }

                return;
            }

            horizontalVelocity = new Vector2(moveStats.dashSpeed * dashDirection.x, 0f);

            if (dashDirection.y != 0f || isAirDashing)
                ChangeVerticalVelocity(moveStats.dashSpeed * dashDirection.y);
        }

        //HANDLE DASH CUT TIME
        else if (isDashFastFalling)
        {
            //new
            if (verticalVelocity > 0f)
            {
                if (dashFastFallTime < moveStats.dashTimeForUpwardsCancel)
                {
                    ChangeVerticalVelocity(Mathf.Lerp(dashFastFallReleaseSpeed, 0f, (dashFastFallTime / moveStats.dashTimeForUpwardsCancel)));
                }
                else if (dashFastFallTime >= moveStats.dashTimeForUpwardsCancel)
                {
                    IncrementVerticalVelocity(moveStats.gravity * moveStats.dashGravityOnReleaseMultiplier * Time.fixedDeltaTime);
                }

                dashFastFallTime += Time.fixedDeltaTime;
            }
            else
            {
                IncrementVerticalVelocity(moveStats.gravity * moveStats.dashGravityOnReleaseMultiplier * Time.fixedDeltaTime);
            }

            //if (dashFastFallTime >= moveStats.DashTimeForUpwardsCancel)
            //{
            //    IncrementVerticalVelocity(moveStats.gravity * moveStats.DashGravityOnReleaseMultiplier * Time.fixedDeltaTime);
            //}
            //else if (dashFastFallTime < moveStats.DashTimeForUpwardsCancel)
            //{
            //    ChangeVerticalVelocity(Mathf.Lerp(dashFastFallReleaseSpeed, 0f, (dashFastFallTime / moveStats.DashTimeForUpwardsCancel)));
            //}

        }
    }

    #endregion

    #region Timers

    private void CountTimers()
    {
        //JUMP BUFFER TIMER
        jumpBufferTimer -= Time.deltaTime;

        //HANDLE WALL JUMP BUFFER TIMER
        if (!ShouldApplyPostWallJumpBuffer())
        {
            wallJumpPostBufferTimer -= Time.deltaTime;
        }

        //HANDLE COYOTE TIMER
        if (!isGrounded)
        {
            coyoteTimer -= Time.deltaTime;
        }
        else { coyoteTimer = moveStats.jumpCoyoteTime; }

        //HANDLE DASH TIMER
        if (isGrounded)
        {
            dashOnGroundTimer -= Time.deltaTime;
        }
    }

    #endregion

    #region Collision Checks

    private void IsGrounded()
    {
        Vector2 boxCastOrigin = new Vector2(feetColl.bounds.center.x, feetColl.bounds.min.y);
        Vector2 boxCastSize = new Vector2(feetColl.bounds.size.x, moveStats.groundDetectionRayLength);
        if (!statRandomizer.isReverseGravity)
        {
            feetCheckDirection = Vector2.down;
        }
        else
        {
            feetCheckDirection = Vector2.up;
        }
        groundHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, feetCheckDirection, moveStats.groundDetectionRayLength, moveStats.groundLayer);
        if (groundHit.collider != null)
        {
            isGrounded = true;
        }
        else { isGrounded = false; }

        #region Debug Visualization
        if (moveStats.debugShowIsGroundedBox)
        {
            Color rayColor;
            if (isGrounded)
            {
                rayColor = Color.green;
            }
            else { rayColor = Color.red; }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * moveStats.groundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y), Vector2.down * moveStats.groundDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - moveStats.groundDetectionRayLength), Vector2.right * boxCastSize.x, rayColor);
        }
        #endregion
    }

    private void IsTouchingWall()
    {
        if (canWallSlide)
        {
            float originEndPoint = 0f;
            if (isFacingRight)
            {
                originEndPoint = bodyColl.bounds.max.x;
            }
            else { originEndPoint = bodyColl.bounds.min.x; }

            float adjustedHeight = bodyColl.bounds.size.y * moveStats.wallDetectionRayHeightMultiplier;
            //Vector2 boxCastOrigin = new Vector2(originEndPoint, bodyColl.bounds.max.y);
            Vector2 boxCastOrigin = new Vector2(originEndPoint, bodyColl.bounds.center.y);
            Vector2 boxCastSize = new Vector2(moveStats.wallDetectionRayLength, adjustedHeight);

            wallHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, transform.right, moveStats.wallDetectionRayLength, moveStats.groundLayer);
            if (wallHit.collider != null)
            {
                lastWallHit = wallHit;
                isTouchingWall = true;

                //if (moveStats.ResetJumpsOnWallTouch)
                //{
                //    numberOfJumpsUsed = 0;
                //}
            }
            else { isTouchingWall = false; }

            #region Debug Visualization

            if (moveStats.debugShowWallHitBox)
            {
                Color rayColor;
                if (isTouchingWall)
                {
                    rayColor = Color.green;
                }
                else { rayColor = Color.red; }

                Vector2 boxBottomLeft = new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y - boxCastSize.y / 2);
                Vector2 boxBottomRight = new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y - boxCastSize.y / 2);
                Vector2 boxTopLeft = new Vector2(boxCastOrigin.x - boxCastSize.x / 2, boxCastOrigin.y + boxCastSize.y / 2);
                Vector2 boxTopRight = new Vector2(boxCastOrigin.x + boxCastSize.x / 2, boxCastOrigin.y + boxCastSize.y / 2);

                Debug.DrawLine(boxBottomLeft, boxBottomRight, rayColor);
                Debug.DrawLine(boxBottomRight, boxTopRight, rayColor);
                Debug.DrawLine(boxTopRight, boxTopLeft, rayColor);
                Debug.DrawLine(boxTopLeft, boxBottomLeft, rayColor);
            }

            #endregion
        }

    }


    private void BumpedHead()
    {
        Vector2 boxCastOrigin = new Vector2(feetColl.bounds.center.x, headColl.bounds.max.y);
        Vector2 boxCastSize = new Vector2(feetColl.bounds.size.x * moveStats.headWidth, moveStats.headDetectionRayLength);

        headHit = Physics2D.BoxCast(boxCastOrigin, boxCastSize, 0f, Vector2.up, moveStats.headDetectionRayLength, moveStats.groundLayer);
        if (headHit.collider != null)
        {
            bumpedHead = true;
        }
        else { bumpedHead = false; }

        #region Debug Visualization

        if (moveStats.debugShowHeadBumpBox)
        {
            float headWidth = moveStats.headWidth;

            Color rayColor;
            if (bumpedHead)
            {
                rayColor = Color.green;
            }
            else { rayColor = Color.red; }

            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y), Vector2.up * moveStats.headDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x + (boxCastSize.x / 2) * headWidth, boxCastOrigin.y), Vector2.up * moveStats.headDetectionRayLength, rayColor);
            Debug.DrawRay(new Vector2(boxCastOrigin.x - boxCastSize.x / 2 * headWidth, boxCastOrigin.y + moveStats.headDetectionRayLength), Vector2.right * boxCastSize.x * headWidth, rayColor);
        }

        #endregion
    }

    private void CollisionChecks()
    {
        IsGrounded();
        IsTouchingWall();
        BumpedHead();
    }


    #endregion

    #region Jump Visualization

    private void DrawJumpArc(float moveSpeed, Color gizmoColor)
    {
        Vector2 startPosition = new Vector2(feetColl.bounds.center.x, feetColl.bounds.min.y);
        Vector2 previousPosition = startPosition;
        float speed = 0f;
        if (moveStats.drawRight)
        {
            speed = moveSpeed;
        }
        else { speed = -moveSpeed; }
        Vector2 velocity = new Vector2(speed, moveStats.initialJumpVelocity);

        Gizmos.color = gizmoColor;

        float timeStep = 2 * moveStats.timeTillJumpApex / moveStats.arcResolution; // time step for the simulation
                                                                                   //float totalTime = (2 * moveStats.TimeTillJumpApex) + moveStats.ApexHangTime; // total time of the arc including hang time

        for (int i = 0; i < moveStats.visualizationSteps; i++)
        {
            float simulationTime = i * timeStep;
            Vector2 displacement;
            Vector2 drawPoint;

            if (simulationTime < moveStats.timeTillJumpApex) // Ascending
            {
                displacement = velocity * simulationTime + 0.5f * new Vector2(0, moveStats.gravity) * simulationTime * simulationTime;
            }
            else if (simulationTime < moveStats.timeTillJumpApex + moveStats.apexHangTime) // Apex hang time
            {
                float apexTime = simulationTime - moveStats.timeTillJumpApex;
                displacement = velocity * moveStats.timeTillJumpApex + 0.5f * new Vector2(0, moveStats.gravity) * moveStats.timeTillJumpApex * moveStats.timeTillJumpApex;
                displacement += new Vector2(speed, 0) * apexTime; // No vertical movement during hang time
            }
            else // Descending
            {
                float descendTime = simulationTime - (moveStats.timeTillJumpApex + moveStats.apexHangTime);
                displacement = velocity * moveStats.timeTillJumpApex + 0.5f * new Vector2(0, moveStats.gravity) * moveStats.timeTillJumpApex * moveStats.timeTillJumpApex;
                displacement += new Vector2(speed, 0) * moveStats.apexHangTime; // Horizontal movement during hang time
                displacement += new Vector2(speed, 0) * descendTime + 0.5f * new Vector2(0, moveStats.gravity) * descendTime * descendTime;
            }

            drawPoint = startPosition + displacement;

            if (moveStats.stopOnCollision)
            {
                RaycastHit2D hit = Physics2D.Raycast(previousPosition, drawPoint - previousPosition, Vector2.Distance(previousPosition, drawPoint), moveStats.groundLayer);
                if (hit.collider != null)
                {
                    // If a hit is detected, stop drawing the arc at the hit point
                    Gizmos.DrawLine(previousPosition, hit.point);
                    break;
                }
            }

            Gizmos.DrawLine(previousPosition, drawPoint);
            previousPosition = drawPoint;
        }
    }


    #endregion

    #region Audio

    public void PlayStep1()
    {
        stepSound1.Play();
    }
    public void PlayStep2()
    {
        stepSound2.Play();
    }
    public void PlayWallSlide()
    {
        wallSlideSound.Play();
    }
    public void PlayWallGrab()
    {
        wallGrabSound.Play();
    }

    #endregion
}
