using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour
{
    public float targetTime;
    public TMP_Text timerText = null;
    public GameObject finishFlag = null;
    public GameObject golfCart;
    private bool timerOver = false;

    void Start()
    {
        targetTime++;
    }

    void Update()
    {

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetCar();
            targetTime = 121;
        }

        if (GameIsFinished())
        {
            if (timerText != null)
            {
                timerText.text = "[M]enu\n[P]lay Again";
            }
            if (Input.GetKey(KeyCode.M))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (Input.GetKey(KeyCode.P))
            {
                ReloadLevel();
            }
        }
        else if (timerText != null && !timerOver)
        {
            targetTime -= Time.deltaTime;
            if (targetTime <= 0f)
            {
                timerOver = true;
                TimeOver();
                return;
            }
            int minutes = Mathf.FloorToInt(targetTime / 60f);
            int seconds = Mathf.FloorToInt(targetTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    private void ResetCar()
    {
        // Reset car's position to initial position
        golfCart.transform.position = new Vector3(66f, 0f, 12f);

        // Reset car's rotation to zero
        golfCart.transform.rotation = Quaternion.identity;

        // Freeze the golf cart's movement and rotation
        Rigidbody cartRigidbody = golfCart.GetComponent<Rigidbody>();
        if (cartRigidbody != null)
        {
            cartRigidbody.velocity = Vector3.zero;
            cartRigidbody.angularVelocity = Vector3.zero;
            cartRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }

        // Unfreeze the golf cart's movement and rotation after 1 second
        Invoke("UnfreezeCar", 1f);
    }

    private void UnfreezeCar()
    {
        // Unfreeze the golf cart's movement and rotation
        Rigidbody cartRigidbody = golfCart.GetComponent<Rigidbody>();
        if (cartRigidbody != null)
        {
            cartRigidbody.constraints = RigidbodyConstraints.None;
        }
    }




    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void TimeOver()
    {
        StartCoroutine(PlayTimeOverSoundAndReloadLevel());
    }

    private IEnumerator PlayTimeOverSoundAndReloadLevel()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(3f);
        ReloadLevel();
    }

    private bool GameIsFinished()
    {
        if (finishFlag == null)
        {
            return false;
        }

        float positionX = finishFlag.transform.position.x;
        float positionZ = finishFlag.transform.position.z;

        float cartPositionX = golfCart.transform.position.x;
        float cartPositionZ = golfCart.transform.position.z;
        return cartPositionZ + 10 > positionZ && cartPositionZ - 10 < positionZ
            && cartPositionX + 10 > positionX && cartPositionX - 10 < positionX;
    }
}
