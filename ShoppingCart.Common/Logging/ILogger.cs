﻿using System;

namespace ShoppingCart.Common.Logging
{
    public interface ILogger
    {
        void Log(string message, LogLevel level, Exception exception = null);
    }

    public enum LogLevel
    {
        Fatal,
        Error,
        Warn,
        Info,
        Debug
    }
}