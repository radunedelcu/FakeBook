using System.Net;
using System.Text.Json;

namespace FakeBook.Api.Middleware {
  public class ErrorDetails {
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public override string ToString() { return JsonSerializer.Serialize(this); }
  }

  public class ExceptionMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next) { _next = next; }

    public async Task Invoke(HttpContext context) {
      try {
        await _next(context);
      } catch (Exception ex) {
        await HandleExceptionAsync(context, ex);
      }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
      context.Response.ContentType = "application/json";
      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
      await context.Response.WriteAsync(new ErrorDetails() {
        StatusCode = context.Response.StatusCode, Message = exception.Message
      }
                                            .ToString());
    }
  }
}
