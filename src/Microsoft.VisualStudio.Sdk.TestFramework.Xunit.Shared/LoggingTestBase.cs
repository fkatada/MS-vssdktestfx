﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.Sdk.TestFramework;

/// <summary>
/// A base class for Xunit tests that need logging.
/// </summary>
public abstract class LoggingTestBase : TestBase
{
    private CancellationTokenRegistration timeoutLoggerRegistration;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoggingTestBase"/> class.
    /// </summary>
    /// <param name="logger">The xunit logging helper. Can be taken directly as a concrete test class constructor parameter.</param>
    public LoggingTestBase(ITestOutputHelper logger)
    {
        this.Logger = logger;
        this.timeoutLoggerRegistration = this.TimeoutToken.Register(() => this.Logger.WriteLine($"TEST TIMEOUT: {nameof(TestBase)}.{nameof(this.TimeoutToken)} has been canceled due to the test exceeding the {this.UnexpectedTimeout} time limit."));
    }

    /// <summary>
    /// Gets the logger for the current.
    /// </summary>
    public ITestOutputHelper Logger { get; }

    /// <inheritdoc/>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.timeoutLoggerRegistration.Dispose();
            (this.Logger as IDisposable)?.Dispose();
        }

        base.Dispose(disposing);
    }
}
