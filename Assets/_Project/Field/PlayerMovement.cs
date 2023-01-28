using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AmericanFootballManager
{
  public class PlayerMovement : MonoBehaviour
  {
    public bool RightSide;
    private Vector3 FacingDirection;
    private Vector3 BackDirection;
    private Rigidbody rb;
    public float speed;
    public PlayerAnimation PlayerAnimation;
    public PlayerRotation PlayerRotation;
    void Start()
    {
      rb = GetComponent<Rigidbody>();

      if (RightSide) FacingDirection = Vector3.left;
      else FacingDirection = Vector3.right;
      transform.forward = FacingDirection;

      BackDirection = FacingDirection * -1;

      StartCoroutine(WalkingCycle());
    }
    IEnumerator WalkingCycle()
    {
      while (true)
      {
        WalkBack();
        yield return new WaitForSeconds(1);
        Idle();
        yield return new WaitForSeconds(1);
        WalkForward();
        yield return new WaitForSeconds(1);
        Idle();
        yield return new WaitForSeconds(1);

        TurnAndWalk(BackDirection);
        yield return new WaitForSeconds(1);
        Idle();
        yield return new WaitForSeconds(1);
        TurnAndWalk(FacingDirection);
        yield return new WaitForSeconds(1);
        Idle();
        yield return new WaitForSeconds(1);
      }
    }
    void TurnAndWalk(Vector3 direction)
    {
      PlayerRotation.TurnOverTime(direction);
      rb.velocity = direction * speed;
      PlayerAnimation.MoveForward();
    }
    void WalkBack()
    {
      rb.velocity = BackDirection * speed;
      PlayerAnimation.MoveBackward();
    }
    void WalkForward()
    {
      rb.velocity = FacingDirection * speed;
      PlayerAnimation.MoveForward();
    }
    void Idle()
    {
      PlayerRotation.Stop();
      rb.velocity = Vector3.zero;
      PlayerAnimation.Idle();
    }
  }
}
