using Godot;

namespace ARPG.scenes.world;

public partial class followCamera : Camera2D
{
	[Export]
	public TileMap TileMap;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var mapRect = TileMap.GetUsedRect();
		var tileSize = TileMap.CellQuadrantSize;
		var worldSize = mapRect.Size * tileSize;
		LimitRight = worldSize.X;
		LimitBottom = worldSize.Y;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}