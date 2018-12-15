using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicalProgrammingLanguage
{
	class ShapeFactory
	{
		public Shape GetShape(string shapeType)
		{
			shapeType = shapeType.ToUpper().Trim();

			if(shapeType == "RECTANGLE")
			{
				return new Rectangle();
			}
			else if(shapeType == "SQUARE")
			{
				return new Square();
			}
			else if(shapeType == "CIRCLE")
			{
				return new Circle();
			}
			else
			{
				return new Rectangle();
			}
		}
	}
}
