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
    private Nullable<Vector3> RotateTowards;
    private Rigidbody rb;
    public float speed;
    public float turnRate = 500;
    public PlayerAnimation PlayerAnimation;

    void Start()
    {
      rb = GetComponent<Rigidbody>();

      if (RightSide) FacingDirection = Vector3.left;
      else FacingDirection = Vector3.right;
      transform.forward = FacingDirection;

      BackDirection = FacingDirection * -1;

      RotateTowards = null;

      StartCoroutine(WalkingCycle());
    }

    void Update()
    {
      if (RotateTowards.HasValue)
      {
        Quaternion toRotation = Quaternion.LookRotation(RotateTowards.Value, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, turnRate * Time.deltaTime);
      }
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
      // TurnImmediately(direction);
      TurnOverTime(direction);
      rb.velocity = direction * speed;
      PlayerAnimation.MoveForward();
    }

    void TurnImmediately(Vector3 direction)
    {
      transform.forward = direction;
    }

    void TurnOverTime(Vector3 direction)
    {
      RotateTowards = direction;
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
      RotateTowards = null;
      rb.velocity = Vector3.zero;
      PlayerAnimation.Idle();
    }
  }
}
