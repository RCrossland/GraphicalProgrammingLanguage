using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class Polygon : Shape
	{
		int[] integerPoints;
		List<Point> points = new List<Point>();

		public Polygon()
		{
		}

		/// <summary>
		/// Sets the colour and x,y values of the Polygon.
		/// </summary>
		/// <param name="colour">Color: Sets the colour to draw the polygon.</param>
		/// <param name="list">A list of integers for the currentX, currentY, x and y values for the polygon.</param>
		public override void Set(Color colour, params int[] list)
		{
			base.Set(colour, list[0], list[1]);
			integerPoints = list.Skip(2).ToArray();
		}

		/// <summary>
		/// Draws a polygon for a number of points.
		/// </summary>
		/// <param name="g">The graphics panel of where to draw the polygon.</param>
		public override void Draw(Graphics g)
		{
			for (int i = 0; i < integerPoints.Length; i += 2)
			{
				points.Add(new Point(integerPoints[i], integerPoints[i + 1]));
			}

			g.DrawPolygon(new Pen(colour), points.ToArray());
		}
	}
}
