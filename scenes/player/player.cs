using Godot;
using System;


public partial class player : CharacterBody2D
{
    [Export] public int Speed = 10;
    private AnimationPlayer _animationPlayer; 
    
	public void HandleInput()
    {
        var movementDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        Velocity = movementDirection * Speed;
    }

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleInput();
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
}
