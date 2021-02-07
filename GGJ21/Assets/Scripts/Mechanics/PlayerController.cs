﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;
        public AudioClip flyingAudio;
        public AudioClip mushroomJumpAudio;
        public AudioClip peckAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        public int maxJumpCount = 1;

        private int jumpCount = 0;

        [HideInInspector]
        public bool isPecking = false;

        [HideInInspector]
        public bool canPeckAlready = false;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            animator.SetBool("falling", true);
        }

        protected override void Update()
        {
            if(animator.GetBool("falling") && IsGrounded)
            {
                animator.SetBool("falling", false);
            }

            if (controlEnabled && !GameController.Instance.cinematic1)
            {
                move.x = Input.GetAxis("Horizontal");
                if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
                {
                    InitJump();
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    EndJump();
                }

                if (canPeckAlready && Input.GetButtonDown("Peck"))
                {
                    isPecking = true;
                }
                else if(Input.GetButtonUp("Peck"))
                {
                    isPecking = false;
                }

                animator.SetBool("pecking", isPecking);

                if (isPecking)
                {
                    if(!audioSource.isPlaying)
                        audioSource.PlayOneShot(peckAudio);
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    jumpCount = 0;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            //if (jump && IsGrounded)
            if (jump && jumpCount <= maxJumpCount)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public void InitJump()
        {
            jumpState = JumpState.PrepareToJump;

            jumpCount++;
        }

        public void EndJump()
        {
            stopJump = true;
            Schedule<PlayerStopJump>().player = this;
        }

        public void MushBounce()
        {
            if (jump && jumpCount <= maxJumpCount)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Aux01"))
            {
                AudioManager.Instance.CheckColiision(AudioType.Aux01);
            }
            else if (collision.CompareTag("Aux02"))
            {
                AudioManager.Instance.CheckColiision(AudioType.Aux02);
            }
            else if (collision.CompareTag("Aux03"))
            {
                AudioManager.Instance.CheckColiision(AudioType.Aux03);
            }
        }
    }
}