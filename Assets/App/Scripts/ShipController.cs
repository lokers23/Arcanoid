using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShipController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbodyComponent;
    [SerializeField] private Camera currentCamera;
    [SerializeField] private SpriteRenderer sprite;
    
    [SerializeField] private float speed = 5f;
    private float _stepX = 0f;
    private bool _mousePressed;
    private float border;
    private Vector2 _startPosition ;

    private void Awake()
    {
        _startPosition = currentCamera.transform.position;
        var leftBottomCorner = currentCamera.ViewportToWorldPoint(new Vector2(0, 0));
        var rightBottomCorner = currentCamera.ViewportToWorldPoint(new Vector2(1, 0));
        border = Vector2.Distance(leftBottomCorner, rightBottomCorner) / 2;
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
        
        var size = sprite.size.x / 2;
        position = Mathf.Clamp(position, -border + size, border - size);
        rigidbodyComponent.MovePosition(new Vector2(position, rigidbodyComponent.position.y));
    }
    
    private void ResetTouch()
    {
        _mousePressed = false;
    }
}
