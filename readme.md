# Generic BCL interface tests

## Idea

This repository provides generic tests for classes that implement interfaces of the .NET base class library (e. g. IEquatable, IEquatable&lt;T&gt;...).

## Why should I use this project?

When designing classes for your project, you often end up implementing interfaces of the .NET base class library, e. g. you might implement a class deriving from IEquatable&lt;T&gt; and IComparable&lt;T&gt;. When testing your classes, you find that you write very similar test code. This repository provides generic test cases which are able to test your classes with only very little adaption.

## How do I set up a test to run with my classes?

For testing your class Foo that implements the IEquatable&lt;T&gt; interface, for example, you derive your test class from TestIEquatableOfTFor&lt;Foo&gt;. In the constructor of your test class, you invoke the constructor of the base class, passing lambda expressions that adapt your class to the test logic. For example, TestIEquatableOfTFor&lt;T&gt; needs to know how an instance of your class is created, how an equal instance of your class is created, and how a different instance of your class is created.

## Current status

Proof of concept. __Do not use in production code__.
