using System.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class Script : ScriptBase
{
	public override async Task<HttpResponseMessage> ExecuteAsync()
	{
		return await this.HandleHashOperation().ConfigureAwait(false);
	}

	private async Task<HttpResponseMessage> HandleHashOperation()
	{
		HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
		var queryString = ParseQueryString(this.Context.Request.RequestUri);
		var valuetoHash = queryString.First(x => x.Key == "string").Value;

		var method = queryString.First(x => x.Key == "type").Value;

		var toHash = Hash(valuetoHash, method);

		var responseContent = new JObject
		{
			["hash"] = toHash,
			["queryOptions"] = valuetoHash
		};


		response.Content = CreateJsonContent(responseContent.ToString());
		return response;
	}

	private static readonly Regex _regex = new Regex(@"[?&](\w[\w.]*)=([^?&]+)");

	public static IReadOnlyDictionary<string, string> ParseQueryString(Uri uri)
	{
		var match = _regex.Match(uri.PathAndQuery);
		var paramaters = new Dictionary<string, string>();
		while (match.Success)
		{
			paramaters.Add(match.Groups[1].Value, match.Groups[2].Value);
			match = match.NextMatch();
		}

		return paramaters;
	}

	static string Hash(string input, string method)
	{
		switch (method)
		{
			case "sha1":
				return CreatSHA1(input);
				break;
			case "md5":
				return CreateMD5(input);
				break;
			default:
				return CreatSHA1(input);
		}
	}

	public static string CreateMD5(string input)
	{
		// Use input string to calculate MD5 hash
		using (MD5 md5 = MD5.Create())
		{
			byte[] hashBytes = md5.ComputeHash(Encoding.Unicode.GetBytes(input));

			// Convert the byte array to hexadecimal string
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < hashBytes.Length; i++)
			{
				sb.Append(hashBytes[i].ToString("X2"));
			}

			return sb.ToString();
		}
	}

	public static string CreatSHA1(string input)
	{
		using (SHA1Managed sha1 = new SHA1Managed())
		{
			var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sb = new StringBuilder(hash.Length * 2);

			foreach (byte b in hash)
			{
				sb.Append(b.ToString("X2"));
			}

			return sb.ToString();
		}
	}
}