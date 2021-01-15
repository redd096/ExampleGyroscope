namespace redd096
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class Utility
    {
        #region general

        /// <summary>
        /// Set lockState, and visible only when not locked
        /// </summary>
        public static void LockMouse(CursorLockMode lockMode)
        {
            Cursor.lockState = lockMode;
            Cursor.visible = lockMode != CursorLockMode.Locked;
        }

        /// <summary>
        /// Remap a value min and max
        /// </summary>
        /// <param name="value">Value to remap</param>
        /// <param name="from_prev">Previous minimum value</param>
        /// <param name="to_prev">Previous maximum value</param>
        /// <param name="from_new">New minimum value</param>
        /// <param name="to_new">New max value</param>
        /// <returns></returns>
        public static float Remap(this float value, float from_prev, float to_prev, float from_new, float to_new)
        {
            return (value - from_prev) / (to_prev - from_prev) * (to_new - from_new) + from_new;
        }

        /// <summary>
        /// Get touch or mouse position
        /// </summary>
        public static Vector3 InputPosition()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.GetTouch(0).position;
#else
            return Input.mousePosition;
#endif
        }

        #endregion

        #region find nearest

        /// <summary>
        /// Find nearest to position
        /// </summary>
        public static T FindNearest<T>(this T[] array, Vector3 position) where T : Component
        {
            T nearest = default;
            float distance = Mathf.Infinity;

            //foreach element in the array
            foreach (T element in array)
            {
                //only if there is element
                if (element == null)
                    continue;

                //check distance to find nearest
                float newDistance = Vector3.Distance(element.transform.position, position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearest = element;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Find nearest to position
        /// </summary>
        public static GameObject FindNearest(this GameObject[] array, Vector3 position)
        {
            GameObject nearest = default;
            float distance = Mathf.Infinity;

            //foreach element in the array
            foreach (GameObject element in array)
            {
                //only if there is element
                if (element == null)
                    continue;

                //check distance to find nearest
                float newDistance = Vector3.Distance(element.transform.position, position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearest = element;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Find nearest to position
        /// </summary>
        public static T FindNearest<T>(this List<T> list, Vector3 position) where T : Component
        {
            T nearest = default;
            float distance = Mathf.Infinity;

            //foreach element in the list
            foreach (T element in list)
            {
                //only if there is element
                if (element == null)
                    continue;

                //check distance to find nearest
                float newDistance = Vector3.Distance(element.transform.position, position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearest = element;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Find nearest to position
        /// </summary>
        public static GameObject FindNearest(this List<GameObject> list, Vector3 position)
        {
            GameObject nearest = default;
            float distance = Mathf.Infinity;

            //foreach element in the list
            foreach (GameObject element in list)
            {
                //only if there is element
                if (element == null)
                    continue;

                //check distance to find nearest
                float newDistance = Vector3.Distance(element.transform.position, position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearest = element;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Find nearest to position
        /// </summary>
        public static T FindNearest<K, T>(this Dictionary<K, T> dictionary, Vector3 position, out K key) where T : Component
        {
            K nearestKey = default;
            float distance = Mathf.Infinity;

            //foreach element in the dictionary
            foreach (K elementKey in dictionary.Keys)
            {
                //only if there is element
                if (dictionary[elementKey] == null)
                    continue;

                //check distance to find nearest
                float newDistance = Vector3.Distance(dictionary[elementKey].transform.position, position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearestKey = elementKey;
                }
            }

            key = nearestKey;
            return dictionary[nearestKey];
        }

        /// <summary>
        /// Find nearest to position
        /// </summary>
        public static GameObject FindNearest<K>(this Dictionary<K, GameObject> dictionary, Vector3 position, out K key)
        {
            K nearestKey = default;
            float distance = Mathf.Infinity;

            //foreach element in the dictionary
            foreach (K elementKey in dictionary.Keys)
            {
                //only if there is element
                if (dictionary[elementKey] == null)
                    continue;

                //check distance to find nearest
                float newDistance = Vector3.Distance(dictionary[elementKey].transform.position, position);
                if (newDistance < distance)
                {
                    distance = newDistance;
                    nearestKey = elementKey;
                }
            }

            key = nearestKey;
            return dictionary[nearestKey];
        }

        #endregion
    }

    public static class Collections
    {
        #region create copy

        /// <summary>
        /// Create a copy of the array
        /// </summary>
        public static T[] CreateCopy<T>(this T[] array)
        {
            T[] newArray = new T[array.Length];

            //add every element in new array
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }

            return newArray;
        }

        /// <summary>
        /// Create a copy of the list
        /// </summary>
        public static List<T> CreateCopy<T>(this List<T> list)
        {
            List<T> newList = new List<T>();

            //add every element in new list
            foreach (T element in list)
            {
                newList.Add(element);
            }

            return newList;
        }

        /// <summary>
        /// Create a copy of the dictionary (N.B. a copy of dictionary, not elements neither keys)
        /// </summary>
        public static Dictionary<T, J> CreateCopy<T, J>(this Dictionary<T, J> dictionary)
        {
            Dictionary<T, J> newDictionary = new Dictionary<T, J>();

            //add every element in new dictionary
            foreach (T key in dictionary.Keys)
            {
                newDictionary.Add(key, dictionary[key]);
            }

            return newDictionary;
        }

        #endregion

        #region set parent

        /// <summary>
        /// Set parent for every element in the array
        /// </summary>
        public static void SetParent<T>(this T[] array, Transform parent, bool worldPositionStays = true) where T : Component
        {
            foreach (T c in array)
            {
                c.transform.SetParent(parent, worldPositionStays);
            }
        }

        /// <summary>
        /// Set parent for every element in the array
        /// </summary>
        public static void SetParent(this GameObject[] array, Transform parent, bool worldPositionStays = true)
        {
            foreach (GameObject c in array)
            {
                c.transform.SetParent(parent, worldPositionStays);
            }
        }

        /// <summary>
        /// Set parent for every element in the list
        /// </summary>
        public static void SetParent<T>(this List<T> list, Transform parent, bool worldPositionStays = true) where T : Component
        {
            foreach (T c in list)
            {
                c.transform.SetParent(parent, worldPositionStays);
            }
        }

        /// <summary>
        /// Set parent for every element in the list
        /// </summary>
        public static void SetParent(this List<GameObject> list, Transform parent, bool worldPositionStays = true)
        {
            foreach (GameObject c in list)
            {
                c.transform.SetParent(parent, worldPositionStays);
            }
        }

        /// <summary>
        /// Set parent for every element in the dictionary
        /// </summary>
        public static void SetParent<T, J>(this Dictionary<T, J> dictionary, Transform parent, bool worldPositionStays = true) where J : Component
        {
            foreach (T key in dictionary.Keys)
            {
                dictionary[key].transform.SetParent(parent, worldPositionStays);
            }
        }

        /// <summary>
        /// Set parent for every element in the dictionary
        /// </summary>
        public static void SetParent<T>(this Dictionary<T, GameObject> dictionary, Transform parent, bool worldPositionStays = true)
        {
            foreach (T key in dictionary.Keys)
            {
                dictionary[key].transform.SetParent(parent, worldPositionStays);
            }
        }

        #endregion
    }
}