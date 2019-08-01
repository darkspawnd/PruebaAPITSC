# Test - API
In order to see the good practices, capacities and abilities. It is needed to be developed as an API REST JSON.

## API
Its requested the creation of a JSON REST API, for the management of countries 
(https://en.wikipedia.org/wiki/ISO_3166-1) and its subdivisions (ie.: Departments)

## Expected
It is expected that the API allows:
- Register a country (name, alpha code, numeric code, etc)
- Register a country subdivision
- List multiple countries
	- Filter by name and Alpha Code
	- Totalize the amount of obtained countries
- Consult the information of a country subdivision
- List multiple subdivisions of a country
	- Totalize the amount of obtained country subdivisions
- Update a countrys information
- Update a country subdivisions information
- Delete a country
- Delete a country subdivision

The API must be developed in ASP.NET Core utilizing the C# Web API Template and have with the instalation and usage 
documentation.


## Installation

1. Clone the repository
2. Execute the following command `dotnet ef database update`
3. Execute the Visual Studio Solution

Swagger Route: `http://localhost:port/swagger/v1`