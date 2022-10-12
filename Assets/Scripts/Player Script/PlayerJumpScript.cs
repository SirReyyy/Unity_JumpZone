using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpScript : MonoBehaviour
{
    public static PlayerJumpScript instance;

    private Rigidbody2D myBody;
    private Animator anim;

    [SerializeField]
    private float forceX, forceY;
    private float tresholdX = 7f, tresholdY = 14f;

    private bool setPower, didJump;

    private Slider powerBar;
    private float powerBarTreshold = 10f;
    private float powerBarValue = 0f;

    void Awake() {
        MakeInstance();
        Initialize();
    }

    void Update() {
        SetPower();
    }

    void Initialize() {
        powerBar = GameObject.Find("PowerBar").GetComponent<Slider>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        powerBar.minValue = 0f;
        powerBar.maxValue = 10f;
        powerBar.value = powerBarValue;
    }

    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    void SetPower() {
        if(setPower) {
            forceX += tresholdX * Time.deltaTime;
            forceY += tresholdY * Time.deltaTime;

            if(forceX > 6.5f) {
                forceX = 6.5f;
            }

            if(forceY > 13.5f) {
                forceY = 13.5f;
            }

            powerBarValue += powerBarTreshold * Time.deltaTime;
            powerBar.value = powerBarValue;
        }
    }

    public void SetPower(bool setPower) {
        this.setPower = setPower;

        if(!setPower) {
            Jump();
        }
    }

    void Jump() {
        myBody.velocity = new Vector2(forceX, forceY);
        forceX = forceY = 0f;
        didJump = true;
        anim.SetBool("PlayerJump", didJump);

        powerBarValue = 0f;
        powerBar.value = powerBarValue;
    }

    private void OnTriggerEnter2D(Collider2D target) {
        if (didJump) {
            didJump = false;

            if (target.tag == "Platform") {
                if(GameManagerScript.instance != null) {
                    GameManagerScript.instance.CreateNewPlatformAndLerp(target.transform.position.x);
                }
                SoundManagerScript.instance.LandSound();

                if(ScoreManager.instance != null) {
                    ScoreManager.instance.IncrementScore();
                }
                target.tag = "Start";
            }
        }

        if (target.tag == "Dead") {
            if (GameOverScript.instance != null) {
                GameOverScript.instance.GameOverShowPanel();
            }
            SoundManagerScript.instance.FallSound();
            Destroy(gameObject);
        }
    }
}
