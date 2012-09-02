What does Multiple Web Config Transformations mean?
===================================================

Visual Studio 2010 and MSBuild 4.0 introduced the concept of Web Config Transformations to .NET projects; allowing
elements and attibutes to be added, removed or changed when a Web Project is published. In it's first incarnation Web
Config Transformations were applied depending on the Build Configuraiton selected.

For the projects I have worked on at least this has resulted in build configurations getting out of hand with many build
configurations being created to service the various environments we deploy to, for example:

 * Debug
 * Release
 * Dev - Project #1
 * QA - Project #1
 * Testing - Project #1
 * Staging - Project #1
 * Production - Project #1
 * Dev - Project #2
 * QA - Project #2
 * Testing - Project #2
 * Staging - Project #2
 * Production - Project #2

Typically the only differences between projects is the database connection string, all other paramters (Configuration,
Error Pages, Redirects, etc.) will be the same in each environment (i.e. QA, Testing, Production, Staging and Live).

What does this project provide?
===============================

Namely a way to apply a second tier of Web Config Transformation after the Build Configuraiton. This is acheived through
the following components:

 1. A MSBuild .targets file providing the actual functionality to provide a second transformation
 2. An addition of a $(Environment) property in the project file allowing the environment to be specified
 3. A naming scheme for the second tier web.configs
 
This allows for the standard two build configurations (Debug and Release), to be applied across multiple environments
(QA, Testing, Staging, Production).
 
What is this naming scheme?
===========================

Web.Configs are aranged into three tiers:

 * Web.Config - the base configuration file that is transformed
  * Web.{Configuration}.Config - build configuration specific Web Config Transform (i.e. Web.Release.Config)
   * Web.{Configuration}.{Environment}.Config - environment and build configuration specific Web Config Transform (i.e. 
     Web.Release.QA.Config

So for example in a scenario when the Release build configuration is specified and the QA environment is specified and
the project is published:

 1. Web.Config will copied to a staging directory and have the Web.Release.Config transform applied.
 2. The transformed version of Web.Config will be copied with a different file name and have the Web.Release.QA.config 
 transform applied.
 3. Finally the now twice transformed Web.Config will be published with the rest of the project files.

Isn't this already supported in Visual Studio 2012?
===================================================

I belive it has been resolved; Visual Studio 2012 and MSBuild 4.5 attempted to solve this issue by allowing multiple
web transformations to be applied sequentially this is likely to be sufficent for the majority of developers. However if
you have more specific requirements or are unable to move away from Visual Studio 2010 and MSBuild 4.0 then this project
goes a step towards getting multiple Web Config Transformations.