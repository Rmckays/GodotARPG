using Godot;
using System;

public partial class slime : CharacterBody2D
{
    [Export] public float Speed = 20f;
    [Export] public float Limit = 0.5f;
    [Export] public Marker2D MarkerPosition;

    private AnimatedSprite2D _animatedSprite2D;
    private Vector2 startPosition;
    private Vector2 endPosition;
    public override void _Ready()
    {
        startPosition = Position;
        endPosition = MarkerPosition.GlobalPosition;
        _animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        UpdateVelocity();
        MoveAndSlide();
        UpdateAnimation(); 
        base._PhysicsProcess(delta);
    }

    private void UpdateAnimation()
    {
        var animationString = "walkUp";
        if (Velocity.Y > 0)
        {
            animationString = "walkDown";
        }
        else if (Velocity.Y < 0)
        {
            animationString = "walkUp";
        }
        else if (Velocity.X > 0)
        {
            animationString = "walkRight";
        }
        else if (Velocity.X < 0)
        {
            animationString = "walkLeft";
        }
        
        _animatedSprite2D.Play(animationString);
    }

    private void ChangeDirection()
    {
        var tempPosition = endPosition;
        endPosition = startPosition;
        startPosition = tempPosition;
    }
    private void UpdateVelocity()
    {
        var moveDirection = endPosition - Position;
        if (moveDirection.Length() < Limit)
        {
            Position = endPosition;
            ChangeDirection();
        }
        
        Velocity = moveDirection.Normalized() * Speed;
    }
}
