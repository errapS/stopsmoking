
# Stop Smoking Web Page

This web page is designed to help individuals quit smoking by providing valuable information and tools to track smoking habits and financial impact. It is built using .NET 8.0 for the backend, SQLite for database storage, and Vue.js for the frontend.

## Features

- **Compatibility**: Compatible with both tobacco-based smoking and electronic cigarettes.
- **Toxin Data**: Displays general information about toxins present in smoking substances.
- **Calculator**: Includes a calculator feature where users can input their smoking habits to calculate the toxins consumed and money spent. It also provides insights into potential financial gains if the money were invested in the stock market instead.

## Screenshots
*Toxin Data*
![Screenshot 2024-02-29 162718](https://github.com/errapS/stopsmoking/assets/86922755/57ad6c03-f03e-4277-94b4-27742eb4fe80)


*Calculator*
![Screenshot 2024-02-29 162810](https://github.com/errapS/stopsmoking/assets/86922755/395d1afe-8c25-4bd2-ae5f-6ca57768f9c3)


## How to Use

1. Clone this repository to your local machine.
2. Ensure you have .NET 8.0 and Node.js installed on your system.
3. Navigate to the project directory and run the following commands:
   ```bash
   dotnet run
   ```
   This will start the backend server.
4. Navigate to the `client` directory and run the following commands:
   ```bash
   npm install
   npm run serve
   ```
   This will start the Vue.js frontend server.
5. Open your web browser and go to `http://localhost:8080` to view the Stop Smoking web page.
