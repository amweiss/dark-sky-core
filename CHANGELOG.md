# Changelog

Sorry for not starting it earlier, this may change over time but for now I'll list changes between major versions here.

## From version 5 -> 6

The `ResponseHeaders` are now a separate model class, not an inner class.
The `ExclusionBlocks` are now a separate model class, not an inner class.
The `OptionalParameters` are now a separate model class, not an inner class.
`DarkSkyService` now correctly implements `IDisposable` and should be handled as such.
`IHttpClient` now correctly implements `IDisposable` and should be handled as such.
`IHttpClient` method name changed from `HttpRequest` to `HttpRequestAsync` to correctly reflect that it's async.

## From version 4 -> 5

The `Flags` object now has `AdditionalData` to capture all the undocumented fields instead of casting to a few known ones. See [8f8a6f5](https://github.com/amweiss/dark-sky-core/commit/8f8a6f5e5109f909b039fa7cfc8c88a715d6723a)

## From version 3 -> 4

The `Flags` object now has `NearestStation` as a `double?` correctly. See [f631325](https://github.com/amweiss/dark-sky-core/commit/f6313258ba2f375370ae9cdacb9376936783420a)

## From version 2 -> 3

All `DateTimeOffset` property names now end with `DateTime`, see [59cf6f2](https://github.com/amweiss/dark-sky-core/commit/59cf6f26a9b9b1213d24f4572d6ae8d2531a0535)
