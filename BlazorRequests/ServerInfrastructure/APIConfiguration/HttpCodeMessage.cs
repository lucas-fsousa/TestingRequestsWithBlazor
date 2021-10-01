using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Infrastructure.APIConfiguration {
  public static class HttpCodeMessage {
    public static string Code200 { get; } = "Request completed successfully.";
    public static string Code201 { get; } = "The item was created successfully.";
    public static string Code204 { get; } = "Item not found. Possible reasons: Does not exist, is blocked or is in use.";
    public static string Code404 { get; } = "No results for the request.";
    public static string Code400 { get; } = "The request to the server was not terminated properly. Try again.";
    public static string Code401 { get; } = "Not authorized. Authenticate and try again.";
    public static string Code500 { get; } = "The request was not completed due to an internal error on the server side.";
  }
}
