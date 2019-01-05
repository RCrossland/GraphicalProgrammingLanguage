using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class Rectangle:Shape
	{
		int width, height;
		
		public Rectangle() : base()
		{
			width = 100;
			height = 100;
		}

		public Rectangle(Color colour, int x, int y, int width, int height):base(colour, x, y)
		{
			this.width = width;
			this.height = height;
		}

		/// <summary>
		/// Sets the colour, currentX, currentY, width and height values of the Rectangle shape.
		/// </summary>
		/// <param name="colour">Color: The colour to draw the rectangle.</param>
		/// <param name="list">A list of integers for the currentX, currentY, width and height values for the rectangle.</param>
		public override void Set(Color colour, params int[] list)
		{
			base.Set(colour, list[0], list[1]);
			this.width = list[2];
			this.height = list[3];
		}

		/// <summary>
		/// Draws a rectangle based on the width and height.
		/// </summary>
		/// <param name="g">The graphics panel of where to draw the rectangle.</param>
		public override void Draw(Graphics g)
		{
			Pen p = new Pen(colour, 2);
			g.DrawRectangle(p, x, y, height, width);
		}
	}
}
