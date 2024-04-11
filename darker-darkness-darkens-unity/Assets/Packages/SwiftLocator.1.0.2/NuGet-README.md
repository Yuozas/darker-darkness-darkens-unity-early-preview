# ![Logo](https://raw.githubusercontent.com/Euphelia-Interactive/SwiftLocator/master/Logo.png) SwiftLocator
[![Build Status](https://github.com/Euphelia-Interactive/SwiftLocator/actions/workflows/CI.yml/badge.svg)](https://github.com/Euphelia-Interactive/SwiftLocator/actions/workflows/CI.yml)
[![NuGet version (SwiftLocator)](https://img.shields.io/nuget/v/SwiftLocator.svg?style=flat-square)](https://www.nuget.org/packages/SwiftLocator/)

Description
---

SwiftLocator is a simple .NET 5 library that implements:
* Transient, Scoped, Singleton service scopes. 
* Service Locator.
* Optional Dependency Injection for the services.

How it works
---
There is a basic static class ```ServiceLocator``` that has registration and getting logic for transient, scoped, singleton service types.
Registrator interface that ServiceLocator provides has many overloads, you can:
* Pass instance
* Use factory
* Pass generic class type that will be used to construct new instances with automated dependency injection.
* Pass generic class with generic interface/abstract type that will always be a real class representative while getting the service.

Transient doesn't support instance registration since the instance wouldn't be transient anymore as it is always the same.

#### Dependency injection 
Dependency injection is automated for generic types you pass. It will look up whether your generic class constructor is asking for any properties and will inject them. 
Adding types that are not registered to the constructor will generate an error.

Singleton services can receive singleton and transient dependencies.
Transient services can receive singleton and transient dependencies.
Scoped services can receive all dependencies.

Option to do if you don't want automated dependency injection 
* Use an empty constructor
* Register service with an instance
* Register service with a factory function.

Use example
---
Here only a few examples are covered cause the namings of the methods are all the same and using the method overloads of this library are very intuitive.

Codespace somewhere where you have a first program initialization (recommended way of usage).
```csharp
// Register singleton services.
ServiceLocator.SingletonRegistrator.Register<ParentTestClass>();

// Register transient services.
ServiceLocator.TransientRegistrator
    .Register<SecondChildInjectClass>()
    .Register<ChildInjectClass>()
    .Register<ChildChildInjectClass>();
```
Codespace anywhere in the project.
```csharp

// Get singleton service with automatically injected transient 
// and scoped instances that ParentTestClass class is asking for in contstructor.
var singletonService = ServiceLocator.GetSingleton<ParentTestClass>();

// Get transient service with automatically injected transient 
// and scoped instances that ParentTestClass class is asking for in contstructor.
var singletonService = ServiceLocator.GetTransient<SecondChildInjectClass>();
```
Scoped example
You can execute you're scope registration whenever or wherever you see fit.
```csharp
// This key will act as a key to access the scope.
// With it, scoped service is the same as a singleton service, but it's tied to a key which allows singleton services 
// to act like scoped services if keys and registration are properly used.
const string scopeKey = "my random scope key";

ServiceLocator.GetScopedRegistrator(scopeKey)
    .Register<ParentTestClass>()
    .Register<SecondChildInjectClass>()
    .Register<ChildInjectClass>()
    .Register<ChildChildInjectClass>();

var scopedService = ServiceLocator.GetScoped<ParentTestClass>(scopeKey);
```

Urls
---
- [NuGet Package](https://www.nuget.org/packages/SwiftLocator)
- [License](https://github.com/Euphelia-Interactive/SwiftLocator/blob/main/LICENSE.md)
