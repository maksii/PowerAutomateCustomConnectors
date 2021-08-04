using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PowerAutomate.CustomCode.Helpers;

namespace PowerAutomate.CustomCode
{
	public class Script : ScriptBase
	{
		public string ActionName = "Action";
		public IScriptContext _context;
		public Script()
		{
			this.Context = _context;
		}

		public override async Task<HttpResponseMessage> ExecuteAsync()
		{
			// Check if the operation ID matches what is specified in the OpenAPI definition of the connector
			if (this.Context.OperationId == "Action")
			{
				return await this.HandleRegexIsMatchOperation().ConfigureAwait(false);
			}

			if (this.Context.OperationId == "ForwardAsPostRequest")
			{
				return await this.HandleForwardOperation().ConfigureAwait(false);
			}

			// Check if the operation ID matches what is specified in the OpenAPI definition of the connector
			if (this.Context.OperationId == "ForwardAndTransformRequest")
			{
				return await this.HandleForwardAndTransformOperation().ConfigureAwait(false);
			}

			// Handle an invalid operation ID
			HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest)
			{
				Content = CreateJsonContent($"Unknown operation ID '{this.Context.OperationId}'")
			};
			return response;
		}

		private async Task<HttpResponseMessage> HandleRegexIsMatchOperation()
		{
			HttpResponseMessage response;

			// We assume the body of the incoming request looks like this:
			// {
			//   "textToCheck": "<some text>",
			//   "regex": "<some regex pattern>"
			// }
			var contentAsString = await this.Context.Request.Content.ReadAsStringAsync().ConfigureAwait(false);

			// Parse as JSON object
			var contentAsJson = JObject.Parse(contentAsString);

			// Get the value of text to check
			var textToCheck = (string)contentAsJson["textToCheck"];

			JObject output = new JObject
			{
				["textToCheck"] = textToCheck
			};

			response = new HttpResponseMessage(HttpStatusCode.OK) {Content = CreateJsonContent(output.ToString())};
			return response;
		}

		private async Task<HttpResponseMessage> HandleForwardOperation()
		{
			// Example case: If your OpenAPI definition defines the operation as 'GET', but the backend API expects a 'POST',
			// use this script to change the HTTP method.
			this.Context.Request.Method = HttpMethod.Post;

			// Use the context to forward/send an HTTP request
			HttpResponseMessage response = await this.Context.SendAsync(this.Context.Request, this.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);
			return response;
		}

		private async Task<HttpResponseMessage> HandleForwardAndTransformOperation()
		{
			// Use the context to forward/send an HTTP request
			HttpResponseMessage response = await this.Context.SendAsync(this.Context.Request, this.CancellationToken).ConfigureAwait(continueOnCapturedContext: false);

			// Do the transformation if the response was successful, otherwise return error responses as-is
			if (response.IsSuccessStatusCode)
			{
				var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);

				// Example case: response string is some JSON object
				var result = JObject.Parse(responseString);

				// Wrap the original JSON object into a new JSON object with just one key ('wrapped')
				var newResult = new JObject
				{
					["wrapped"] = result,
				};

				response.Content = CreateJsonContent(newResult.ToString());
			}

			return response;
		}
	}
}
