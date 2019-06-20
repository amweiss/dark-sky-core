
// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.

[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "Not globalizing", Scope = "namespaceanddescendants", Target = "DarkSky")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Injected or serialized", Scope = "type", Target = "~T:DarkSky.Tests.UnitTests.Services.DarkSkyEnumJsonConverterUnitTests.SampleIconObject")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1812:Avoid uninstantiated internal classes", Justification = "Injected or serialized", Scope = "type", Target = "~T:DarkSky.Tests.UnitTests.Services.DarkSkyEnumJsonConverterUnitTests.SamplePrecipitationTypeObject")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Needed for JSON deserialization", Scope = "namespaceanddescendants", Target = "DarkSky.Models")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "options can be null", Scope = "member", Target = "~M:DarkSky.Services.DarkSkyService.GetForecast(System.Double,System.Double,DarkSky.Models.OptionalParameters)~System.Threading.Tasks.Task{DarkSky.Models.DarkSkyResponse}")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "API needs lowercase", Scope = "member", Target = "~M:DarkSky.Services.DarkSkyService.BuildRequestUri(System.Double,System.Double,DarkSky.Models.OptionalParameters)~System.String")]
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Non static for serialization reasons", Scope = "type", Target = "~T:DarkSky.Models.DarkSkyResponse")]
