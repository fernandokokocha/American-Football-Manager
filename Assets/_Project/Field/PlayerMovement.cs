using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmericanFootballManager
{
  public class PlayerMovement : MonoBehaviour
  {
    public Animator Animator;
    public bool Forward;
    private Rigidbody rb;

    void Start()
    {
      rb = GetComponent<Rigidbody>();
      if (Forward) MoveForward();
      else MoveBackward();
    //   InvokeRepeating("MoveForward", 0.0f, 10.0f);
    //   InvokeRepeating("MoveBackward", 5.0f, 10.0f);
      // rb.velocity = new Vector3(10, 0, 10);
    }

    void MoveForward()
    {
    //   float x = Random.Range(10.0f, 0.0f);
    //   float z = Random.Range(10.0f, 0.0f);
    //   rb.velocity = new Vector3(x, 0, z);
      rb.velocity = new Vector3(0, 0, -10);
      Animator.SetInteger("Move", 1);
      Animator.SetInteger("MoveForward", 1);
    }

    void MoveBackward()
    {
    //   float x = Random.Range(0.0f, -10.0f);
    //   float z = Random.Range(0.0f, -10.0f);
    //   rb.velocity = new Vector3(x, 0, z);
      rb.velocity = new Vector3(0, 0, 10);
      Animator.SetInteger("Move", 1);
      Animator.SetInteger("MoveForward", 0);
    }
  }
}
