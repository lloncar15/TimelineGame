using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static readonly int Movement = Animator.StringToHash("movement");

    [SerializeField]
    private PlayerCharacterProperties _playerCharacterProperties;
    [SerializeField]
    private PlayerCharacterProperties _defaultPlayerCharacterProperties;

    [SerializeField]
    private Rigidbody2D _rigidbody2D;
    
    private Animator _animator;
    private bool _isFlipped = false;

    private void Start()
    {
        _playerCharacterProperties = _defaultPlayerCharacterProperties;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        HorizontalMovement(x);
    }

    private void HorizontalMovement(float x)
    {
        _animator.SetFloat(Movement, Mathf.Abs(x));
        _rigidbody2D.linearVelocity = (new Vector2(x * _playerCharacterProperties._moveSpeed, _rigidbody2D.linearVelocity.y));
        
        if (x > 0 && _isFlipped || x < 0 && !_isFlipped)
        {
            Flip();
        }
    }
    
    private void Flip()
    {
        /*
        _isFlipped = !_isFlipped;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        */
    }
}
