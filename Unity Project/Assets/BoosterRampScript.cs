using UnityEngine;
using System.Collections;


public class BoosterRampScript : MonoBehaviour
{
    public float boostByMassMultiplier;
    public float boostDelay;

    private void OnCollisionEnter(Collision other)
    {
        Golf_Cart_Control golfCartControl = other.gameObject.GetComponent<Golf_Cart_Control>();
        if (golfCartControl != null)
        {
            Debug.Log("Golf cart detected!"); // Check if Golf_Cart_Control script is detected

            // Call the ApplyBoostDelayed method with a delay
            golfCartControl.Stop_Correction_Forces = true;
            golfCartControl.ApplyBoost(boostByMassMultiplier);
            //StartCoroutine(ApplyBoostDelayed(golfCartControl, boostDelay));
        }
    }

    private void OnCollisionExit(Collision other)
    {
        Golf_Cart_Control golfCartControl = other.gameObject.GetComponent<Golf_Cart_Control>();
        if (golfCartControl != null)
        {
            golfCartControl.Started_Boost = true;
        }
    }

    // Coroutine to apply boost after a delay
    /*private IEnumerator ApplyBoostDelayed(Golf_Cart_Control golfCartControl, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Apply the boost after the delay
        golfCartControl.ApplyBoost(boostByMassMultiplier);
    }*/
}
