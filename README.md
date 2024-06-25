# Movie Web Application

## Overview

This Movie Web Application is built using ASP.NET Core with Entity Framework for data access. The application allows users to manage movies, actors, categories, and platforms. It features CRUD (Create, Read, Update, Delete) operations for all entities and integrates with the ImgBB API for image uploads. The front end is enhanced with Bootstrap for improved visual quality and user experience, and Select2 is used for better handling of multiple selections.

## Features

- **Movie Management**: Add, edit, delete, and view details of movies.
- **Actor Management**: Add, edit, delete, and list actors.
- **Category Management**: Add, edit, delete, and list categories.
- **Platform Management**: Add, edit, delete, and list platforms.
- **Image Upload**: Upload images for movies using ImgBB API.
- **Filtering**: Filter movies by category, platform, and IMDb ratings.
- **Recommendations**: Display recommended movies on the home page.
- **Enhanced UI**: Utilize Bootstrap for a responsive and visually appealing design.
- **Improved Selection**: Use Select2 for handling multiple selections in forms.
- **JavaScript Enhancements**: Include custom JavaScript for better user interactions, such as image uploads.

## Technologies Used

- **Backend**: ASP.NET Core, Entity Framework Core
- **Frontend**: Bootstrap, JavaScript, Select2
- **Database**: MSSQL
- **Image Hosting**: ImgBB API

## Installation

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/yourrepository.git
    ```

2. **Navigate to the project directory**:
    ```bash
    cd yourrepository
    ```

3. **Install the required packages**:
    ```bash
    dotnet restore
    ```

4. **Update the database connection string**:
   - Open `appsettings.json` and update the `ConnectionStrings` section with your MSSQL database connection string.

5. **Apply database migrations**:
    ```bash
    dotnet ef database update
    ```

6. **Run the application**:
    ```bash
    dotnet run
    ```

## Usage

### Movie Management

- **Create**: Navigate to the "Create Movie" page, fill in the details, upload an image, and submit.
- **Edit**: Navigate to the "Edit Movie" page, modify the details, optionally upload a new image, and submit.
- **Delete**: Navigate to the movie list, select a movie, and delete.
- **Details**: View detailed information about a movie by selecting it from the movie list.

### Actor, Category, and Platform Management

- **Create, Edit, Delete, and List**: Perform CRUD operations on actors, categories, and platforms through their respective management pages.

### Filtering Movies

- **Filter by Category**: Select a category from the filter options to view movies in that category.
- **Filter by Platform**: Select a platform from the filter options to view movies available on that platform.
- **Filter by IMDb Ratings**: Select an IMDb rating range to view movies within that range.

### Recommendations

- **Home Page**: View recommended movies displayed as cards on the home page.

## Customization

### ImgBB API Integration

To integrate with the ImgBB API for image uploads:

1. Obtain an API key from ImgBB.
2. Update the `ImgBBApiKey` in the `appsettings.json` file with your API key.

### Select2 Integration

To use Select2 in your project:

1. Install Select2 via npm or include the CDN link in your views.
2. Initialize Select2 in your JavaScript files for relevant form elements.

### Bootstrap Integration

Bootstrap is included via CDN. To customize the styling:

1. Override Bootstrap styles in your custom CSS files.
2. Ensure the custom CSS file is included after the Bootstrap CDN link in your views.

---

Feel free to adjust any details or add any additional sections specific to your project.