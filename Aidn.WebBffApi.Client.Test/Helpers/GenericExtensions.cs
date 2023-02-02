using Refit;
using System.Net;

namespace Aidn.WebBffApi.Client.Test.Helpers;

public static class GenericExtensions
{
    public static ApiResponse<T> ToApiResponse<T>(this T obj, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ApiResponse<T>(new HttpResponseMessage(statusCode), obj, new RefitSettings());
    }
}

