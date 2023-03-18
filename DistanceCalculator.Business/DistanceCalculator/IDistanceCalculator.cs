using System;
using DistanceCalculator.Business.DistanceCalculator.Models;

namespace DistanceCalculator.Business.DistanceCalculator
{
	/// <summary>
	/// Distance calculator.
	/// </summary>
	public interface IDistanceCalculator
	{
		/// <summary>
		/// Return a distance between two points.
		/// </summary>
		/// <param name="startPoint">Start point.</param>
		/// <param name="endPoint">End point.</param>
		/// <returns>Distance.</returns>
		double GetDistance(Point startPoint, Point endPoint);
	}

	/// <inheritdoc/>
	public class DistanceCalculator : IDistanceCalculator
	{
		/// <inheritdoc/>
		/// <remarks>
		/// I implemented a distance calculation for points on Cartesian coordinate system.
		/// Of course it isn't correct for coordinates.
		/// But I hope the exact distance it is not a point of this challenge.
		/// </remarks>
		public double GetDistance(Point startPoint, Point endPoint)
		{
			return Math.Sqrt(Math.Pow(startPoint.X - endPoint.X, 2) +
			                 Math.Pow(startPoint.Y - endPoint.Y, 2));
		}
	}
}