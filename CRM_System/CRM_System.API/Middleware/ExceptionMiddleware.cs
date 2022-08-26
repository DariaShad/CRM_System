﻿using CRM_System.BusinessLayer;
using System.Net;

namespace CRM_System.API;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.NotFound, error.Message);
        }
        catch (AccessDeniedException error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Forbidden, error.Message);
        }
        catch (NotUniqueEmailException error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.Conflict, error.Message);
        }
        catch (Exception error)
        {
            await HandleExceptionAsync(httpContext, HttpStatusCode.InternalServerError, error.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(new ErrorResult()
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        }.ToString());
    }
}
