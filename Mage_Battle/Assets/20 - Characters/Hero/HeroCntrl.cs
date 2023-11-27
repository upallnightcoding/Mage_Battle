using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroCntrl : MonoBehaviour
{
    [SerializeField] private InputCntrl inputCntrl;
    [SerializeField] private float maximumSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private SpellSO spell;
    [SerializeField] private Transform castPoint;

    private Vector3 moveDirection;
    private Vector2 playerMove;
    private SpellCasterCntrl spellCaster = null;

    private CharacterController charCntrl;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        charCntrl = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        spellCaster = new SpellCasterCntrl();
        spellCaster.Set(spell);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement(Time.deltaTime);

        if (inputCntrl.HasFired)
        {
            HasFired();
        }
    }

    private void HasFired()
    {
        spellCaster.Cast(castPoint.position, transform.forward);
        inputCntrl.HasFired = false;
    }

    private void PlayerMovement(float dt)
    {
        playerMove = inputCntrl.GetPlayerMovement();

        moveDirection.x = playerMove.x; // Horizontal
        moveDirection.y = 0.0f;
        moveDirection.z = playerMove.y; // Vertical

        float magnitude = Mathf.Clamp01(moveDirection.magnitude);

        animator.SetFloat("Speed", magnitude, 0.05f, dt);

        if (moveDirection != Vector3.zero)
        {
            moveDirection.Normalize();

            Vector3 velocity = magnitude * maximumSpeed * moveDirection;

            Debug.Log($"Velocity: {velocity}");

            charCntrl.Move(velocity * dt);

            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * dt);
        }
    }
}
