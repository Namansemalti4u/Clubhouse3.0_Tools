using UnityEngine;

namespace Clubhouse.Helper
{
    public enum AnchorType
    {
        TopLeft,
        TopMiddle,
        TopRight,
        MiddleLeft,
        Center,
        MiddleRight,
        BottomLeft,
        BottomMiddle,
        BottomRight,
    }
    public static class RectHelper
    {
        public static Vector2 GetAnchorPosition(AnchorType a_type)
        {
            return a_type switch
            {
                AnchorType.TopLeft => new Vector2(0, 1),
                AnchorType.TopMiddle => new Vector2(0.5f, 1),
                AnchorType.TopRight => new Vector2(1, 1),
                AnchorType.MiddleLeft => new Vector2(0, 0.5f),
                AnchorType.Center => new Vector2(0.5f, 0.5f),
                AnchorType.MiddleRight => new Vector2(1, 0.5f),
                AnchorType.BottomLeft => new Vector2(0, 0),
                AnchorType.BottomMiddle => new Vector2(0.5f, 0),
                AnchorType.BottomRight => new Vector2(1, 0),
                _ => new Vector2(0.5f, 0.5f), // Default to Center
            };
        }

        public static void SetRectPosition(RectTransform a_transform, Vector2 a_position, AnchorType a_anchorType = AnchorType.Center)
        {
            // Set anchor preset
            a_transform.anchorMin = GetAnchorPosition(a_anchorType);
            a_transform.anchorMax = GetAnchorPosition(a_anchorType);

            // Change X and Y position
            a_transform.anchoredPosition = a_position;
        }
    }
}
