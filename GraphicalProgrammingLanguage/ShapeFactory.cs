using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class ShapeFactory
	{
		/// <summary>
		/// Shape factory to return the requested shape.
		/// </summary>
		/// <param name="shapeType">String: The shape to be returned to the user.</param>
		/// <returns>Shape: A refernece to the requested shape.</returns>
		public Shape GetShape(string shapeType)
		{
			shapeType = shapeType.ToUpper().Trim();

			if(shapeType == "RECTANGLE")
			{
				return new Rectangle();
			}
			else if(shapeType == "SQUARE")
			{
				return new Rectangle();
			}
			else if(shapeType == "CIRCLE")
			{
				return new Circle();
			}
			else if(shapeType == "TRIANGLE")
			{
				return new Triangle();
			}
			else if(shapeType == "POLYGON")
			{
				return new Polygon();
			}
			else if(shapeType == "MOVETO")
			{
				return new Line();
			}
			else
			{
				return new Rectangle();
			}
		}
	}
}
