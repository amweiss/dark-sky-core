#region

using System.Diagnostics.CodeAnalysis;

#endregion

[assembly:
    SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task",
        Justification = "Not needed for coreapp", Scope = "namespaceanddescendants", Target = "DarkSkyCore.Tests")]
[assembly:
    SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes",
        Justification = "Keeping local for clear tests", Scope = "type",
        Target = "~T:DarkSkyCore.Tests.UnitTests.Services.DarkSkyEnumJsonConverterUnitTests")]