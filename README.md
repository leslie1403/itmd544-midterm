# ITMD544 - Assignment 2
# Leslie Esquivel

## Links

GitHub Repository URL: https://github.com/leslie1403/itmd544-assignment2 

Live API Documentation URL: https://itmd544-assignment2-c6ecbphyf8dubtep.canadacentral-01.azurewebsites.net/ 


## Reflection 
For this assignment, I chose to create a library domain because as a book collector, it caught my attention to attempt to model a library as a REST API. A library system is able utilize CRUD operations since books can be listed, created, retrieved, updated, and deleted. It also allowed me to create a custom endpoint through /stats, where I could return aggregate information such as total books, average publication year, and books grouped by genre. I decided to use ASP.NET Core Web API because I wanted more experience working in C# and building structured backend applications.

This assignment helped me understand the importance of contract-first API development. Instead of starting with code, I first defined the API using an OpenAPI 3.1.0 specification. This required me to carefully think through all endpoints, request bodies, response formats, and data structures before implementation. The specification acted as a clear blueprint for the API and made it easier to stay organized while developing. It also helped ensure consistency and made testing more structured, since every request and response followed a predefined contract.

One challenge I faced was making sure that my implementation matched the specification exactly. Even small mismatches in route paths, field names, or response structures could cause issues. Another challenge was organizing the project structure and ensuring that each endpoint behaved correctly, especially when using an in-memory data store where POST, PATCH, and DELETE operations modified the data. Testing each operation carefully and returning the correct HTTP status codes also required attention to detail.

Compared to a code-first approach, contract-first development felt more structured and intentional. In a code-first workflow, it is easy to start building quickly and document the API afterward, which can lead to inconsistencies. This is different to a contract-first approach since it forces you to plan ahead and define clear expectations before writing any code. Overall, this assignment showed me how an OpenAPI specification can guide development, improve consistency, and make APIs easier to test and understand.
 
## Screenshots 
Review PDF attached in Canvas for a more in depth view of the operations functioning. 