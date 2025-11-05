# Umbraco .NET Templates

This repository contains opinionated, (sort of) batteries-included .NET templates for Umbraco, designed to streamline the setup of new Umbraco projects. These templates aim to provide a solid foundation for developers working with Umbraco CMS.

## Features

- Pre-configured .NET templates for Umbraco
- Optimized project structure
- Easy installation and usage
- Regular updates to align with new upstream package updates, etc

## Available Templates

The templates are orchestrated with .NET Aspire for a ready-to-go developer experience

- `umbraco-headless-bff` - This is the main template which contains both CMS project and a BFF API layer

## Installation

To install the templates, run the following command:

```sh
 dotnet new install MiguelGuedelha.UmbracoTemplates
```

## Usage

Once installed, you can create a new project using:

```sh
 dotnet new <template-name> -n MyUmbracoProject
```

Replace `<template-name>` with the specific template name you wish to use.

## Contributing

Contributions are welcome! If you would like to improve the templates, feel free to submit a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

---

### Notes

- Ensure you have .NET SDK 8+ installed.
- These templates are maintained and versioned alongside Umbraco versioning.
  - MiguelGuedelha.UmbracoTemplates::13.x.x -> Umbraco 13
  - MiguelGuedelha.UmbracoTemplates::17.x.x -> Umbraco 17
  - etc...
- Feedback and suggestions are highly appreciated!

> [!WARNING]
> With Umbraco 17 in the horizon (early Nov as of this README edit) creating brand new projects with Umbraco 13 makes less and less sense.
>
> With this in mind, no further updates will be done to the V13 branch and no further tags/releases will be created for V13
> From the moment Umbraco 17 is out and the dependency packages are all available a initial 17.0.0 template will be releases

Happy coding! 🚀
