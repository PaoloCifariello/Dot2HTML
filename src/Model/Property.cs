using System;

namespace PA_Final.Model
{
	public class Property
	{
		private string PropertyName;
		private PropertyType Type;

		public Property ()
		{
		}
	}

    public enum PropertyType
    {
        Boolean,
        String,
        Integer,
        Real,
        Enumeration
    }
}

