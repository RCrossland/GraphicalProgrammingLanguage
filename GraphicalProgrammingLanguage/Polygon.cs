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

		public override void Set(Color colour, params int[] list)
		{
			base.Set(colour, list[0], list[1]);
			integerPoints = list.Skip(2).ToArray();
		}


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
