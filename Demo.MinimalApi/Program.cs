//Cred: https://khalidabuhakmeh.com/static-abstract-members-in-csharp-10-interfaces

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapMethods("/", new[] { "GET" }, (HttpRequest _) => "Hello, World!");
app.MapMethods("/foo", new[] { "GET" }, (HttpRequest _) => "Hello, foo!");

app.Run();


public interface IHandler
{
    static abstract string Route { get; }
    static abstract HttpMethod Method { get; }
    static abstract Delegate Handle { get; }
}

public record struct GetDefault : IHandler
{
    public static HttpMethod Method => HttpMethod.Get;
    public static string Route => "/";
    public static Delegate Handle => (HttpRequest _) => "Hello, World!";
}

public record struct GetFoo : IHandler
{
    public static HttpMethod Method => HttpMethod.Get;
    public static string Route => "/foo";
    public static Delegate Handle => (HttpRequest _) => "Hello, foo!";
}

public static class ApplicationHandlerExtensions
{
    public static void MapHandler<T>(this WebApplication app)
        where T : IHandler
    {
        app.MapMethods(T.Route, new[] { T.Method.ToString() }, T.Handle);
    }
}