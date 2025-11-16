# Umbraco .NET Templates

This repository contains .NET template(s) for Umbraco, designed to streamline the setup of new Umbraco projects. It aims to provide a solid foundation for developers working with Umbraco CMS.

## Features

- Pre-configured .NET template(s) for Umbraco
- Optimized project structure, with a high focus separation by feature
- Easy installation and usage

## Available Templates

The templates are orchestrated with .NET Aspire for a ready-to-go, batteries included developer experience

- `umbraco-headless-bff` - This is the main template which contains both a CMS project and a BFF Api layer

## Installation

To install the templates, run the following command:

```sh
 dotnet new install MiguelGuedelha.UmbracoTemplates
```

## Usage

Once installed, you can create a new project with the usual .NET template syntax, such as:

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

- Ensure you have .NET SDK 10+ installed.
- These templates are maintained and versioned alongside Umbraco versioning.
  - MiguelGuedelha.UmbracoTemplates::17.x.x -> Umbraco 17
  - etc...
- I've started to mark V13 versions of the package as deprecated, as I have no plans in maintaining these anymore with the new LTS around the corner
- Feedback and suggestions are highly appreciated!

Happy coding! 🚀
