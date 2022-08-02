using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbodyComponent;
    [SerializeField] private float speed = 15f;
    [SerializeField] private Camera currentCamera;
    
    private float _stepX = 0f;
    private bool _mousePressed;
    private Vector2 _startPosition ;

    private void Awake()
    {
        _startPosition = currentCamera.transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _mousePressed = true;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            ResetTouch();
        }

        CheckTouch();
    }

    private void CheckTouch()
    {
        if (!_mousePressed)
        {
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            return;
        }
        
        var worldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        _stepX =  worldPosition.x > _startPosition.x ? 1f : -1f;
        var position = rigidbodyComponent.position.x + _stepX * speed * Time.fixedDeltaTime;
        rigidbodyComponent.MovePosition(new Vector2(position, rigidbodyComponent.position.y));
    }
    
    private void ResetTouch()
    {
        _mousePressed = false;
    }
}
