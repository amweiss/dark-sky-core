
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "CA2007:Consider calling ConfigureAwait on the awaited task", Justification = "Not needed for coreapp", Scope = "namespaceanddescendants", Target = "DarkSky.Tests")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Keeping local for clear tests", Scope = "type", Target = "~T:DarkSky.Tests.UnitTests.Services.DarkSkyEnumJsonConverterUnitTests")]

