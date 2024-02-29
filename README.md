
# Stop Smoking Web Page

This web page is designed to help individuals quit smoking by providing valuable information and tools to track smoking habits and financial impact. It is built using .NET 8.0 for the backend, SQLite for database storage, and Vue.js for the frontend.

## Features

- **Compatibility**: Compatible with both tobacco-based smoking and electronic cigarettes.
- **Toxin Data**: Displays general information about toxins present in smoking substances.
- **Calculator**: Includes a calculator feature where users can input their smoking habits to calculate the toxins consumed and money spent. It also provides insights into potential financial gains if the money were invested in the stock market instead.

## Screenshots
*Toxin Data*
![image](https://github.com/errapS/stopSmoking/assets/86922755/4d81cc49-b4ab-479a-a486-04e288d97610)

*Calculator*
![image](https://github.com/errapS/stopSmoking/assets/86922755/9dfcbe49-5b21-4e5b-af71-f9b6efe996e9)

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
