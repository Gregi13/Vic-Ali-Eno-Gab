using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce; // permets de modifier la vitesse de saut et de déplacement dans l'inspector
    
    
    private Inputs inputs;
    private Rigidbody2D myRB;
    private Vector2 direction;
    
    private void OnEnable()
    {
        inputs = new Inputs();
        inputs.Enable();
        inputs.Player.Move.performed += OnMovePerformed;  // lorsque l'on appuie sur le bouton "Move", le personnage bouge
        inputs.Player.Move.canceled += OnMoveCanceled;    // Lorsque l'on n'appuie plus sur le bouton "Move", le joueur arrete de bouger
        inputs.Player.Jump.performed += OnJumpPerformed;  // va chercher l'input "Jump" afin de permettre au joueur de sauter
    }

    private void OnJumpPerformed(InputAction.CallbackContext obj)
    {
        myRB.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // permets de donner un coup sec au saut 
    }
    
    
    
    private void OnMoveCanceled(InputAction.CallbackContext obj)
    {
        direction = Vector2.zero;
    }

    private void OnMovePerformed(InputAction.CallbackContext obj)
    {
        direction = obj.ReadValue<Vector2>();
    }

    private void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
    }
    
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        direction.y = 0;
        if (myRB.velocity.sqrMagnitude < maxSpeed)
            myRB.AddForce(direction * speed);
    }
}
