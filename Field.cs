using Godot;
using System;

public partial class Field : Node2D
{
	[Export]
	private Godot.Collections.Array<PackedScene> Objects = new Godot.Collections.Array<PackedScene>();
	private Timer SpawnTimer = new Timer();
	private float SpawnInterval; // Time in seconds between spawns
	[Export]
	private Vector2 SpawnIntervalRange = new Vector2(1.5f, 0.5f); // Range for random spawn interval
	private RandomNumberGenerator random = new RandomNumberGenerator();
	public override void _Ready()
	{
		random.Randomize();
		SpawnEnemy();
		SpawnTimer.WaitTime = (float)GD.RandRange(
			SpawnIntervalRange.Y,
			SpawnIntervalRange.X
		);
		SpawnTimer.OneShot = false;
		SpawnTimer.Autostart = true;
		AddChild(SpawnTimer);
		SpawnTimer.Timeout += OnSpawnTimerTimeout;
	}

	private void OnSpawnTimerTimeout()
	{
		SpawnEnemy();
		SpawnTimer.WaitTime = (float)GD.RandRange(
			SpawnIntervalRange.Y,
			SpawnIntervalRange.X
		);
	}

	private void SpawnEnemy()
	{
		int randomIndex = random.RandiRange(0, Objects.Count - 1);
		GD.Print($"Spawning enemy of type: {Objects[randomIndex].ResourcePath}");
		DamagingObject obj = (DamagingObject)Objects[randomIndex].Instantiate();
		AddChild(obj);
		Enemy enemy = obj as Enemy;
		if (enemy != null)
			obj.Position = enemy.GetSpawnPosition();
		else
			obj.Position = new Vector2(1300f, 480f); // Default spawn position if not an Enemy
	}
}
