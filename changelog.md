# DevHawk.RIPEMD60 Change Log

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

This project uses [NerdBank.GitVersioning](https://github.com/AArnott/Nerdbank.GitVersioning)
to manage version numbers. This tool automatically sets the Semantic Versioning Patch
value based on the [Git height](https://github.com/AArnott/Nerdbank.GitVersioning#what-is-git-height)
of the commit that generated the build. As such, released versions of this extension
will not have contiguous patch numbers. Initial major and minor releases will be documented
in this file without a patch number. Patch version will be included for bug fix releases, but
may not exactly match a publicly released version.

## [3.0] - Unreleased

### Added

- Added `RIPEMD160.IncrementalHash` class, modeled after the sealed
  [System.Security.Cryptography.IncrementalHash](https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.incrementalhash?view=netstandard-2.1)
  class from .NET Standard 2.1.
- Added internal `RIPEMD160HashProvider` class to share hashing logic between `RIPEMD160`
  and `RIPEMD160.IncrementalHash` classes
- added [SourceLink](https://github.com/dotnet/sourcelink) support.

### Removed

- **Breaking Change**: Removed `RIPEMD160.Create()` and `RIPEMD160.Create(string)` static methods.
  - To create a `RIPEMD160` instance, simply use `new RIPEMD160()` rather than `RIPEMD160.Create()`
- **Breaking Change**: Removed `RIPEMD160Managed` class.

## [2.0] - 2019-10-08

Initial Public Release
