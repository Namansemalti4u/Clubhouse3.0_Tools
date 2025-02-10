using UnityEngine;

namespace Clubhouse.Tools
{
    public static partial class Feel
    {
        #region Helper
        private static void SetColorInGradients(Gradient a_gradient, Color a_color)
        {
            a_gradient.colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(a_color, 0f)
            };
        }
        #endregion
    }
}
