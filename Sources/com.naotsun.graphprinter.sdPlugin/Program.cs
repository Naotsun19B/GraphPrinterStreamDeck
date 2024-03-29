﻿// Copyright 2023 Naotsun. All Rights Reserved.

using StreamDeckLib;
using System.Threading.Tasks;

namespace GraphPrinterStreamDeck
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            using var config = StreamDeckLib.Config.ConfigurationBuilder.BuildDefaultConfiguration(args);
            await ConnectionManager.Initialize(args, config.LoggerFactory)
                .RegisterAllActions(typeof(Program).Assembly)
                .StartAsync();
        }
    }
}
