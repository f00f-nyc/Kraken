using Godot;

namespace kraken.src
{
    public class Helper
    {
        public static int Interpolate(int start, int end, float percent)
        {
            if (start > end)
            {
                return (int)((start - end) * (1 - percent) + end);
            }

            return (int)((end - start) * percent + start);
        }

        public static float Interpolate(float start, float end, float percent)
        {
            if (start > end)
            {
                return (start - end) * (1 - percent) + end;
            }

            return (end - start) * percent + start;
        }

        public static byte InterpolateByte(float start, float end, float percent)
        {
            return (byte)Interpolate(start * 255, end * 255, percent);
        }

        public static Color Interpolate(Color start, Color end, float percent)
        {
            return Color.Color8(
                r8: InterpolateByte(start.r, end.r, percent),
                g8: InterpolateByte(start.g, end.g, percent),
                b8: InterpolateByte(start.b, end.b, percent),
                a8: InterpolateByte(start.a, end.a, percent)
            );
        }

        public static Vector2 Interpolate(Vector2 start, Vector2 end, float percent)
        {
            return new Vector2(x: Interpolate(start.x, end.x, percent), y: Interpolate(start.y, end.y, percent));
        }
    }
}
