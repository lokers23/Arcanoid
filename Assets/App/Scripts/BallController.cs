using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D componentRigidbody;
    [SerializeField] private float force = 300f;
    [SerializeField] private float offsetX = 100f;
    private bool _isMoving;
    void Start()
    {
        componentRigidbody.bodyType = RigidbodyType2D.Kinematic;
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0) && !_isMoving)
        {
            _isMoving = true;
            componentRigidbody.bodyType = RigidbodyType2D.Dynamic;
            componentRigidbody.AddForce(new Vector2(offsetX, force));
        }
    }
}
