using Godot;
using System;

public partial class slime : CharacterBody2D
{
    [Export] public float Speed = 20f;
    [Export] public float Limit = 0.5f;
    [Export] public Marker2D MarkerPosition;

    private AnimationPlayer _animationPlayer;
    private Vector2 startPosition;
    private Vector2 endPosition;
    public override void _Ready()
    {
        startPosition = Position;
        endPosition = MarkerPosition.GlobalPosition;
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
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
        if (Velocity.Length() == 0)
        {
            _animationPlayer.Stop();
        }
        else
        {
            var direction = "Down";
            if (Velocity.X < 0)
            {
                direction = "Left";
            }
            else if (Velocity.X > 0)
            {
                direction = "Right";
            }
            else if (Velocity.Y < 0)
            {
                direction = "Up";
            }
            
            _animationPlayer.Play("walk" + direction);
        }
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
