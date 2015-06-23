﻿using System;

namespace PA_Final.Model
{
	public class DotAttribute
	{
		private String key;
		private String value;

		public String Key {
			get {
				return key;
			}
		}

		public String Value {
			get {
				return value;
			}
		}
		
		public DotAttribute (String key, String value)
		{
			this.key = key;
			this.value = value;
		}
	}
}

