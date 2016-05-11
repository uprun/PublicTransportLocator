﻿using NUnit.Common;
using NUnit.Framework;
using NUnitLite;
using System;
using System.Reflection;


namespace UnitTests
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return new AutoRun(typeof(Program).GetTypeInfo().Assembly)
                .Execute(args, new ExtendedTextWrapper(Console.Out), Console.In);
        }
    }
}
