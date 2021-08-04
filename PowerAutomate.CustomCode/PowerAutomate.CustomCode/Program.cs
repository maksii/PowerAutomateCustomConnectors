using System;
using PowerAutomate.CustomCode.Helpers;

namespace PowerAutomate.CustomCode
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var script = new Script();
			var scriptResult = script.ExecuteAsync();
		}
	}
}
