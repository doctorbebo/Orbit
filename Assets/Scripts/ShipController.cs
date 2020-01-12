using UnityEngine;

public class ShipController : MonoBehaviour
{
    public HowToPlayScript howToPlayScript;

    public static bool isPlayerDead;
    public static int score;

    public Rigidbody2D rb;
    public float launchVelocity = 14f;
    public float lerpTimerSpeed = 2.5f;
    public float radius = 3.8f;
    public float lerpRadiusMin = 1f;
    public float orbitSpeed = 3f;
    public float spaceRotationSpeed = 105f;

    private static bool isRewardedRestart;
    private static bool canBeRewardedRestart = true;

    [HideInInspector]
    public float xPosCamera;
    public float angle;

    public ParticleSystem particleSystem1, particleSystem2;
    public ParticleSystem.EmissionModule particleSystemEmission1, particleSystemEmission2;

    float orbitSpeedOriginal;
    float OriginalGravityScale;
    float lerpTimer;
    float LerpRadius;
    float spaceRotationTime;

    int lerpSwitchIndex;

    bool isOrbiting;
    bool isCounterClockWise;
    bool isLaunchable;

    Vector3 pastPos, shipPositionChange;
    Vector2 PivotPosition;

    public enum GameMode
    {
        NotPlaying,
        Playing,
        Learning
    }

    public static GameMode gameMode = GameMode.NotPlaying;

    private void Start()
    {
        OriginalGravityScale = rb.gravityScale;
        orbitSpeedOriginal = orbitSpeed;
        particleSystemEmission1 = particleSystem1.emission;
        particleSystemEmission2 = particleSystem2.emission;
        isPlayerDead = false;

        if(isRewardedRestart)
        {
            isRewardedRestart = false;
        }else
        {
            score = -1;
        }
        isLaunchable = true;
        Launch();
    }

    private void Update()
    {
        Debug.Log(gameMode);
        if (Input.GetMouseButtonDown(0))
        {
            switch (gameMode)
            {
                case GameMode.NotPlaying:
                    return;
                case GameMode.Playing:
                    Launch();
                    break;
                case GameMode.Learning:
                    if(Time.timeScale < 0.5)
                    {
                        Launch();
                        Time.timeScale = 1f;
                        howToPlayScript.ShowOrHideHelpText(false);
                    }
                    break;
            }
            
        }
    }

        private void FixedUpdate()
        {
            if (isOrbiting)
            {
                OrbitMechanic();
                RotationMechanic();
                xPosCamera = PivotPosition.x;
            }
            else
            {
                xPosCamera = transform.position.x;
                SpaceRotationMechanics();
            }

            CheckIfPlayerIsLostInSpace();

            shipPositionChange = transform.position - pastPos;
            pastPos = transform.position;
        }


        void OrbitMechanic()
        {
            if (isCounterClockWise)
            {
                angle -= orbitSpeed * Time.fixedDeltaTime;
            }
            else
            {
                angle += orbitSpeed * Time.fixedDeltaTime;
            }

            float posX = PivotPosition.x + Mathf.Sin(angle) * radius;
            float posY = PivotPosition.y + Mathf.Cos(angle) * radius;
            Vector3 OrbitPosition = new Vector3(posX, posY);

            posX = PivotPosition.x + Mathf.Sin(angle) * LerpRadius;
            posY = PivotPosition.y + Mathf.Cos(angle) * LerpRadius;
            Vector3 LerpOrbitPosition = new Vector3(posX, posY);



            if (angle * Mathf.Rad2Deg >= 360f)
            {
                angle -= Mathf.Deg2Rad * 360;
            }
            else if (angle * Mathf.Rad2Deg < 0f)
            {
                angle += Mathf.Deg2Rad * 360;
            }


            switch (lerpSwitchIndex)
            {
                case 1:
                    transform.position = Vector3.Lerp(OrbitPosition, LerpOrbitPosition, lerpTimer);
                    lerpTimer += lerpTimerSpeed * Time.deltaTime;
                    orbitSpeed += lerpTimer / (7f - 2.5f * (radius - LerpRadius));
                    if (lerpTimer > 0.98f)
                    {
                        lerpSwitchIndex++;
                    }
                    break;
                case 2:
                    transform.position = Vector3.Lerp(OrbitPosition, LerpOrbitPosition, lerpTimer);
                    lerpTimer -= lerpTimerSpeed * Time.deltaTime;
                    orbitSpeed -= lerpTimer / (7f - 2.5f * (radius - LerpRadius));
                    if (lerpTimer < 0f)
                    {
                        lerpSwitchIndex = 0;
                    }
                    break;
                default:
                    transform.position = OrbitPosition;
                    orbitSpeed = orbitSpeedOriginal;
                    particleSystemEmission1.enabled = false;
                    particleSystemEmission2.enabled = false;

                    break;
            }


        }

        void RotationMechanic()
        {
            float rotationOffset = angle * Mathf.Rad2Deg;
            float shipRotation = (isCounterClockWise) ? -70 + (orbitSpeed * 2) : 70 - (orbitSpeed * 2);
            Quaternion desiredRotation = Quaternion.Euler(0, 0, -rotationOffset + shipRotation);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, 0.125f);

        }

        void SpaceRotationMechanics()
        {
            spaceRotationTime += spaceRotationSpeed * Time.deltaTime / 500;

            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0), spaceRotationTime);
        }

        void InitializeOrbit()
        {
            Vector2 pos = transform.position;
            Vector2 futurePos = new Vector2(shipPositionChange.x, shipPositionChange.y) + pos;
            float PosAngle = Mathf.Atan2(pos.x - PivotPosition.x, pos.y - PivotPosition.y) * Mathf.Rad2Deg;
            float FuturePosAngle = Mathf.Atan2(futurePos.x - PivotPosition.x, futurePos.y - PivotPosition.y) * Mathf.Rad2Deg;

            if (PosAngle < 0)
            {
                PosAngle = 180 + PosAngle + 180;
            }
            if (FuturePosAngle < 0)
            {
                FuturePosAngle = 180 + FuturePosAngle + 180;
            }
            if (PosAngle >= 350f)
            {
                FuturePosAngle += 360f;
            }

            if (FuturePosAngle < PosAngle)
            {
                isCounterClockWise = true;
            } else
            {
                isCounterClockWise = false;
            }

            angle = PosAngle * Mathf.Deg2Rad;

            LerpRadius = Mathf.Abs(Vector3.Distance(new Vector3(pos.x, pos.y) + shipPositionChange * 6f, PivotPosition));
            if (LerpRadius > radius)
            {
                LerpRadius = radius;
            }
            if (LerpRadius < lerpRadiusMin)
            {
                LerpRadius = lerpRadiusMin;
            }

            if (shipPositionChange == Vector3.zero)
                Debug.LogError(shipPositionChange);

        }

        void Launch()
        {
            if (isLaunchable)
            {
 
                    rb.gravityScale = OriginalGravityScale;
                    spaceRotationTime = 0;
                    isOrbiting = false;
                    rb.AddRelativeForce(Vector2.down * 100 * launchVelocity);
                    isLaunchable = false;
                    angle = 0f;
                    particleSystemEmission1.enabled = true;
                    particleSystemEmission2.enabled = true;
                    orbitSpeed = orbitSpeedOriginal;
             }

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            PivotPosition = other.transform.position;
            InitializeOrbit();
            RigidBodyMechanics();
            isOrbiting = true;
            lerpTimer = 0.1f;
            lerpSwitchIndex = 1;
            score++;

            isLaunchable = true;
        }

        public void RigidBodyMechanics()
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
        }

        void CheckIfPlayerIsLostInSpace()
        {
            if (Mathf.Abs(transform.position.y) >= 30f)
            {
                isPlayerDead = true;
                gameObject.SetActive(false);
            //    AdManager.ShowAd();
            }
        }
        public void SetIsRewardedNewLife(bool b)
        {
        isRewardedRestart = b;
        }
}

    




