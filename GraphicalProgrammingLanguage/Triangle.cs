using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class Triangle:Shape
	{
		int point1a, point1b, point2a, point2b, point3a, point3b;

		public Triangle()
		{
		}

		public override void Set(Color colour, params int[] list)
		{
			base.Set(colour, list[0], list[1]);
			this.point1a = list[2];
			this.point1b = list[3];
			this.point2a = list[4];
			this.point2b = list[5];
			this.point3a = list[6];
			this.point3b = list[7];
		}

		public override void Draw(Graphics g)
		{
			Point[] points = { new Point(point1a, point1b), new Point(point2a, point2b), new Point(point3a, point3b) };
			g.DrawPolygon(new Pen(colour), points);
		}
	}
}
