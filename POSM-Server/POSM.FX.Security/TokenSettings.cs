﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSM.FX.Security
{
	public class TokenSettings
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string Key { get; set; }
	}
}
