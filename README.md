# Umbraco .NET Templates

This repository contains .NET templates for Umbraco, designed to streamline the setup of new Umbraco projects. These templates aim to provide a solid foundation for developers working with Umbraco CMS.

## Features

- Pre-configured .NET templates for Umbraco
- Optimized project structure
- Easy installation and usage
- Regular updates to align with new upstream package updates, etc

## Available Templates

The templates are orchestrated with .NET Aspire for a ready-to-go, batteries included developer experience

- `umbraco-headless-bff` - This is the main template which contains both CMS project and a BFF Api layer

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
  - MiguelGuedelha.UmbracoTemplates::16.x.x -> Umbraco 16
  - etc...
- Feedback and suggestions are highly appreciated!

Happy coding! 🚀
