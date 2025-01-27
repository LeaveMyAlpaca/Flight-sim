using Godot;
using System;

public partial class Thruster : Node3D
{
	[Export] private RigidBody3D rb;
	private const string throttleUpAction = "throttleUp";
	private const string throttleDownAction = "throttleDown";

	[Export] public float thrusterStrength = 100;
	public float throttle = 0;
	private double throttleChangeSpeedModifier = .2;
	[Export] private EngineSoundController engineSoundController;



	public override void _Process(double delta)
	{
		if (Input.IsActionPressed(throttleUpAction))
		{
			ChangeThrottle(Input.GetActionStrength(throttleUpAction), delta);
		}
		if (Input.IsActionPressed(throttleDownAction))
		{
			ChangeThrottle(-Input.GetActionStrength(throttleDownAction), delta);
		}
	}

	private void ChangeThrottle(float amount, double delta)
	{
		throttle = (float)Mathf.Clamp(throttle + amount * delta * throttleChangeSpeedModifier, 0, 1);
		engineSoundController.Throttle = throttle;
	}

	public override void _PhysicsProcess(double delta)
	{

		rb.ApplyForce(rb.Transform.Basis.Z * throttle * thrusterStrength);
		base._PhysicsProcess(delta);
	}




}
