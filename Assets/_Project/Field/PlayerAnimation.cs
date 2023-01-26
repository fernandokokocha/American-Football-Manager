using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmericanFootballManager
{
  public class PlayerAnimation : MonoBehaviour
  {
    public Animator Animator;
    public void MoveForward()
    {
      Animator.SetInteger("Move", 1);
      Animator.SetInteger("MoveForward", 1);
    }

    public void MoveBackward()
    {
      Animator.SetInteger("Move", 1);
      Animator.SetInteger("MoveForward", 0);
    }
    public void Idle()
    {
      Animator.SetInteger("Move", 0);
      Animator.SetInteger("MoveForward", 0);
    }
  }
}
