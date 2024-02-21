using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamtkSchedule.Application.Exceptions
{

	[Serializable]
	public class ColumnIndexNotFoundException : ApplicationException
	{
		public ColumnIndexNotFoundException() { }
		public ColumnIndexNotFoundException(string message) : base(message) { }
		public ColumnIndexNotFoundException(string message, Exception inner) : base(message, inner) { }
		protected ColumnIndexNotFoundException(
		  System.Runtime.Serialization.SerializationInfo info,
		  System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
	}
}
