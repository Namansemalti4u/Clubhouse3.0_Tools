using System;
using UnityEngine;

namespace Clubhouse.Games.Utilities
{
    #region Grid Helpers
    public static class Helper
    {
        public static void ForEachInGrid(Vector2Int a_dimensions, Action<int, int> a_action)
        {
            for (int y = 0; y < a_dimensions.x; y++)
            {
                for (int x = 0; x < a_dimensions.y; x++)
                {
                    a_action(x, y);
                }
            }
        }

        public static void ForEachInRow(Vector2Int a_dimensions, int a_row, Action<int, int> a_action)
        {
            for (int x = 0; x < a_dimensions.y; x++)
            {
                a_action(x, a_row);
            }
        }

        public static void ForEachInColumn(Vector2Int a_dimensions, int a_column, Action<int, int> a_action)
        {
            for (int y = 0; y < a_dimensions.x; y++)
            {
                a_action(a_column, y);
            }
        }
        #endregion

        #region Randomization
        public static void SwapRandomInRange<T>(ref T[] a_values, int a_from, int a_to)
        {
            System.Random random = new System.Random();
            if (a_values.Length == 1) return;

            for (int i = a_from; i < a_to; i++)
            {
                int firstIndex = i;
                int secondIndex = 0;
                do
                {
                    secondIndex = random.Next(a_values.Length);
                } while (secondIndex == firstIndex);

                T temp = a_values[firstIndex];
                a_values[firstIndex] = a_values[secondIndex];
                a_values[secondIndex] = temp;
            }
        }

        public static void SwapIndexInRange<T>(ref T[] a_values, int index, int a_from, int a_to, bool mustSwap = false)
        {
            System.Random random = new System.Random();
            if (a_values.Length == 1) return;

            int firstIndex = index;
            int secondIndex = 0;
            if (mustSwap)
            {
                do
                {
                    secondIndex = random.Next(a_from, a_to);
                } while (secondIndex == firstIndex);
            }
            else
            {
                secondIndex = random.Next(a_from, a_to);
                if (secondIndex == firstIndex) return;
            }

            T temp = a_values[firstIndex];
            a_values[firstIndex] = a_values[secondIndex];
            a_values[secondIndex] = temp;
        }
        #endregion

        public static T GetClickedGameObject2D<T>() where T : MonoBehaviour
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    if (hit)
                    {
                        if(hit.collider.TryGetComponent(out T component))
                        {
                            return component;
                        }
                    }
                }
            }
            return null;
        }

        public static T GetClickedGameObject<T>() where T : MonoBehaviour
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);

                    if (Physics.Raycast(ray, out RaycastHit hit))
                    {
                        if(hit.collider.TryGetComponent(out T component))
                        {
                            return component;
                        }
                    }
                }
            }
            return null;
        }
    }
}
