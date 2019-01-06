using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	public abstract class Shape:Shapes
	{
		protected Color colour;
		protected int x, y;

		public Shape()
		{
			colour = Color.Red;
			x = y = 100;
		}

		public Shape(Color colour, int x, int y)
		{
			this.colour = colour;
			this.x = x;
			this.y = y;
		}

		// This function must be overriden by any derived classes
		public abstract void Draw(Graphics g);

		// Set to virtual so it can be overriden by derived classes
		// However the child version can call this function to do more generic stuff
		public virtual void Set(Color colour, params int[] list)
		{
			this.colour = colour;
			this.x = list[0];
			this.y = list[1];
		}

		public virtual void SetTexture(string textureFile)
		{
		}
	}
}
