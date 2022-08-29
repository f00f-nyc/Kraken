using Godot;

namespace kraken.src.kraken.segments
{
    public enum Direction
    {
        PositiveDownToZero,
        ZeroDownToNegative,
        NegativeUpToZero,
        ZeroUpToPositive,
    }

    public class SegmentPosition
    {
        /// <summary>
        /// True, if the rotation is increasing in Z
        /// </summary>
        public Direction Direction { get; set; } = Direction.PositiveDownToZero;

        /// <summary>
        /// Maximum and minumum Z rotation
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Maximum Y transform
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The Y translation of the Node at the start
        /// </summary>
        public float StartY { get; set; }

        /// <summary>
        /// How long the animation lasts
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// The tween node which will be responsible for animating
        /// </summary>
        public Tween TweenNode { get; set; }

        /// <summary>
        /// The actual node
        /// </summary>
        public Spatial Node { get; set; }

        /// <summary>
        /// The given animation is broken up into four parts
        /// </summary>
        private struct Animation
        {
            public float StartRotationZ;
            public float EndRotationZ;
            public float StartTranslateY;
            public float EndTranslateY;
            public Direction NextDirection;
            public Tween.EaseType EaseType;
        }

        private Animation GetNextAnimation()
        {
            switch (this.Direction)
            {
                case Direction.PositiveDownToZero:
                    return new Animation
                    {
                        StartRotationZ = this.Z,
                        EndRotationZ = 0,
                        StartTranslateY = this.StartY + this.Y,
                        EndTranslateY = this.StartY,
                        NextDirection = Direction.ZeroDownToNegative,
                        EaseType = Tween.EaseType.In,
                    };
                case Direction.ZeroDownToNegative:
                    return new Animation
                    {
                        StartRotationZ = 0,
                        EndRotationZ = -this.Z,
                        StartTranslateY = this.StartY,
                        EndTranslateY = this.StartY - this.Y,
                        NextDirection = Direction.NegativeUpToZero,
                        EaseType = Tween.EaseType.Out,
                    };
                case Direction.NegativeUpToZero:
                    return new Animation
                    {
                        StartRotationZ = -this.Z,
                        EndRotationZ = 0,
                        StartTranslateY = this.StartY - this.Y,
                        EndTranslateY = this.StartY,
                        NextDirection = Direction.ZeroUpToPositive,
                        EaseType = Tween.EaseType.In,
                    };
                case Direction.ZeroUpToPositive:
                    return new Animation
                    {
                        StartRotationZ = 0,
                        EndRotationZ = this.Z,
                        StartTranslateY = this.StartY,
                        EndTranslateY = this.StartY + this.Y,
                        NextDirection = Direction.PositiveDownToZero,
                        EaseType = Tween.EaseType.Out,
                    };
                default:
                    return new Animation();
            }
        }

        /// <summary>
        /// Creates the animation Animates rotation along Z axis, no effect if animation is on-going
        /// </summary>
        public void ApplyAnimation()
        {
            if ((this.TweenNode != null) && !this.TweenNode.IsActive())
            {
                var animation = GetNextAnimation();
                this.Direction = animation.NextDirection;
                var duration = this.Duration / 2;

                this.TweenNode.InterpolateProperty(
                    this.Node,
                    "rotation_degrees:x",
                    animation.StartRotationZ,
                    animation.EndRotationZ,
                    duration,
                    Tween.TransitionType.Sine,
                    animation.EaseType
                );
                
                this.TweenNode.InterpolateProperty(
                    this.Node,
                    "translation:y",
                    animation.StartTranslateY,
                    animation.EndTranslateY,
                    duration,
                    Tween.TransitionType.Sine,
                    animation.EaseType
                    );
                    
                this.TweenNode.Start();

                Debug.Print<SegmentPosition>($"Starting tween for Y translation y={animation.StartTranslateY} to y={animation.EndTranslateY} for {duration} on node {this.Node.Name} using Tween Node {this.TweenNode.Name}");
            }
        }
    }
}
