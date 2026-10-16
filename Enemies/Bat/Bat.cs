using Godot;
using System;

namespace learningproject2.Enemies.Bat;

[GlobalClass]
public partial class Bat : Enemy
{
	[Export]
	private Vector2 amplitudeRange = new Vector2(400.0f, 1000.0f);  // Амплитуда колебания  
	private float amplitude;  // Текущая амплитуда колебания
	private float spawnX = 1300f;
	private Vector2 spawnYRange = new Vector2(0f, 400f);

	public override void _Ready()
	{
		VisibleOnScreenNotifier2D notifier = new VisibleOnScreenNotifier2D();
		AddChild(notifier);
		notifier.Connect("screen_exited", new Callable(this, "_On_Screen_Exited"));
		
		amplitude = (float)GD.RandRange(
			amplitudeRange.X,
			amplitudeRange.Y
		);  // Случайная амплитуда колебания
	}

	public override Vector2 GetSpawnPosition()
	{
		return new Vector2(
			spawnX,
			(float)GD.RandRange(spawnYRange.X, spawnYRange.Y)
		);
	}
	
	private float offset;
	protected override void Move(float delta)
	{
		offset += velocity.Y * delta;
		Position += new Vector2(
			velocity.X * delta,
			Mathf.Sin(offset) * amplitude * delta
		);
	}
}
