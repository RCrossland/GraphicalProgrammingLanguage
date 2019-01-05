using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class Line:Shape
	{
		int newX, newY;

		/// <summary>
		/// Creates a new line shape with x and y values being 0.
		/// </summary>
		public Line() : base()
		{
			newX = 0;
			newY = 0;
		}

		/// <summary>
		/// Creates a new line with the specified colour and x,y values.
		/// </summary>
		/// <param name="colour">Color: The colour to set the line.</param>
		/// <param name="currentX">Integer: The value of the current X.</param>
		/// <param name="currentY">Integer: The value of the current Y.</param>
		/// <param name="x">Integer: The value for the new X.</param>
		/// <param name="y">Integer: The value for the new Y.</param>
		public Line(Color colour, int currentX, int currentY, int x, int y) : base(colour, currentX, currentY) {
			this.newX = x;
			this.newY = y;
		}

		/// <summary>
		/// Sets the colour and x,y values of the line.
		/// </summary>
		/// <param name="colour">Color: The colour to set the line.</param>
		/// <param name="list">A list of integers for the currentX, currentY, newX and newY values.</param>
		public override void Set(Color colour, params int[] list)
		{
			base.Set(colour, list[0], list[1]);
			newX = list[2];
			newY = list[3];
		}

		/// <summary>
		/// Draws a line between two points.
		/// </summary>
		/// <param name="g">The graphics panel of where to draw the line.</param>
		public override void Draw(Graphics g)
		{
			Pen pen = new Pen(colour, 2);

			g.DrawLine(pen, new Point(x, y), new Point(newX, newY));
		}
	}
}
