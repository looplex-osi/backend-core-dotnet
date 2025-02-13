# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/versionize/versionize) for commit guidelines.

<a name="4.0.6"></a>
## 4.0.6 (2025-02-13)

### Bug Fixes

* **database:** chaged open connection to async

<a name="4.0.5"></a>
## 4.0.5 (2025-02-13)

### Bug Fixes

* **databaseservice:** added query multiple async

<a name="4.0.4"></a>
## 4.0.4 (2025-02-11)

<a name="4.0.3"></a>
## 4.0.3 (2025-02-10)

<a name="4.0.2"></a>
## 4.0.2 (2025-01-16)

### Bug Fixes

* **irbacservice:** removed cancellation token from methods in interface

<a name="4.0.1"></a>
## 4.0.1 (2025-01-16)

### Bug Fixes

* **icrudservice:** removed cancellation token from methods in interface

<a name="4.0.0"></a>
## 4.0.0 (2025-01-16)

### Bug Fixes

* **context:** dispose context instance if it is IDisposable

### Breaking Changes

* **context:** cancellation token is now in the context state

<a name="3.2.3"></a>
## 3.2.3 (2025-01-15)

<a name="3.2.2"></a>
## 3.2.2 (2025-01-15)

### Bug Fixes

* **routes:** changed routebuilderoptions from array to list

<a name="3.2.1"></a>
## 3.2.1 (2025-01-15)

### Bug Fixes

* **github:** upgrade version to .net 9

<a name="3.2.0"></a>
## 3.2.0 (2025-01-15)

### Features

* **sdk:** upgrade to .net 9
* **utils:** added extension method to get caller member name

### Bug Fixes

* **dependencies:** upgrade nuget packages

<a name="3.1.1"></a>
## 3.1.1 (2025-01-14)

### Bug Fixes

* **rbac:** removed constraint on action definition

<a name="3.1.0"></a>
## 3.1.0 (2025-01-14)

### Features

* **irbacservice:** moved rbacservice interface from middlewares repo to this repo

<a name="3.0.5"></a>
## 3.0.5 (2025-01-14)

### Bug Fixes

* upgrade nuget packages

<a name="3.0.4"></a>
## 3.0.4 (2024-12-13)

<a name="3.0.3"></a>
## 3.0.3 (2024-12-11)

<a name="3.0.2"></a>
## 3.0.2 (2024-12-11)

<a name="3.0.1"></a>
## 3.0.1 (2024-12-04)

<a name="3.0.0"></a>
## 3.0.0 (2024-11-28)

### Features

* **isecretsservice:** add interface for secret manager service

### Bug Fixes

* **idatabasecontext:** removed old data access interfaces, removed core infra project

### Breaking Changes

* **idatabasecontext:** removed old data access interfaces, removed core infra project

<a name="2.3.0"></a>
## 2.3.0 (2024-11-28)

### Features

* **isqldatabaseservice:** added sql database and provider interfaces

<a name="2.2.0"></a>
## 2.2.0 (2024-11-28)

### Features

* **iredisservice:** added interface for redis and removed cache interface

<a name="2.1.1"></a>
## 2.1.1 (2024-11-25)

### Bug Fixes

* **cache:** added cache service factory

<a name="2.1.0"></a>
## 2.1.0 (2024-11-12)

### Features

* **jsonutils:** added traverse method and removed unused old files

<a name="2.0.1"></a>
## 2.0.1 (2024-10-28)

<a name="2.0.0"></a>
## 2.0.0 (2024-10-28)

### Bug Fixes

* **pagination:** moved to scimv2 project

### Breaking Changes

* **pagination:** moved to scimv2 project

<a name="1.3.0"></a>
## 1.3.0 (2024-10-18)

### Features

* **icacheservice:** added new interface for a cache service

<a name="1.2.1"></a>
## 1.2.1 (2024-10-16)

### Bug Fixes

* **routebuilder:** added missing mapput route method

<a name="1.2.0"></a>
## 1.2.0 (2024-10-16)

### Features

* **icrudservice:** added update method

<a name="1.1.2"></a>
## 1.1.2 (2024-09-12)

### Bug Fixes

* **observableproxy:** added intercept to proxy equals to apply on target

<a name="1.1.1"></a>
## 1.1.1 (2024-09-11)

### Bug Fixes

* **observableproxy:** now child properties and elements or collections that are eligible are proxies

<a name="1.1.0"></a>
## 1.1.0 (2024-09-06)

### Features

* added observable trait for type changes tracker

<a name="1.0.18"></a>
## 1.0.18 (2024-07-30)

### Bug Fixes

* **routebuilder:** added map patch

<a name="1.0.17"></a>
## 1.0.17 (2024-07-30)

### Bug Fixes

* **icrudservice:** added patch method

<a name="1.0.16"></a>
## 1.0.16 (2024-07-20)

### Bug Fixes

* **jsonutils:** changed write async json extension  method to receive an already serialized value

<a name="1.0.15"></a>
## 1.0.15 (2024-07-20)

### Bug Fixes

* upgrade open for extension to 1.1.1

<a name="1.0.14"></a>
## 1.0.14 (2024-07-20)

### Bug Fixes

* upgraded open for extension package

<a name="1.0.13"></a>
## 1.0.13 (2024-07-19)

### Bug Fixes

* removed dtos, upgraded packages

<a name="1.0.12"></a>
## 1.0.12 (2024-07-19)

### Bug Fixes

* removed open api

<a name="1.0.11"></a>
## 1.0.11 (2024-07-19)

### Bug Fixes

* **icontextfactory:** moved to application.abstractions

<a name="1.0.10"></a>
## 1.0.10 (2024-07-19)

### Bug Fixes

* **exceptionmiddleware:** added case for entityinvalidexception

<a name="1.0.9"></a>
## 1.0.9 (2024-07-19)

### Bug Fixes

* upgrade open for extensio to 1.0.9. fixed warnings

<a name="1.0.8"></a>
## 1.0.8 (2024-07-18)

<a name="1.0.7"></a>
## 1.0.7 (2024-07-18)

### Bug Fixes

* **exceptions:** added exception for entity validation

<a name="1.0.6"></a>
## [1.0.6](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.6) (2024-07-11)

### Bug Fixes

* **routebuilder:** post maps were not adding correct middlewares ([f42082c](https://www.github.com/looplex-osi/backend-core-dotnet/commit/f42082ca9e9e8e46de83a6037c1e1479f7fce824))

<a name="1.0.5"></a>
## [1.0.5](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.5) (2024-07-11)

<a name="1.0.4"></a>
## [1.0.4](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.4) (2024-07-11)

### Bug Fixes

* **core.webapi:** added default ext methods to add profiles and services ([0577a61](https://www.github.com/looplex-osi/backend-core-dotnet/commit/0577a61ad99a9f7cdd692df9e3f6f2a78b4a73f9))

<a name="1.0.3"></a>
## [1.0.3](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.3) (2024-07-11)

### Bug Fixes

* **routebuilder:** improvements on route builder ([f9fd1d2](https://www.github.com/looplex-osi/backend-core-dotnet/commit/f9fd1d2f6d3b25fe6da10edeb11e7191cdfcac57))

<a name="1.0.2"></a>
## [1.0.2](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.2) (2024-07-08)

### Bug Fixes

* upgraged OpenForExtension packages to 1.0.8 ([6567b87](https://www.github.com/looplex-osi/backend-core-dotnet/commit/6567b8735cb8ee46debd38542e511dcb602249b8))

<a name="1.0.1"></a>
## [1.0.1](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.1) (2024-07-02)

### Bug Fixes

* **routebuilder:** context should be unique per request ([bfe4321](https://www.github.com/looplex-osi/backend-core-dotnet/commit/bfe43215a41436d5449c1fa96e43bc85d2be6c54))

<a name="1.0.0"></a>
## [1.0.0](https://www.github.com/looplex-osi/backend-core-dotnet/releases/tag/v1.0.0) (2024-07-01)

