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

		public Line() : base()
		{
			newX = 0;
			newY = 0;
		}

		public Line(Color colour, int currentX, int currentY, int x, int y) : base(colour, currentX, currentY) {
			this.newX = x;
			this.newY = y;
		}

		public override void Set(Color colour, params int[] list)
		{
			base.Set(colour, list[0], list[1]);
			newX = list[2];
			newY = list[3];
		}

		public override void Draw(Graphics g)
		{
			Pen pen = new Pen(colour, 2);

			g.DrawLine(pen, new Point(x, y), new Point(newX, newY));
		}
	}
}
