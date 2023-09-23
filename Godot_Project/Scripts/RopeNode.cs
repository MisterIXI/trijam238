using Godot;
using System;
using System.Diagnostics;


public partial class RopeNode : Node2D
{
	[Export]
	private int _startSegmentCount = 5;
	[Export]
	private int _constraintIterations = 10;
	[Export]
	private float _segmentLength = 10;

	[Export]
	private NodePath _startNode;
	private Node2D _startNode2D;

	[Export]
	private NodePath _endNode;
	private Node2D _endNode2D;

	[Export]
	private Vector2 _gravity = new Vector2(-1, 0);
	private Line2D _line;
	private Vector2[] _segments;


	private int printCounter = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_line = GetNode<Line2D>("Line2D");
		_startNode2D = GetNode<Node2D>(_startNode);
		_endNode2D = GetNode<Node2D>(_endNode);
		_segments = new Vector2[_startSegmentCount + 2];
		Vector2 pos = _startNode2D.Position;
		Vector2 step = (_endNode2D.Position - _startNode2D.Position) / (_startSegmentCount + 1);
		for (int i = 1; i < _startSegmentCount + 1; i++)
		{
			pos += step;
			_segments[i] = pos;
		}
		_segments[0] = _startNode2D.Position;
		_segments[_startSegmentCount + 1] = _endNode2D.Position;
		_line.Points = _segments;
	}


	public override void _PhysicsProcess(double delta)
	{
		_segments[0] = _startNode2D.Position;
		_segments[_startSegmentCount + 1] = _endNode2D.Position;
		// Save the current positions of the segments
		Vector2[] oldSegments = (Vector2[])_segments.Clone();

		// Update the positions of the segments using verlet integration
		for (int i = 0; i < _startSegmentCount + 2; i++)
		{
			Vector2 velocity = _segments[i] - oldSegments[i];
			_segments[i] += velocity + _gravity * (float)(delta * delta);
		}

		// Constrain the rope to its maximum length
		for (int i = 0; i < _constraintIterations; i++)
		{
			ConstrainRope();
		}

		// Update the start and end nodes
		_startNode2D.Position = _segments[0];
		_endNode2D.Position = _segments[_startSegmentCount + 1];

		// Update the line renderer
		_line.Points = _segments;

		// // Print the segments
		// printCounter++;
		// if (printCounter > 60)
		// {
		// 	printCounter = 0;
		// 
		// 	String s = "Segments: ";
		// 	for (int i = 0; i < _startSegmentCount + 2; i++)
		// 	{
		// 		s += _segments[i].ToString() + "; ";
		// 	}
		// 	 Debug.WriteLine(s);
		// }
	}
	private void ConstrainRope()
	{
		for (int i = 0; i < _startSegmentCount + 1; i++)
		{
			Vector2 segment = _segments[i + 1] - _segments[i];
			float segmentLength = segment.Length();
			float maxLength = _segmentLength;

			if (segmentLength > maxLength)
			{
				Vector2 direction = segment / segmentLength;
				if (i != 0)
					_segments[i] += (segmentLength - maxLength) * direction;
				
				_segments[i + 1] -= (segmentLength - maxLength) * direction;
			}
		}
	}
}
