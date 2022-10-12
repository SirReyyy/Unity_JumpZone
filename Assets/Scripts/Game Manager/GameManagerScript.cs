using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject Platform;

    [SerializeField]
    private GameObject Start;

    private float minX = -2.5f, maxX = 2.5f;
    private float minY = -3.0f, maxY = -5.5f;

    private bool lerpCamera;
    private float lerpTime = 1.5f;
    private float lerpX;

    void Awake() {
        MakeInstance();
        CreateInitialPlatforms();
    }

    void Update() {
        if(lerpCamera) {
            LerpCamera();
        }
    }

    void MakeInstance() {
        if(instance == null) {
            instance = this;
        }
    }

    void LerpCamera() {
        float x = Camera.main.transform.position.x;

        x = Mathf.Lerp(x, lerpX, lerpTime * Time.deltaTime);
        Camera.main.transform.position = new Vector3(x, Camera.main.transform.position.y, Camera.main.transform.position.z);

        if(Camera.main.transform.position.x >= (lerpX - 0.07f)) {
            lerpCamera = false;
        }
    }

    void CreateInitialPlatforms() {
        Vector3 temp = new Vector3(Random.Range(minX, minX + 1.2f), Random.Range(minY, maxY), 0);
        Instantiate(Start, temp, Quaternion.identity);

        temp.y += 2f;
        Instantiate(Player, temp, Quaternion.identity);

        temp = new Vector3(Random.Range(maxX, maxX - 1.2f), Random.Range(minY, maxY), 0);
        Instantiate(Platform, temp, Quaternion.identity);
    }

    public void CreateNewPlatformAndLerp(float lerpPosition) {
        CreateNewPlatform();

        lerpX = lerpPosition + maxX;
        lerpCamera = true;
    }

    void CreateNewPlatform() {
        float cameraX = Camera.main.transform.position.x;

        float newMaxX = (maxX * 2) + cameraX;
        Instantiate(Platform, new Vector3(Random.Range(newMaxX, newMaxX - 1f), Random.Range(minY, maxY), 0), Quaternion.identity);
    }
}
