# Bookstore web app

Angular client for browsing, viewing, adding, and deleting books through the bookstore API.

## Run locally

1. Start the API from the repository root with `dotnet run --project BookstoreArchitecture.Api --launch-profile https`.
2. In this directory, run `npm install` and then `npm start`.
3. Open `http://localhost:4200`.

The development server proxies `/books` requests to `https://localhost:7251`. The API uses the PostgreSQL connection configured in its application settings.

Run `npm test` for the frontend tests and `npm run build` to create a production build.
