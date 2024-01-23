# Curatify
A web application that curates various media sources, implemented with "ASP.NET Core Web App". 
It aggregates **media sources such as podcast channels, YouTube videos, and blog posts** (RSS feeds) and populates **recent items**.

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#installation">Installation</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <!-- <li><a href="#contributing">Contributing</a></li> -->
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <!-- <li><a href="#acknowledgments">Acknowledgments</a></li> -->
  </ol>
</details>

## About The Project

> [!WARNING]  
> This repository represents a technical exercise I undertook early in my ASP.NET learning journey.
> At present, it is not fully operational or finalized. However, I may choose to revisit and further develop it in the future.

[![Screenshot](https://raw.githubusercontent.com/jackie4u/dime-pub/main/Screenshot-Index-01.jpg)](https://dimepub.azurewebsites.com)

This project is intended as a hobby project on which I tried various developing techniques.
The intent is to develop a comprehensive media aggregator as **a web app that curates various media sources**.

Web app aggregates media sources like **podcast channels, YouTube videos, and blog posts** and populates recent **episodes (items/articles)**.

It is implemented as a simple media reader in ASP.NET Core (Model-View-Controller) with .Net 7.0 and Entity Framework Core 7.
For GUI, Bootstrap 5 and some icons from Font Awesome 6.

I have tried to separate the data layer into a stand-alone project **FeedDataLibrary** with a repository pattern and MVVM approach.
However, I have not grasped these concepts appropriately yet, so it need further refactoring.

There are currently some bugs and not implemented features. Some of them are marked in a comment with *TODO* tag .

### Built With

* ASP.NET Core Web App (Model-View-Controller)
* .Net 7.0 (LTS)
* Entity Framework Core 7
* Microsoft SQL Server
* Bootstrap 5
* Font Awesome 6

## Getting Started

To get a local copy up and running follow these simple example steps.

### Prerequisites

* Install .NET 6 (SDK and Runtime) from https://dotnet.microsoft.com/en-us/download/dotnet/6.0
* Install powershell global tools
  ```powershell
  dotnet tool install dotnet-ef -g
  dotnet tool install Microsoft.Web.LibraryManager.Cli -g
  ```
* Install Microsoft SQL Server from https://www.microsoft.com/en-in/sql-server/sql-server-downloads

### Installation

1. Clone the repo
```sh
   git clone https://github.com/jackie4u/dime-pub.git
```

2. Enter the name of your SQL Server in `appsettings.json` and eventually change the database name and security
```json
  {
    "Logging": {
      "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
      }
    },
    "AllowedHosts": "*",
    "ConnectionStrings": {
      "DimePubConnection": "Data Source=<SQL Server>; Initial Catalog=DimePubDb; Integrated Security=True"
    }
  }
```

3. Initialize database
```powershell
   cd FeedDataLibrary/dime-pub
   dotnet ef --startup-project ../DimePubWeb database update
```

4. Run the application
```powershell
   dotnet run
```

## Usage

The content of the web app focuses on aggregating various media sources (currenlty only via RSS feeds).

### Episodes (articles):
- The index page lists all episodes (articles)
- If you are on a different page click on "Episodes" link in the top menu
- By default, there are shown only the latest 10 episodes - for older episodes follows page numbers at the bottom of the page
- To filter episodes (articles) by date choose the date range from the top header and press "Filter"
- To Reload all episodes (articles) from all podcasts (feeds) press the button "Refresh all"

### Sources:
- To list all sources click on "Sources" link in the top menu
- To filter sources by title enter string into the search textbox and press "Search"
- To check the detail of the selected source and list all episodes (articles) in that source click on the "Detail" link in the "Sources" table
- To delete a source click on the "Delete" link in the "Sources" table or the "Delete" button on the source detail page
- To Reload articles in selected source press button "Refresh" on the source detail page

#### To add a new RSS feed 
1. **click on "Add new source"** 
2. enter the "Source URL" of your favourite media source - for example, Coding Blocks at "https://www.codingblocks.net/podcast-feed.xml"
3. optionally add your custom "Source Note"
3. press the button "Save new source"

## Roadmap

- [ ] Implement about page
- [ ] Improve graphic design
- [ ] Add checkboxes for deleting multiple sources from the source list
    - [ ] Add button for checking all checkboxes
- [ ] Implement error handling
- [ ] Add sorting to list views and tables
    - [ ] Add sorting to sources
    - [ ] Add sorting to episodes
- [ ] Implement IsRead flag

See the [open issues](https://github.com/jackie4u/curatify/issues) for a full list of proposed features (and known issues).

## Contributing

If you have a suggestion that would make this better, please fork the repo and create a pull request. You can also simply open an issue with the tag "enhancement".
Don't forget to give the project a star! Thanks again!

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the GNU GENERAL PUBLIC LICENSE v3 License. 
See `LICENSE.txt` for more information.

## Contact

Twitter - [@jackie4u](https://twitter.com/jackie4u)

## Acknowledgments

**This project is influenced by learning from the great book ["Pro ASP.NET Core 6: Develop Cloud-Ready Web Applications Using MVC, Blazor, and Razor Pages"](https://www.amazon.com/dp/B09TDKPSCZ) by [Adam Feeman(MarkP88)](https://github.com/MarkP88).**

[contributors-shield]: https://img.shields.io/github/contributors/jackie4u/curatify.svg?style=for-the-badge
[contributors-url]: https://github.com/jackie4u/curatify/graphs/contributors
[forks-shield]: https://img.shields.io/github/forks/jackie4u/curatify.svg?style=for-the-badge
[forks-url]: https://github.com/jackie4u/curatify/network/members
[stars-shield]: https://img.shields.io/github/stars/jackie4u/curatify.svg?style=for-the-badge
[stars-url]: https://github.com/jackie4u/curatify/stargazers
[issues-shield]: https://img.shields.io/github/issues/jackie4u/curatify.svg?style=for-the-badge
[issues-url]: https://github.com/jackie4u/curatify/issues
[license-shield]: https://img.shields.io/github/license/jackie4u/curatify.svg?style=for-the-badge
[license-url]: https://github.com/jackie4u/curatify/blob/master/LICENSE.txt
[linkedin-shield]: https://img.shields.io/badge/-LinkedIn-black.svg?style=for-the-badge&logo=linkedin&colorB=555
[linkedin-url]: https://linkedin.com/in/vackar
[product-screenshot]: images/screenshot.png
