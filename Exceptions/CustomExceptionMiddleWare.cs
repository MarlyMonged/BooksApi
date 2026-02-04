namespace BooksApi.Exceptions
{
    public class CustomExceptionMiddleWare
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(PublisherNameException ex)
            {
                context.Response.StatusCode = 400;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message,
                    publisherName = ex.PublisherName
                });
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    message = ex.Message,
                });
            }
        }
    }
}
