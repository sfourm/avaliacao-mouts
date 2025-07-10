﻿namespace Ambev.DeveloperEvaluation.Packages.Application.Exceptions;

public static class ThrowIfNullException
{
    public static T ThrowIfNull<T>(this T argument)
    {
        if (argument != null) return argument;

        throw new ArgumentNullException(typeof(T).Name);
    }
}