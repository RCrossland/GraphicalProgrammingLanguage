using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class Circle:Shape
	{
		int radius;

		public Circle() : base()
		{

		}

		/// <summary>
		/// Sets the colour and diameter values of the circle
		/// </summary>
		/// <param name="colour">Color: The colour of the circle.</param>
		/// <param name="list">A list of integer values for the currentx and currenty of the shape as well the diameter.</param>
		public override void Set(Color colour, params int[] list)
		{
			try
			{
				base.Set(colour, list[0], list[1]);
				radius = list[2];
			}
			catch (IndexOutOfRangeException)
			{
				throw;
			}
		}

		/// <summary>
		/// Draws a circle based on the diameter.
		/// </summary>
		/// <param name="g">The graphics panel of where to draw the circle.</param>
		public override void Draw(Graphics g)
		{
			Pen pen = new Pen(colour, 2);
			g.DrawEllipse(pen, x, y, radius * 2, radius * 2);
		}
	}
}
