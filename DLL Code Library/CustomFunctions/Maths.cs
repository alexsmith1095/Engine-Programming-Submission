using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomFunctions
{
	public class Maths : MonoBehaviour
    {

        public const float Pi = 3.14159274f;

		/// <summary>
		/// Returns the absolute value of value
		/// </summary>
        public static float Absolute(float value)
		{
            if (value < 0)
				value *= -1;
			return value;
		}

		/// <summary>
		/// Returns the absolute value of value
		/// </summary>
        public static int Absolute(int value)
		{
            if (value < 0)
				value *= -1;
			return value;
		}

		/// <summary>
		/// Return the angle between from and to in degrees
		/// </summary>
		public static float AngleInDegrees(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
        }

		/// <summary>
		/// Clamps value between min and max
		/// </summary>
		public static float Clamp(float value, float min, float max)
		{
			if (value <= min)
				value = min;
			else if (value >= max)
				value = max;
			return value;
        }

		/// <summary>
		/// Clamps value between min and max
		/// </summary>
		public static int Clamp(int value, int min, int max)
		{
			if (value <= min)
				value = min;
			else if (value >= max)
				value = max;
			return value;
        }

		/// <summary>
		/// Returns the greatest value
		/// </summary>
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		/// <summary>
		/// Returns the greatest value
		/// </summary>
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		/// <summary>
		/// Returns the lesser value
		/// </summary>
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		/// <summary>
		/// Returns the lesser value
		/// </summary>
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
        }

		/// <summary>
		/// Returns value squared
		/// </summary>
		public static float Squared(float value)
        {
			value *= value;
			return value;
		}

		/// <summary>
		/// Returns value squared
		/// </summary>
		public static int Squared(int value)
        {
			value *= value;
			return value;
        }

		/// <summary>
		/// Moves current towards target no faster than maxDelta
		/// </summary>
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			float result;
			if (Maths.Absolute(target - current) <= maxDelta) {
				result = target;
			}
			else {
				result = current + Mathf.Sign(target - current) * maxDelta;
			}
			return result;
		}

		/// <summary>
		/// Moves current towards target no faster than maxDelta
		/// </summary>
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float num = Maths.DeltaAngle(current, target);
			float result;
			if (-maxDelta < num && num < maxDelta) {
				result = target;
			}
			else {
				target = current + num;
				result = Maths.MoveTowards(current, target, maxDelta);
			}
			return result;
		}

		/// <summary>
		/// Returns shortest difference between current and target in degrees
		/// </summary>
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			if (num > 180f) {
				num -= 360f;
			}
			return num;
        }

		/// <summary>
		/// Returns the square magnitude of Vector3 a
		/// </summary>
		public static float SqrMag(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}
	}
}
