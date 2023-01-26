using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmericanFootballManager
{
  public class PlayerMovement : MonoBehaviour
  {
    public bool RightSide;
    private Vector3 FacingDirection;
    private Rigidbody rb;
    public float speed;
    public PlayerAnimation PlayerAnimation;

    void Start()
    {
      rb = GetComponent<Rigidbody>();

      if (RightSide) FacingDirection = Vector3.left;
      else FacingDirection = Vector3.right;

      transform.forward = FacingDirection;

      StartCoroutine(WalkingCycle());
    }

    IEnumerator WalkingCycle()
    {
      while (true)
      {
        WalkBack();
        yield return new WaitForSeconds(3);
        Idle();
        yield return new WaitForSeconds(1);
        WalkForward();
        yield return new WaitForSeconds(3);
        Idle();
        yield return new WaitForSeconds(1);
      }
    }

    void WalkBack()
    {
      // transform.Translate(FacingDirection * speed * Time.deltaTime, Space.World)

      rb.velocity = FacingDirection * -1 * speed;
      PlayerAnimation.MoveBackward();
    }

    void WalkForward()
    {
      // transform.Translate(FacingDirection * speed * Time.deltaTime, Space.World)

      rb.velocity = FacingDirection * speed;
      PlayerAnimation.MoveForward();
    }

    void Idle()
    {
      rb.velocity = Vector3.zero;
      PlayerAnimation.Idle();
    }
  }
}
