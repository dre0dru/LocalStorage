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