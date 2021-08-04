using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PowerAutomate.CustomCode.Helpers
{
	public interface IScriptContext
	{
		// Correlation Id
		string CorrelationId { get; }

		// Connector Operation Id
		string OperationId { get; }

		// Incoming request
		HttpRequestMessage Request { get; }

		// Logger instance
		ILogger Logger { get; }

		// Used to send an HTTP request
		// Use this method to send requests instead of HttpClient.SendAsync
		Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken);
	}
}
