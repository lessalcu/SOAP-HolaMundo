# SOAPHelloWorld - ASP.NET Core

This is a **"Hello World"** project using **SOAP** in **ASP.NET Core**. The application exposes a SOAP web service that responds with a greeting message, and has interactive documentation automatically generated using **Swagger UI**.

## Features

- Implementation of a **SOAP** service with ASP.NET Core.
- SOAP endpoint available at `/HelloWorld` that processes **POST** requests with an XML message.
- **Swagger UI** for visualizing and testing the API.
- SOAP response with a custom message.

## Requirements

- .NET 6.0 or higher
- ASP.NET Core
- Swagger for documentation

## Installation

1. Clone the repository:
```bash
git clone https://github.com/lessalcu/SOAP-HelloWorld.git
cd soapholamundo
```

2. Restore dependencies:
```bash
dotnet restore
```

3. Run the application:
```bash
dotnet run
```

4. The application will run at `http://localhost:5000` (by default). You can access Swagger UI at `http://localhost:5000/swagger`.

## Usage

- **Path for HTML file**:
Accessing `http://localhost:5000/` will serve the `index.html` file

- **SOAP endpoint**:
The SOAP service is available at the `/HelloWorld` endpoint and accepts **POST** requests in the following format:

**SOAP request example**:
```xml
<soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:web="http://localhost/">
<soapenv:Header/>
<soapenv:Body>
<web:SayHello>
<web:Name>John</web:Name>
</web:SayHello>
</soapenv:Body>
</soapenv:Envelope>
``` 

**SOAP response example**:
 ```xml
 <soapenv:Envelope xmlns:soapenv="http://schemas.xmlsoap.org/soap/envelope/" xmlns:web="http://localhost/">
 <soapenv:Header/>
 <soapenv:Body>
 <web:SayHelloResponse>
 <web:Message>Hello, John</web:Message>
 </web:SayHelloResponse>
 </soapenv:Body>
 </soapenv:Envelope>
 ```

##Docker

### Download the image from Docker Hub

1. To download the image from Docker Hub:
 ```bash
 docker pull lssalas/hello-world-soap:latest
 ```
2. To run the image:
 ```bash
 docker run -p 5000:5000 --name hello-world-soap-container lssalas/hello-world-soap:latest
```
## Notes

This project serves as a basic example to understand the implementation of a SOAP service in **ASP.NET Core**. The integration with **Swagger UI** allows for easy exploration and testing of the service.
```