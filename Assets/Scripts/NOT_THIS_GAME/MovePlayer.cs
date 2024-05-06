using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    //Variables for infinte runner with a MoveLeft and MoveRight method using the InputAction.CallbackContext context
    public float HorizontalVelocity = 0;
    public InputAction moveLeft;
    public InputAction moveRight;

    public int laneNumber = 2;
    public string controlLocked = "n";

    // get the 3D player rigidbody in the update with the GetComponent method
    private void Update()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(HorizontalVelocity, 0, -5);

        if ((moveLeft.triggered) && (laneNumber > 1) && (controlLocked == "n"))
        {
            MoveLeft();

        }
        if ((moveRight.triggered) && (laneNumber < 3) && (controlLocked == "n"))
        {
            MoveRight();

        }

    }

    // MoveLeft method using InputAction.CallbackContext context parameter
    public void MoveLeft()
    {
        HorizontalVelocity = 1;
        StartCoroutine(StopSlide());
        laneNumber -= 1;
        controlLocked = "y";
    }

    // MoveRight method using InputAction.CallbackContext context parameter
    public void MoveRight()
    {
        HorizontalVelocity = -1;
        StartCoroutine(StopSlide());
        laneNumber += 1;
        controlLocked = "y";
    }

    // OnCollisionEnter method using Collision parameter
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lethal")
        {
            Destroy(gameObject);
            
            Debug.Log("Game Over");
        }
    }

    // Stop method using StopSlide Ienumerator using yield return new WaitForSeconds
    IEnumerator StopSlide()
    {
        yield return new WaitForSeconds(0.5f);
        HorizontalVelocity = 0;
        controlLocked = "n";
    }


}
