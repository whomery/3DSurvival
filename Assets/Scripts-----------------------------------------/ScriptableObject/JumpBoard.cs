using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBoard : MonoBehaviour
{
    public float jumpPower = 150f;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        // 간단한 바닥 판정 (레이캐스트)
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}
