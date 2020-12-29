using Godot;
using kraken.src;
using kraken.src.kraken.segments;
using System.Linq;

public class tentacle : Spatial
{
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.segments = new SegmentFactory(this)
			.WithStart(new SegmentPosition { Duration = 8.0f, Z = 15f, Y = .25f, })
			.WithEnd(new SegmentPosition { Duration = 2.0f, Z = 10f, Y = 0f })
			.GetAllSegments()
			.ToArray();

		Debug.Print<AlwaysPrint>($"Read, steady, go tentacle");		
	}

	private SegmentPosition[] segments;

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(float delta)
	{
		foreach (var segment in this.segments)
		{
			segment.ApplyAnimation();
		}

	}
}
