# [3.0.0](https://github.com/dre0dru/LocalStorage/compare/v2.4.0...v3.0.0) (2021-05-19)


### Features

* misspelled footer in commits, need to bump version ([53bece0](https://github.com/dre0dru/LocalStorage/commit/53bece0575dfe033bb9c844d3793afcebd230836))


### BREAKING CHANGES

* bump package version

# [2.4.0](https://github.com/dre0dru/LocalStorage/compare/v2.3.0...v2.4.0) (2021-05-19)


### Features

* added `IPlayerPrefsStorage` and its implementation ([310d9e9](https://github.com/dre0dru/LocalStorage/commit/310d9e99dd5f6032764b6b89958386eac555dfa4))
* added new `async` APIs for encryption and compression, removed `async` keyword and replaced with task chaining ([549e7b7](https://github.com/dre0dru/LocalStorage/commit/549e7b77ee6360a5853fab1075d87e3ae221c4b7))
* changed `IFileStorage` generic interface declaration to use only `ISerializationProvider` as generic constraint ([683e57f](https://github.com/dre0dru/LocalStorage/commit/683e57fe0bb42c2701d1a1e83b3148f5c0f58300))
* introduced `IDataTransform` and `DataTransformSerializationProvider` ([145b6f4](https://github.com/dre0dru/LocalStorage/commit/145b6f4b73da206d4d530479e2c236a1d327f7c4))
* preparing for adding player prefs storage ([aa1750c](https://github.com/dre0dru/LocalStorage/commit/aa1750c427dd0a9b87eb20a7d48d40e0aa05d229))

# [2.3.0](https://github.com/dre0dru/LocalStorage/compare/v2.2.0...v2.3.0) (2021-05-15)


### Features

* added async methods to `ISerializationProvider`, removed async keyword from `Storage`, returning chained tasks instead ([dce37ff](https://github.com/dre0dru/LocalStorage/commit/dce37ff34a6c9f6170456fb31422764196bc6276))

# [2.2.0](https://github.com/dre0dru/LocalStorage/compare/v2.1.0...v2.2.0) (2021-05-08)


### Features

* reintroduced `IStorage` interface for service abstraction ([8153927](https://github.com/dre0dru/LocalStorage/commit/8153927ea81bf3244e648e88854c96172a611c3d))

# [2.1.0](https://github.com/dre0dru/LocalStorage/compare/v2.0.0...v2.1.0) (2021-05-07)


### Features

* added compressed data provider ([e7d3ed7](https://github.com/dre0dru/LocalStorage/commit/e7d3ed7f3524f2b279ec9295b2c6e27555f2ffbb))

# [2.0.0](https://github.com/dre0dru/LocalStorage/compare/v1.0.0...v2.0.0) (2021-04-02)


### Bug Fixes

* `IOException` for custom path if directory does not exist ([47862e4](https://github.com/dre0dru/LocalStorage/commit/47862e47c0b89b521ba2628729e735c9eeb40737))
* fixed parameter name in `FileProvider` ([577c8c1](https://github.com/dre0dru/LocalStorage/commit/577c8c141272888d554da5264d282e47947dd7ca))


### Features

* added generic storage ([d0f203f](https://github.com/dre0dru/LocalStorage/commit/d0f203f747f1ab6804121a05826fc509ae5ab6b8))
* default path for `FileProvider` is `Application.persistentDataPath` ([885b7db](https://github.com/dre0dru/LocalStorage/commit/885b7db8c519431ebd94f6072cc29a2d1db94d9a))
* moved file manipulation from storage to file provider, added custom file paths ([90c62b1](https://github.com/dre0dru/LocalStorage/commit/90c62b12f4995db66168a745691cc9083697d005))


### BREAKING CHANGES

* file provider now uses `fileName` as parameter instead of `filePath`

# 1.0.0 (2021-03-22)


### Features

* removed storage interface - it is unnecessary abstraction ([1a1ff37](https://github.com/dre0dru/LocalStorage/commit/1a1ff377d2c2d236264a67c0efbe54aa37287012))
