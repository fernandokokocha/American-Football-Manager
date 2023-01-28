using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AmericanFootballManager
{
  public class PlayerBehaviour : MonoBehaviour
  {
    public PlayerMovement PlayerMovement;
    void Start()
    {
      // StartCoroutine(WalkingCycle());
    }
    IEnumerator WalkingCycle()
    {
      while (true)
      {
        yield return new WaitForSeconds(1);
        PlayerMovement.WalkBack();
        yield return new WaitForSeconds(1);
        PlayerMovement.Idle();
        yield return new WaitForSeconds(1);
        PlayerMovement.WalkForward();
        yield return new WaitForSeconds(1);
        PlayerMovement.Idle();

        yield return new WaitForSeconds(1);
        PlayerMovement.TurnAndWalkBack();
        yield return new WaitForSeconds(1);
        PlayerMovement.Idle();
        yield return new WaitForSeconds(1);
        PlayerMovement.TurnAndWalkForward();
        yield return new WaitForSeconds(1);
        PlayerMovement.Idle();
      }
    }
  }
}
