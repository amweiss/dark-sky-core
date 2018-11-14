# Dark Sky Core

[![NuGet](https://img.shields.io/nuget/v/DarkSkyCore.svg?maxAge=2592000)](https://www.nuget.org/packages/DarkSkyCore) [![Build Status](https://dev.azure.com/amweiss/dark-sky-core/_apis/build/status/dark-sky-core-CI)](https://dev.azure.com/amweiss/dark-sky-core/_build/latest?definitionId=5)

A .NET Standard Library for using the [Dark Sky API](https://darksky.net/dev/docs).

## Usage

The main class is [`DarkSkyService`](https://github.com/amweiss/dark-sky-core/blob/master/src/DarkSkyCore/Services/DarkSkyService.cs). When using it you will need provide your API key after [signing up](https://darksky.net/dev/) for a dev account.
You can also provide an implementaion of [`IHttpClient`](https://github.com/amweiss/dark-sky-core/blob/master/src/DarkSkyCore/Services/IHttpClient.cs) if you want to replace the default [`ZipHttpClient`](https://github.com/amweiss/dark-sky-core/blob/master/src/DarkSkyCore/Services/ZipHttpClient.cs)
for testing or other purposes.

Once you have an instance of the class, use `GetForecast` to use the API. The method by default is a [forecast](https://darksky.net/dev/docs/forecast) request.
If you specify a value for `ForecastDateTime` in an `OptionalParameters` instance it will become a [time machine](https://darksky.net/dev/docs/time-machine) request.

The responses all take the form of a [CamelCase](https://en.wikipedia.org/wiki/PascalCase) version of the [Dark Sky Response](https://darksky.net/dev/docs/response) in `DarkSkyResponse`.
This includes the [headers](https://darksky.net/dev/docs/response#response) and properties for the required text and link to use based on the [Terms of Service](https://darksky.net/dev/docs/terms).

You can see an example usage in the [integration tests](https://github.com/amweiss/dark-sky-core/blob/master/test/DarkSkyCoreIntegrationTests/IntegrationTests/DarkSkyServiceIntegrationTests.cs).

## Additional Information

[Code of Conduct](CODE_OF_CONDUCT.md) and [Contributing Guidelines](CONTRIBUTING.md)for the project.
