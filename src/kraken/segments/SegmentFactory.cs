using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace kraken.src.kraken.segments
{
    public class SegmentFactory
    {
        public SegmentFactory(Spatial parent)
            : this(parent, FindMaxNodes(parent))
        { }

        public SegmentFactory(Spatial parent, int maxNodes)
        {
            this.parent = parent;
            this.MaxNodes = maxNodes;
        }

        private Spatial parent;
        private int MaxNodes;
        private SegmentPosition startPosition = new SegmentPosition();
        private SegmentPosition endPosition = new SegmentPosition();

        public SegmentFactory WithStart(SegmentPosition pos)
        {
            this.startPosition = pos;
            return this;
        }

        public SegmentFactory WithEnd(SegmentPosition pos)
        {
            this.endPosition = pos;
            return this;
        }

        public IEnumerable<SegmentPosition> GetAllSegments()
        {
            var startNode = GetNthSpatial(0);
            yield return new SegmentPosition
            {
                Node = startNode,
                Z = this.startPosition.Z,
                TweenNode = GetNthTween(0),
                Duration = this.startPosition.Duration,
                Y = this.startPosition.Y,
                StartY = startNode.Translation.y,
            };

            for (var i = 1; i < this.MaxNodes - 1; i++)
            {
                var pct = i / (float)this.MaxNodes;
                var current = GetNthSpatial(i);

                yield return new SegmentPosition
                {
                    Node = current,
                    Z = Helper.Interpolate(this.startPosition.Z, this.endPosition.Z, pct),
                    TweenNode = GetNthTween(i),
                    Duration = Helper.Interpolate(this.startPosition.Duration, this.endPosition.Duration, pct),
                    Y = Helper.Interpolate(this.startPosition.Y, this.endPosition.Y, pct),
                    StartY = current.Translation.y,
                };
            }

            var endNode = GetNthSpatial(MaxNodes - 1);
            yield return new SegmentPosition
            {
                Node = endNode,
                Z = this.endPosition.Z,
                TweenNode = GetNthTween(MaxNodes - 1),
                Duration = this.endPosition.Duration,
                Y = this.endPosition.Y,
                StartY = endNode.Translation.y,
            };
        }

        private Spatial GetNthSpatial(int index) => DepthFirstSearch<Spatial>(this.parent, $"segment_{index + 1:00}");

        private Tween GetNthTween(int index) => DepthFirstSearch<Tween>(this.parent, $"tween_{index + 1:00}");

        private static T DepthFirstSearch<T>(Node start, string name)
            where T : Node
        {
            if (start.Name.Equals(name, StringComparison.Ordinal))
            {
                return start as T;
            }

            foreach (Node child in start.GetChildren())
            {
                var found = DepthFirstSearch<T>(child, name);
                if (found != null)
                {
                    return found;
                }
            }

            return null;
        }

        private static int FindMaxNodes(Node current)
        {
            var i = 1;

            while (true)
            {
                var next = current.GetChildren().Cast<Node>().FirstOrDefault(n => n.Name.Equals($"segment_{i:00}"));
                if (next == null)
                {
                    return i-1;
                }

                i++;
                current = next;
            }
        }
    }
}
